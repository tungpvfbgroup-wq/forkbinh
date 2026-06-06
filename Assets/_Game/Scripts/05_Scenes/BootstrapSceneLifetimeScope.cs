using BillGameCore.Modules.Enemy.Presentation;
using BillGameCore.Modules.Input.Application;
using BillGameCore.Modules.Input.Commands;
using BillGameCore.Modules.Input.Infrastructure;
using BillGameCore.Modules.InteractionGroup.Loot.Presentation;
using BillGameCore.Modules.Player.Infrastructure.Config;
using BillGameCore.Modules.Player.Presentation;
using BillGameCore.SharedPorts.Economy;
using BillGameCore.SharedPorts.Input;
using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;
namespace BillGameCore.Scenes
{
    public sealed class BootstrapSceneLifetimeScope : LifetimeScope
    {
        private const int CommandBufferCapacity = 32;

        [SerializeField] private InputReader _inputReader;
        [SerializeField] private SceneController _sceneController;
        [SerializeField] private WalletReadSource _walletReadSource;
        [SerializeField] private PlayerView _playerViewPrefab;
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private LootBinder _lootBinderPrefab;
        protected override void Configure(IContainerBuilder builder)
        {
            ValidateConfiguration();
            _inputReader.ValidateConfiguration();

            builder.RegisterComponent(_inputReader);
            builder.RegisterComponent(_sceneController);
            builder.RegisterComponent(_walletReadSource);

            builder.Register<CommandBuffer>(
                _ => new CommandBuffer(CommandBufferCapacity),
                Lifetime.Scoped);

            builder.Register<IInputCommandSource, InputCommandDispatcher>(Lifetime.Scoped);

            builder.Register<PlayerSpawner>(
                          resolver => new PlayerSpawner(
                          _playerViewPrefab,
                          _playerConfig,
                          resolver.Resolve<IInputCommandSource>()),
                                                                    Lifetime.Scoped);

            

            builder.Register<WalletService>(Lifetime.Scoped);
            builder.Register<IWalletService>(
                resolver => resolver.Resolve<WalletService>(),
                Lifetime.Scoped);
            builder.Register<IWalletWriteService>(
                resolver => resolver.Resolve<WalletService>(),
                Lifetime.Scoped);

            builder.Register<IRewardGrantService>(
                resolver => new RewardGrantService(resolver.Resolve<IWalletWriteService>()),
                Lifetime.Scoped);
            builder.Register<EnemyRuntimeFactory>(Lifetime.Scoped);
            builder.Register<LootSpawner>(
                _ => new LootSpawner(_lootBinderPrefab), Lifetime.Scoped);

            builder.RegisterEntryPoint<SceneBootstrapper>(Lifetime.Scoped);
        }

        private void ValidateConfiguration()
        {
            if (_inputReader == null)
            {
                throw new InvalidOperationException("BootstrapSceneLifetimeScope requires an InputReader reference.");
            }

            if (_sceneController == null)
            {
                throw new InvalidOperationException("BootstrapSceneLifetimeScope requires a SceneController reference.");
            }

            if (_walletReadSource == null)
            {
                throw new InvalidOperationException("BootstrapSceneLifetimeScope requires a WalletReadSource reference.");
            }

            if (_playerViewPrefab == null)
            {
                throw new InvalidOperationException("BootstrapSceneLifetimeScope requires a PlayerView prefab reference.");
            }
            if (_lootBinderPrefab == null)
            {
                throw new InvalidOperationException("BootstrapSceneLifetimeScope requires a LootBinder prefab reference.");
            }
            if (_playerConfig == null)
            {
                throw new InvalidOperationException("BootstrapSceneLifetimeScope requires a PlayerConfig reference.");
            }

            if (_playerConfig.MoveSpeed <= 0f)
            {
                throw new InvalidOperationException("BootstrapSceneLifetimeScope requires PlayerConfig.MoveSpeed > 0.");
            }

            if (_playerConfig.AttackDamage < 0f)
            {
                throw new InvalidOperationException("BootstrapSceneLifetimeScope requires PlayerConfig.AttackDamage >= 0.");
            }

            if (_playerConfig.AttackCooldown < 0f)
            {
                throw new InvalidOperationException("BootstrapSceneLifetimeScope requires PlayerConfig.AttackCooldown >= 0.");
            }
        }
    }
}
