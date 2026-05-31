
using BillGameCore.Core.Rewards;
using BillGameCore.Modules.Enemy.Application;
using System;

namespace BillGameCore.Modules.Enemy.Presentation
{
    public class EnemyPresenter
    {
        private readonly EnemyApplication _application;
        public Action<RewardBundle> DiedCallback { get; set; }
        public EnemyPresenter(EnemyApplication application)
        {
            _application = application;
            _application.DiedCallback = HandleDied;
        }
        public void ReceiveDamage(float damage)
        {
            _application.ReceiveDamage(damage);
        }
        private void HandleDied(RewardBundle reward)
        {
            DiedCallback?.Invoke(reward);
        }
    }
}
