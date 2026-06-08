
using BillGameCore.Core.Rewards;
using BillGameCore.Modules.Enemy.Application;
using System;
using BillGameCore.Core.Combat;
using BillGameCore.Core.Inventory;
namespace BillGameCore.Modules.Enemy.Presentation
{
    public class EnemyPresenter : IDisposable
    {
        private readonly EnemyApplication _application;
        public Action<RewardBundle, ItemStack> DiedCallback { get; set; }
        public EnemyPresenter(EnemyApplication application)
        {
            _application = application;
            _application.DiedCallback = HandleDied;
        }
        public EnemyHealthReadModel GetHealth()
        {
            return _application.GetHealth();
        }
        public DamageResult ReceiveDamage(DamageInfo damageInfo)
        {
            return _application.ReceiveDamage(damageInfo);
        }
        private void HandleDied(RewardBundle reward, ItemStack itemStack)
        {
            DiedCallback?.Invoke(reward, itemStack);
        }

        public void Dispose()
        {
            _application.DiedCallback -= HandleDied;
            DiedCallback = null;
        }
    }
}
