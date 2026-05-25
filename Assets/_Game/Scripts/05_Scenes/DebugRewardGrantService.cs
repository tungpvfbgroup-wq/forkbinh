using BillGameCore.Core.Rewards;
using BillGameCore.SharedPorts.Economy;
using UnityEngine;

namespace BillGameCore.Scenes
{
    public sealed class DebugRewardGrantService : IRewardGrantService
    {
        public void Grant(RewardBundle bundle)
        {
            if (bundle.Gold <= 0)
            {
                return;
            }

            Debug.Log($"Granted reward. Gold: {bundle.Gold}");
        }
    }
}