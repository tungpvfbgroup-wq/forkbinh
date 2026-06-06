using BillGameCore.Modules.Enemy.Presentation;
using BillGameCore.Modules.Input.Infrastructure;
using BillGameCore.Modules.InteractionGroup.Loot.Presentation;
using BillGameCore.Modules.Player.Presentation;
using BillGameCore.SharedPorts.Economy;
using System;
using UnityEngine;
using VContainer.Unity;
namespace BillGameCore.Scenes
{ 
    public sealed class SceneBootstrapper : IStartable, ITickable, IDisposable
    {
        private readonly InputReader _inputReader;
        private readonly PlayerSpawner _playerSpawner;
        private readonly SceneController _sceneController;
        private readonly WalletReadSource _walletReadSource;
        private readonly EnemyRuntimeFactory _enemyRuntimeFactory;
        private readonly LootSpawner _lootSpawner;
        private readonly IWalletService _walletService;
        private readonly IRewardGrantService _rewardGrantService;

        private PlayerRuntime _playerRuntime;

        public SceneBootstrapper(
            InputReader inputReader,
            PlayerSpawner playerSpawner,
            SceneController sceneController,
            WalletReadSource walletReadSource,
            EnemyRuntimeFactory enemyRuntimeFactory,
            LootSpawner lootSpawner,
            IWalletService walletService,
            IRewardGrantService rewardGrantService)
        {
            _inputReader = inputReader ?? throw new ArgumentNullException(nameof(inputReader));
            _playerSpawner = playerSpawner ?? throw new ArgumentNullException(nameof(playerSpawner));
            _sceneController = sceneController ?? throw new ArgumentNullException(nameof(sceneController));
            _walletReadSource = walletReadSource ?? throw new ArgumentNullException(nameof(walletReadSource));
            _enemyRuntimeFactory = enemyRuntimeFactory ?? throw new ArgumentNullException(nameof(enemyRuntimeFactory));
            _lootSpawner = lootSpawner ?? throw new ArgumentNullException(nameof(lootSpawner));
            _walletService = walletService ?? throw new ArgumentNullException(nameof(walletService));
            _rewardGrantService = rewardGrantService ?? throw new ArgumentNullException(nameof(rewardGrantService));
        }
        public void Start()
        {
            _playerRuntime = _playerSpawner.Spawn(Vector2.zero);
            _inputReader.SetControlledEntity(_playerRuntime.Id);
            _sceneController.SetInputContextService(_inputReader);
            _playerRuntime.Presenter.OnDiedCallback = _sceneController.HandlePlayerDied;
            _walletReadSource.SetWalletService(_walletService);
            _sceneController.SetRewardGrantService(_rewardGrantService);
            _sceneController.SetLootSpawner(_lootSpawner);
            _sceneController.InitializeEnemyBinders(_enemyRuntimeFactory);
        }
        public void Tick()
        {
            _playerRuntime?.Tick();
        }
        public void Dispose()
        {
            _playerRuntime?.Dispose();
            _playerRuntime = null;
        }
    }
}