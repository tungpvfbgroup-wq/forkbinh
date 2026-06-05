using BillGameCore.Core.Combat;
using BillGameCore.Core.Rewards;
using BillGameCore.Core.ValueObjects;
using BillGameCore.Modules.Player.Domain;
using System;
namespace BillGameCore.Modules.Player.Application
{
    public sealed class PlayerApplication : IDamageReceiver
    {
        private readonly PlayerDefinition _definition;
        private readonly PlayerState _state;
        private readonly BillEntityId _entityId;
        public Action<BillEntityId, RewardBundle> DiedCallback { get; set; }
        public PlayerApplication(BillEntityId entityId, PlayerDefinition definition, PlayerState state)
        {
            if (!entityId.IsValid)
            {
                throw new ArgumentException("PlayerApplication requires a valid entity id.", nameof(entityId));
            }
            _entityId = entityId;
            _definition = definition ?? throw new ArgumentNullException(nameof(definition));
            _state = state ?? throw new ArgumentNullException(nameof(state));
        }

        public void ComputeMoveVelocity(
            float inputX,
            float inputY,
            out float velocityX,
            out float velocityY)
        {
            var magnitudeSquared = (inputX * inputX) + (inputY * inputY);

            if (magnitudeSquared > 1f)
            {
                var magnitude = MathF.Sqrt(magnitudeSquared);
                inputX /= magnitude;
                inputY /= magnitude;
            }

            velocityX = inputX * _definition.MoveSpeed;
            velocityY = inputY * _definition.MoveSpeed;

            _state.SetMoveVelocity(velocityX, velocityY);
        }

        public DamageResult ReceiveDamage(DamageInfo damageInfo)
        {
            if (_state.IsDead)
            {
                return new DamageResult(0f, _state.CurrentHealth, false);
            }

            var requestedDamage = damageInfo.Amount;

            if (requestedDamage < 0f)
            {
                requestedDamage = 0f;
            }

            var appliedDamage = requestedDamage;

            if (appliedDamage > _state.CurrentHealth)
            {
                appliedDamage = _state.CurrentHealth;
            }

            var nextHealth = _state.CurrentHealth - appliedDamage;

            if (nextHealth > 0f)
            {
                _state.SetCurrentHealth(nextHealth);
                return new DamageResult(appliedDamage, nextHealth, false);
            }
            _state.SetCurrentHealth(0f);
            _state.MarkDead();

            DiedCallback?.Invoke(_entityId, new RewardBundle(0, 0));

            return new DamageResult(appliedDamage, 0f, true);
        }
    }
}