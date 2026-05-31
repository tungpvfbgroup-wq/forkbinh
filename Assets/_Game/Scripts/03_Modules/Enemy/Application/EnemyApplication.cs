using System;
using BillGameCore.Core.Rewards;
using BillGameCore.Modules.Enemy.Domain;

namespace BillGameCore.Modules.Enemy.Application
{
    public sealed class EnemyApplication
    {
        private readonly EnemyState _state;
        private readonly EnemyDefinition _definition;

        public Action<RewardBundle> DiedCallback { get; set; }
        public EnemyApplication(EnemyDefinition definition, EnemyState state)
        {
            _definition = definition;
            _state = state;
        }

        public void ReceiveDamage(float damage)
        {
            if (_state.IsDead)
            {
                return;
            }

            if (damage < 0f)
            {
                damage = 0f;
            }

            var nextHealth = _state.CurrentHealth - damage;

            if (nextHealth > 0f)
            {
                _state.SetCurrentHealth(nextHealth);
                return;
            }

            _state.SetCurrentHealth(0f);
            _state.MarkDead();
            DiedCallback?.Invoke(new RewardBundle(_definition.GoldReward, _definition.ExperienceReward));
        }
    }
}