using BillGameCore.Core.Rewards;
using BillGameCore.Core.ValueObjects;

namespace BillGameCore.SharedPorts.Messages
{
    // Publish khi enemy death cần broadcast sang loot/economy/UI/audio.
    // Position không đặt ở SharedPorts nếu SharedPorts không reference UnityEngine.
    public readonly struct EnemyDiedMessage
    {
        public EnemyDiedMessage(EntityId enemyId, RewardBundle bundle)
        {
            EnemyId = enemyId;
            Bundle = bundle;
        }

        public EntityId EnemyId { get; }
        public RewardBundle Bundle { get; }
    }
}