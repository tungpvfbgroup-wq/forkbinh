using BillGameCore.Core.Combat;
using BillGameCore.Core.Rewards;
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
        private BillEntityId _controlledEntityId;
        private readonly float _attackDamage;
        private readonly float _attackCooldown;
        private float _nextAttackTime;
        private Vector2 _lastAttackDirection = Vector2.down;
        public Action<BillEntityId, RewardBundle, Vector2> OnDiedCallback { get; set; }

        public PlayerPresenter(PlayerView view, PlayerApplication application,
            IInputCommandSource inputCommandSource, BillEntityId entityId, float attackDamage, float attackCooldown)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _application = application ?? throw new ArgumentNullException(nameof(application));
            _inputCommandSource = inputCommandSource ?? throw new ArgumentNullException(nameof(inputCommandSource));

            if (!entityId.IsValid)
            {
                throw new ArgumentException("PlayerPresenter requires a valid entity id.", nameof(entityId));
            }

            if (attackDamage < 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(attackDamage), "PlayerPresenter requires attackDamage >= 0.");
            }

            if (attackCooldown < 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(attackCooldown), "PlayerPresenter requires attackCooldown >= 0.");
            }

            _controlledEntityId = entityId;
            _attackDamage = attackDamage;
            _attackCooldown = attackCooldown;
            _view.SetAttackSensorDirection(_lastAttackDirection);
            _application.DiedCallback = HandleDied;
        }
        public void Tick()
        {
            IMoveCommand latestMoveCommand = null;
            while (_inputCommandSource.TryDequeue(out var command))
            {
                if (command.ControlledEntityId != _controlledEntityId)
                {
                    continue;
                }
                if (command is IInteractCommand)
                {
                    HandleInteractCommand();
                    continue;
                }

                if (command is IAttackCommand)
                {
                    HandleAttackCommand();
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

        private void HandleDied(BillEntityId playerId, RewardBundle reward)
        {
            Stop();
            OnDiedCallback?.Invoke(playerId, reward, _view.WorldPosition);
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
            var damageInfo = new DamageInfo(_attackDamage, _controlledEntityId, false);
            var result = currentTarget.ReceiveDamage(damageInfo);
            if (result.AppliedDamage <= 0f)
            {
                return;
            }
            _nextAttackTime = Time.time + _attackCooldown;
        }
        public DamageResult ReceiveDamage(DamageInfo damageInfo)
        {
            return _application.ReceiveDamage(damageInfo);
        }
        public void Stop()
        {
            _view.SetMoveVelocity(Vector2.zero);
        }
    }
}