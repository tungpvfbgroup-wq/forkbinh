using BillGameCore.Core.Combat;
using BillGameCore.Core.Interaction;
using BillGameCore.Core.ValueObjects;
using BillGameCore.Modules.Player.Application;
using BillGameCore.SharedPorts.Input;
using System;
using UnityEngine;
namespace BillGameCore.Modules.Player.Presentation
{
    public sealed class PlayerPresenter
    {
        private readonly PlayerView _view;
        private readonly PlayerApplication _application;
        private readonly IInputCommandSource _inputCommandSource;
        private BillEntityId _entityId;
        private IInteractable _currentInteractable;
        private IDamageReceiver _currentDamageReceiver;
        public PlayerPresenter(PlayerView view, PlayerApplication application,
            IInputCommandSource inputCommandSource, BillEntityId entityId)
        {
            _view = view;
            _application = application;
            _inputCommandSource = inputCommandSource;
            _entityId = entityId;
            _view.TriggerEnteredCallback = HandleTriggerEntered;
            _view.TriggerExitedCallback = HandleTriggerExited;
        }
        private void HandleTriggerEntered(Collider2D other)
        {
            var interactable = other.GetComponent<IInteractable>();

            if (interactable == null)
            {
                return;
            }

            _currentInteractable = interactable;

            var damageReceiver = other.GetComponent<IDamageReceiver>();
            if (damageReceiver != null)
            {
                _currentDamageReceiver = damageReceiver;
            }
        }

        private void HandleTriggerExited(Collider2D other)
        {
            var interactable = other.GetComponent<IInteractable>();
            if (interactable == null)
            {
                return;
            }

            if (_currentInteractable == interactable)
            {
                _currentInteractable = null;
            }

            var damageReceiver = other.GetComponent<IDamageReceiver>();
            if (damageReceiver != null && _currentDamageReceiver == damageReceiver)
            {
                _currentDamageReceiver = null;
            }
        }
        public void Tick()
        {
            IMoveCommand latestMoveCommand = null;

            while (_inputCommandSource.TryDequeue(out var command))
            {
                if (command.ControlledEntityId != _entityId)
                {
                    continue;
                }
                if (command.Type == CommandType.Interact)
                {
                    if (command is IInteractCommand interactCommand)
                    {
                        HandleInteractCommand(interactCommand);
                    }
                    continue;
                }
                if (command.Type == CommandType.Attack)
                {
                    if (command is IAttackCommand attackCommand)
                    {
                        HandleAttackCommand(attackCommand);
                    }
                    continue;
                }
                if (command.Type != CommandType.Move)
                {
                    continue;
                }
                if (command is not IMoveCommand moveCommand)
                {
                    continue;
                }

                latestMoveCommand = moveCommand;
            }

            if (latestMoveCommand == null)
            {
                return;
            }

            _application.ComputeMoveVelocity(
                latestMoveCommand.DirX,
                latestMoveCommand.DirY,
                out float velocityX,
                out float velocityY);

            _view.SetMoveVelocity(new Vector2(velocityX, velocityY));
        }
        private void HandleInteractCommand(IInteractCommand interactCommand)
        {
            if (_currentInteractable == null)
            {
                return;
            }

            if (!_currentInteractable.CanInteract())
            {
                return;
            }

            _currentInteractable.Interact();
        }
        private void HandleAttackCommand(IAttackCommand attackCommand)
        {
            if (_currentDamageReceiver == null)
            {
                return;
            }

            var damageInfo = new DamageInfo(1f, _entityId, false);
            _currentDamageReceiver.ReceiveDamage(damageInfo);
        }
        public Action OnDiedCallback { get; set; }

        public void Stop()
        {
            _view.SetMoveVelocity(Vector2.zero);
        }
    }
}