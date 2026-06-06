using BillGameCore.Core.Rewards;

namespace BillGameCore.Modules.InteractionGroup.Loot.Application
{
    public readonly struct LootCollectResult
    {
        public bool WasCollected { get; }
        public RewardBundle Reward { get; }

        public LootCollectResult(bool wasCollected, RewardBundle reward)
        {
            WasCollected = wasCollected;
            Reward = reward;
        }
    }
}