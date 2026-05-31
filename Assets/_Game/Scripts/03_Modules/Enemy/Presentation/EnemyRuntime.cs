using System;
using BillGameCore.Core.Rewards;
using BillGameCore.Core.Combat;
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
    }
}