using BillGameCore.Core.Inventory;
using BillGameCore.Core.Rewards;
using System;

namespace BillGameCore.Modules.InteractionGroup.Loot.Domain
{
    public sealed class LootDefinition
    {
        public RewardBundle Reward { get; }
        public ItemStack ItemStack { get; }

        public bool HasReward =>
            Reward.Gold > 0 || Reward.Experience > 0;

        public bool HasItemStack =>
            ItemStack.IsValid;
        public LootDefinition(RewardBundle reward)
        {
            if (reward.Gold < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(reward), "LootDefinition requires reward.Gold >= 0.");
            }

            if (reward.Experience < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(reward), "LootDefinition requires reward.Experience >= 0.");
            }

            Reward = reward;
            ItemStack = default;
        }
            public LootDefinition(ItemStack itemStack)
        {
            if (!itemStack.IsValid)
            {
                throw new ArgumentException("LootDefinition requires a valid item stack.", nameof(itemStack));
            }

            Reward = new RewardBundle(0, 0);
            ItemStack = itemStack;
        }
    }
}