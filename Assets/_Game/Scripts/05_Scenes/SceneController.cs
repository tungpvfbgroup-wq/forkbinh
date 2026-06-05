using BillGameCore.Core.Rewards;
using BillGameCore.Modules.InteractionGroup.Chest.Application;
using BillGameCore.Modules.InteractionGroup.Chest.Presentation;
using BillGameCore.SharedPorts.Economy;
using BillGameCore.Modules.Enemy.Presentation;
using System;
using UnityEngine;
using BillGameCore.Core.ValueObjects;
namespace BillGameCore.Scenes
{
    
    public sealed class SceneController : MonoBehaviour
    {
        [SerializeField] private ChestBinder[] _chestBinders;
        [SerializeField] private EnemyBinder[] _enemyBinders;
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

            if (_enemyBinders == null || _enemyBinders.Length == 0)
            {
                return;
            }

            for (int i = 0; i < _enemyBinders.Length; i++)
            {
                var enemyBinder = _enemyBinders[i];

                if (enemyBinder == null)
                {
                    throw new InvalidOperationException($"SceneController has a null EnemyBinder at index {i}.");
                }

                enemyBinder.DiedCallback = HandleEnemyDied;
            }
        }
        public void SetRewardGrantService(IRewardGrantService rewardGrantService)
        {
            _rewardGrantService = rewardGrantService;
        }
        public void HandlePlayerDied(BillEntityId playerId, RewardBundle reward, Vector2 deathWorldPosition)
        {
            Debug.Log(
                $"<color=#ff3333><b>[PLAYER DIED]</b></color> Id: <color=#00ffffff><b>{playerId}</b></color> | " +
                $"Reward Gold: <color=#ffff00><b>{reward.Gold}</b></color> | " +
                $"Reward Exp: <color=#ff00ff><b>{reward.Experience}</b></color> | " +
                $"Position: <color=#7fff00>{deathWorldPosition}</color>");
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