using BillGameCore.Core.Inventory;
using BillGameCore.Core.Rewards;
using BillGameCore.Core.ValueObjects;
using BillGameCore.Modules.Enemy.Presentation;
using BillGameCore.Modules.InteractionGroup.Chest.Application;
using BillGameCore.Modules.InteractionGroup.Chest.Presentation;
using BillGameCore.Modules.InteractionGroup.Loot.Application;
using BillGameCore.Modules.InteractionGroup.Loot.Presentation;
using BillGameCore.SharedPorts.Economy;
using BillGameCore.SharedPorts.Input;
using BillGameCore.SharedPorts.Inventory;
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
        private IInventoryWriteService _inventoryWriteService;
        private IInputContextService _inputContextService;
        private LootSpawner _lootSpawner;
        private bool _isPlayerDead;
        private bool _isRestarting;
        private const float MultiLootHorizontalOffset = 0.15f;
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
        public void SetInventoryWriteService(IInventoryWriteService inventoryWriteService)
        {
            _inventoryWriteService = inventoryWriteService ?? throw new ArgumentNullException(nameof(inventoryWriteService));
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
        public void HandleEnemyDied(BillEntityId enemyId, RewardBundle reward, ItemStack itemStack, Vector2 deathWorldPosition)
        {
            var hasReward =
                reward.Gold != 0 ||
                reward.Experience != 0;

            var hasItemStack = itemStack.IsValid;

            if (!hasReward && !hasItemStack)
            {
                return;
            }

            if (_lootSpawner == null)
            {
                throw new InvalidOperationException("SceneController requires a LootSpawner before handling enemy loot.");
            }

            if (hasReward)
            {
                var rewardLootPosition = hasItemStack
                    ? deathWorldPosition + new Vector2(-MultiLootHorizontalOffset, 0f)
                    : deathWorldPosition;

                var rewardLoot = _lootSpawner.Spawn(rewardLootPosition, reward);
                rewardLoot.CollectedCallback = HandleLootCollected;
            }

            if (hasItemStack)
            {
                var itemLootPosition = hasReward
                    ? deathWorldPosition + new Vector2(MultiLootHorizontalOffset, 0f)
                    : deathWorldPosition;

                var itemLoot = _lootSpawner.Spawn(itemLootPosition, itemStack);
                itemLoot.CollectedCallback = HandleLootCollected;
            }
        }        
        public void HandleLootCollected(LootCollectResult result)
        {
            if (!result.WasCollected)
            {
                return;
            }

            if (result.HasItemStack)
            {
                if (_inventoryWriteService == null)
                {
                    throw new InvalidOperationException("SceneController requires an IInventoryWriteService before handling collected item loot.");
                }

                _inventoryWriteService.AddItem(result.ItemStack);
                return;
            }

            if (!result.HasReward)
            {
                return;
            }

            if (_rewardGrantService == null)
            {
                throw new InvalidOperationException("SceneController requires an IRewardGrantService before handling collected reward loot.");
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