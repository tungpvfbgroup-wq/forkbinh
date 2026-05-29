using BillGameCore.Core.Rewards;
using BillGameCore.SharedPorts.Economy;
using UnityEngine;

namespace BillGameCore.Scenes
{
    public sealed class DebugRewardGrantService : IRewardGrantService
    {
        public readonly DebugWalletService _wallet;
        public DebugRewardGrantService(DebugWalletService wallet)
        {
            _wallet = wallet;
        }
        public void Grant(RewardBundle bundle)
        {
            _wallet.Grant(bundle);

            if (bundle.Gold > 0)
            {
                Debug.Log($"Granted reward. Gold: {bundle.Gold}. Total Gold: {_wallet.Gold}");
            }
        }
    }
}