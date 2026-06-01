using BillGameCore.Core.Combat;
using BillGameCore.Core.Rewards;
using BillGameCore.Modules.Enemy.Application;
using System;
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
        public DamageResult ReceiveDamage(DamageInfo damageInfo)
        {
            return _presenter.ReceiveDamage(damageInfo);
        }
        public EnemyHealthReadModel GetHealth()
        {
            return _presenter.GetHealth();
        }
    }
}