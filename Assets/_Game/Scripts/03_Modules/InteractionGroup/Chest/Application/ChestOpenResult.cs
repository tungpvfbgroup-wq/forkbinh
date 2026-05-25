using BillGameCore.Core.Rewards;

namespace BillGameCore.Modules.InteractionGroup.Chest.Application
{
    public sealed class ChestOpenResult
    {
        public bool WasOpened { get; }
        public RewardBundle Reward { get; }
        public ChestOpenResult(bool wasOpened, RewardBundle reward)
        {
            WasOpened = wasOpened;
            Reward = reward;
        }
    }
}