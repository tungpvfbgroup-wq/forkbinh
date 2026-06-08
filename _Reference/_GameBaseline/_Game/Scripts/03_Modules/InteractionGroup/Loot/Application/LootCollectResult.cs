using BillGameCore.Core.Inventory;
using BillGameCore.Core.Rewards;

namespace BillGameCore.Modules.InteractionGroup.Loot.Application
{
    public readonly struct LootCollectResult
    {
        public bool WasCollected { get; }
        public RewardBundle Reward { get; }
        public ItemStack ItemStack { get; }

        public bool HasReward =>
            Reward.Gold > 0 || Reward.Experience > 0;

        public bool HasItemStack =>
            ItemStack.IsValid;

        public LootCollectResult(bool wasCollected, RewardBundle reward)
        {
            WasCollected = wasCollected;
            Reward = reward;
            ItemStack = default;
        }
        public LootCollectResult(bool wasCollected, ItemStack itemStack)
        {
            WasCollected = wasCollected;
            Reward = new RewardBundle(0, 0);
            ItemStack = itemStack;
        }
    }
}