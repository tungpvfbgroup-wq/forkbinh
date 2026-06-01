using BillGameCore.Core.Rewards;
using BillGameCore.SharedPorts.Economy;
using System;
using UnityEngine;
namespace BillGameCore.Scenes
{
    public sealed class RewardGrantService : IRewardGrantService
    {
        public readonly IWalletWriteService _wallet;
        public RewardGrantService(IWalletWriteService wallet)
        {
            _wallet = wallet ?? throw new ArgumentNullException(nameof(wallet));
        }
        public void Grant(RewardBundle bundle)
        {
            if (bundle.Gold != 0)
            {
                _wallet.AddGold(bundle.Gold);
            }
            if (bundle.Experience != 0)
            {
                _wallet.AddExperience(bundle.Experience);
            }
            if (bundle.Gold > 0 || bundle.Experience > 0)
            {
                Debug.Log($"Granted reward. Gold: {bundle.Gold}, Exp: {bundle.Experience}");

            }
        }
    }
}