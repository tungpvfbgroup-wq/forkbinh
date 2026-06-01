using System;
using BillGameCore.Core.Rewards;
using BillGameCore.Modules.Enemy.Domain;
using BillGameCore.Core.Combat;
namespace BillGameCore.Modules.Enemy.Application
{
    public sealed class EnemyApplication : IDamageReceiver
    {
        private readonly EnemyState _state;
        private readonly EnemyDefinition _definition;

        public Action<RewardBundle> DiedCallback { get; set; }
        public EnemyApplication(EnemyDefinition definition, EnemyState state)
        {
            _definition = definition;
            _state = state;
        }
        public EnemyHealthReadModel GetHealth()
        {
            return new EnemyHealthReadModel(_state.CurrentHealth, _state.IsDead);
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
            DiedCallback?.Invoke(new RewardBundle(_definition.GoldReward, _definition.ExperienceReward));
            return new DamageResult(appliedDamage, 0f, true);
        }
    }
}