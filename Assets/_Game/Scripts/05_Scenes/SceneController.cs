using BillGameCore.Modules.InteractionGroup.Chest.Application;
using BillGameCore.Modules.InteractionGroup.Chest.Presentation;
using BillGameCore.SharedPorts.Economy;
using System;
using UnityEngine;
namespace BillGameCore.Scenes
{
    
    public sealed class SceneController : MonoBehaviour
    {
        [SerializeField] private ChestBinder _chestBinder;
        private IRewardGrantService _rewardGrantService;
        private void Awake()
        {
            if (_chestBinder == null)
            {
                throw new InvalidOperationException("SceneController requires a ChestBinder reference.");
            }

            _chestBinder.OpenedCallback = HandleChestOpened;
        }
        public void SetRewardGrantService(IRewardGrantService rewardGrantService)
        {
            _rewardGrantService = rewardGrantService;
        }
        public void HandlePlayerDied()
        {
        }
        public void HandleChestOpened(ChestOpenResult result)
        {
            if (_rewardGrantService == null)
            {
                throw new InvalidOperationException("SceneController requires an IRewardGrantService before handling chest rewards.");
            }
                _rewardGrantService.Grant(result.Reward);
        }
    }
}