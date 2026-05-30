using BillGameCore.Core.Rewards;
using BillGameCore.SharedPorts.Economy;
using UnityEngine;

namespace BillGameCore.Scenes
{
    public sealed class RewardGrantService : IRewardGrantService
    {
        public readonly WalletService _wallet;
        public RewardGrantService(WalletService wallet)
        {
            _wallet = wallet;
        }
        public void Grant(RewardBundle bundle)
        {
            _wallet.Grant(bundle);

            if (bundle.Gold > 0 || bundle.Experience > 0)
            {
                Debug.Log(
            $"Granted reward. Gold: {bundle.Gold}, Exp: {bundle.Experience}. " +
            $"Total Gold: {_wallet.Gold}, Total Exp: {_wallet.Experience}");
            }
        }
    }
}