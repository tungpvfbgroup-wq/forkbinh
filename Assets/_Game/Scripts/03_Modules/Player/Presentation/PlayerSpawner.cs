using BillGameCore.Core.ValueObjects;
using BillGameCore.Modules.Player.Application;
using BillGameCore.Modules.Player.Domain;
using BillGameCore.Modules.Player.Infrastructure.Config;
using BillGameCore.SharedPorts.Input;
using System;
using UnityEngine;
namespace BillGameCore.Modules.Player.Presentation
{
    public sealed class PlayerSpawner
    {
        private readonly PlayerView _playerViewPrefab;
        private readonly IInputCommandSource _inputCommandSource;
        private readonly PlayerConfig _playerConfig;
        public PlayerSpawner
           (PlayerView playerViewPrefab,
            PlayerConfig playerConfig,
            IInputCommandSource inputCommandSource
            )
        {
            _playerViewPrefab = playerViewPrefab ?? throw new ArgumentNullException(nameof(playerViewPrefab));
            _playerConfig = playerConfig ?? throw new ArgumentNullException(nameof(playerConfig));
            _inputCommandSource = inputCommandSource ?? throw new ArgumentNullException(nameof(inputCommandSource));
        }

        public PlayerRuntime Spawn(Vector2 spawnPosition)
        {
            var playerView = UnityEngine.Object.Instantiate(_playerViewPrefab, spawnPosition, Quaternion.identity);

            var entityId = BillEntityId.New();

            var definition = _playerConfig.ToDefinition();
            var state = new PlayerState(definition.MaxHealth);

            var application = new PlayerApplication(entityId, definition, state);
            var presenter = new PlayerPresenter(
                                           playerView,
                                           application,
                                           _inputCommandSource,
                                           entityId,
                                           definition.AttackDamage,
                                           definition.AttackCooldown);

            var runtime = new PlayerRuntime(presenter, entityId);
            var combatReceiver = playerView.GetComponent<PlayerCombatReceiver>();

            if (combatReceiver == null)
            {
                throw new InvalidOperationException("PlayerView prefab requires a PlayerCombatReceiver component on the root GameObject.");
            }

            combatReceiver.Bind(runtime);
            return runtime;
        }
    }
}