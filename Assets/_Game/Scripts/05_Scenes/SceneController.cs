using BillGameCore.Core.Rewards;
using BillGameCore.Core.ValueObjects;
using BillGameCore.Modules.Enemy.Presentation;
using BillGameCore.Modules.InteractionGroup.Chest.Application;
using BillGameCore.Modules.InteractionGroup.Chest.Presentation;
using BillGameCore.Modules.InteractionGroup.Loot.Application;
using BillGameCore.Modules.InteractionGroup.Loot.Presentation;
using BillGameCore.SharedPorts.Economy;
using BillGameCore.SharedPorts.Input;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace BillGameCore.Scenes
{
    
    public sealed class SceneController : MonoBehaviour
    {
        [SerializeField] private ChestBinder[] _chestBinders;
        [SerializeField] private EnemyBinder[] _enemyBinders;
        [SerializeField] private PlayerDeathHudView _playerDeathHudView;
        private IRewardGrantService _rewardGrantService;
        private IInputContextService _inputContextService;
        private LootSpawner _lootSpawner;
        private bool _isPlayerDead;
        private bool _isRestarting;

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

            if (_playerDeathHudView == null)
            {
                throw new InvalidOperationException("SceneController requires a PlayerDeathHudView reference.");
            }
            _playerDeathHudView.RestartRequested += HandleRestartRequested;
        }
        public void SetRewardGrantService(IRewardGrantService rewardGrantService)
        {
            _rewardGrantService = rewardGrantService;
        }
        public void SetLootSpawner(LootSpawner lootSpawner)
        {
            _lootSpawner = lootSpawner ?? throw new ArgumentNullException(nameof(lootSpawner));
        }
        public void SetInputContextService(IInputContextService inputContextService)
        {
            _inputContextService = inputContextService ?? throw new ArgumentNullException(nameof(inputContextService));
        }
        public void InitializeEnemyBinders(EnemyRuntimeFactory enemyRuntimeFactory)
        {
            if (enemyRuntimeFactory == null)
            {
                throw new ArgumentNullException(nameof(enemyRuntimeFactory));
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
                enemyBinder.InitializeRuntime(enemyRuntimeFactory.Create(enemyBinder.Config));
            }
        }
        private void Update()
        {
            if (!_isPlayerDead || _isRestarting)
            {
                return;
            }

            if (_inputContextService == null)
            {
                return;
            }

            if (_inputContextService.WasSubmitPressedThisFrame())
            {
                HandleRestartRequested();
            }
        }
        public void HandlePlayerDied(BillEntityId playerId, RewardBundle reward, Vector2 deathWorldPosition)
        {
            if (_isPlayerDead)
            {
                return;
            }

            _isPlayerDead = true;
            if (_inputContextService == null)
            {
                throw new InvalidOperationException("SceneController requires an input context service before handling player death.");
            }

            _inputContextService.SwitchContext(InputContext.UI);
            _playerDeathHudView.ShowPlayerDied();
            Debug.Log(
                $"<color=#ff3333><b>[PLAYER DIED]</b></color> Id: <color=#00ffffff><b>{playerId}</b></color> | " +
                $"Reward Gold: <color=#ffff00><b>{reward.Gold}</b></color> | " +
                $"Reward Exp: <color=#ff00ff><b>{reward.Experience}</b></color> | " +
                $"Position: <color=#7fff00>{deathWorldPosition}</color>");
        }
        private void HandleRestartRequested()
        {
            if (!_isPlayerDead || _isRestarting)
            {
                return;
            }

            _isRestarting = true;

            var activeScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(activeScene.path);
        }
        public void HandleEnemyDied(BillEntityId enemyId, RewardBundle reward, Vector2 deathWorldPosition)
        {
            if (_lootSpawner == null)
            {
                throw new InvalidOperationException("SceneController requires a LootSpawner before handling enemy loot.");
            }

            if (reward.Gold == 0 && reward.Experience == 0)
            {
                return;
            }

            var lootBinder = _lootSpawner.Spawn(deathWorldPosition, reward);
            lootBinder.CollectedCallback = HandleLootCollected;
        }
        public void HandleLootCollected(LootCollectResult result)
        {
            if (_rewardGrantService == null)
            {
                throw new InvalidOperationException("SceneController requires an IRewardGrantService before handling collected loot.");
            }

            if (!result.WasCollected)
            {
                return;
            }

            _rewardGrantService.Grant(result.Reward);
        }
        public void HandleChestOpened(ChestOpenResult result)
        {
            if (_rewardGrantService == null)
            {
                throw new InvalidOperationException("SceneController requires an IRewardGrantService before handling chest rewards.");
            }
                _rewardGrantService.Grant(result.Reward);
        }
        private void OnDestroy()
        {
            if (_playerDeathHudView != null)
            {
                _playerDeathHudView.RestartRequested -= HandleRestartRequested;
            }
        }
    }
}