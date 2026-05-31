using BillGameCore.Core.Rewards;
using BillGameCore.Modules.InteractionGroup.Chest.Application;
using BillGameCore.Modules.InteractionGroup.Chest.Presentation;
using BillGameCore.SharedPorts.Economy;
using BillGameCore.Modules.Enemy.Presentation;
using System;
using UnityEngine;
namespace BillGameCore.Scenes
{
    
    public sealed class SceneController : MonoBehaviour
    {
        [SerializeField] private ChestBinder[] _chestBinders;
        [SerializeField] private EnemyDebugBinder[] _enemyDebugBinders;
        private IRewardGrantService _rewardGrantService;
        private void Awake()
        {
            if (_chestBinders == null|| _chestBinders.Length == 0)
            {
                throw new InvalidOperationException("SceneController requires a ChestBinder reference.");
            }
            for (int i = 0; i < _chestBinders.Length; i++)
            {
                var chestBinder = _chestBinders[i];
                if (chestBinder == null)
                {
                    throw new InvalidOperationException($"SceneController has a null ChestBinder at index {i}.");
                }
                    chestBinder.OpenedCallback = HandleChestOpened;
            }

            if (_enemyDebugBinders == null || _enemyDebugBinders.Length == 0)
            {
                return;
            }

            for (int i = 0; i < _enemyDebugBinders.Length; i++)
            {
                var enemyDebugBinder = _enemyDebugBinders[i];

                if (enemyDebugBinder == null)
                {
                    throw new InvalidOperationException($"SceneController has a null EnemyDebugBinder at index {i}.");
                }

                enemyDebugBinder.DiedCallback = HandleEnemyDied;
            }
        }
        public void SetRewardGrantService(IRewardGrantService rewardGrantService)
        {
            _rewardGrantService = rewardGrantService;
        }
        public void HandlePlayerDied()
        {
        }
        public void HandleEnemyDied(RewardBundle reward)
        {
            if (_rewardGrantService == null)
            {
                throw new InvalidOperationException("SceneController requires an IRewardGrantService before handling enemy rewards.");
            }

            _rewardGrantService.Grant(reward);
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