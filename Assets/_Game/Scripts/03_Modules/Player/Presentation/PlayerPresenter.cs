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
        private readonly float _attackDamage;
        private readonly float _attackCooldown;
        private float _nextAttackTime;
        private Vector2 _lastAttackDirection = Vector2.down;
        public PlayerPresenter(PlayerView view, PlayerApplication application,
            IInputCommandSource inputCommandSource, BillEntityId entityId, float attackDamage, float attackCooldown)
        {
            _view = view;
            _application = application;
            _inputCommandSource = inputCommandSource;
            _entityId = entityId;
            _attackDamage = attackDamage;
            _attackCooldown = attackCooldown;
            _view.SetAttackSensorDirection(_lastAttackDirection);
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
                        HandleInteractCommand();
                    }
                    continue;
                }
                if (command.Type == CommandType.Attack)
                {
                    if (command is IAttackCommand attackCommand)
                    {
                        HandleAttackCommand();
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
            if (latestMoveCommand.IsMoving)
            {
                _lastAttackDirection = new Vector2(latestMoveCommand.DirX, latestMoveCommand.DirY);
                _view.SetAttackSensorDirection(_lastAttackDirection);
            }
            _view.SetMoveVelocity(new Vector2(velocityX, velocityY));
        }
        private void HandleInteractCommand()
        {
            var interactSensor = _view.InteractSensor;
            if (interactSensor == null)
            {
                throw new InvalidOperationException("PlayerView requires a PlayerInteractSensor reference for interaction.");
            }

            var currentTarget = interactSensor.CurrentTarget;
            if (currentTarget == null)
            {
                return;
            }


            if (!currentTarget.CanInteract())
            {
                return;
            }

            currentTarget.Interact();
        }
    
        private void HandleAttackCommand()
        {
            var attackSensor = _view.AttackSensor;
            if (attackSensor == null)
            {
                throw new InvalidOperationException("PlayerView requires a PlayerAttackSensor reference for attack.");
            }
            var currentTarget = attackSensor.CurrentTarget;
            if (currentTarget == null)
            {
                return;
            }
            if (Time.time < _nextAttackTime)
            {
                return;
            }
            var damageInfo = new DamageInfo(_attackDamage, _entityId, false);
            var result = currentTarget.ReceiveDamage(damageInfo);
            if (result.AppliedDamage <= 0f)
            {
                return;
            }
            _nextAttackTime = Time.time + _attackCooldown;
        }
        public Action OnDiedCallback { get; set; }
        public void Stop()
        {
            _view.SetMoveVelocity(Vector2.zero);
        }
    }
}