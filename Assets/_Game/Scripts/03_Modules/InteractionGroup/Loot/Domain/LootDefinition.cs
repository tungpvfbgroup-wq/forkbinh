using System;
using BillGameCore.Core.Rewards;

namespace BillGameCore.Modules.InteractionGroup.Loot.Domain
{
    public sealed class LootDefinition
    {
        public RewardBundle Reward { get; }

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
        }
    }
}