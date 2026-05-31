using System;
using BillGameCore.Core.Rewards;

namespace BillGameCore.Modules.Enemy.Presentation
{
    public class EnemyRuntime
    {
        private readonly EnemyPresenter _presenter;
        public EnemyRuntime(EnemyPresenter presenter)
        {
            _presenter = presenter;
        }
        public void SetDiedCallback(Action<RewardBundle> dieCallback)
        {
            _presenter.DiedCallback = dieCallback;
        }
        public void ReceiveDamage(float damage)
        {
            _presenter.ReceiveDamage(damage);
        }
    }
}