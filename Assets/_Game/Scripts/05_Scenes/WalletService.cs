using BillGameCore.Core.Rewards;
using BillGameCore.SharedPorts.Economy;

namespace BillGameCore.Scenes
{
    public sealed class WalletService : IWalletService
    {
        public int Gold { get; private set; }
        public int Experience { get; private set; }

        public void Grant(RewardBundle bundle)
        {
            Gold += bundle.Gold;
            Experience += bundle.Experience;
        }
    }
}