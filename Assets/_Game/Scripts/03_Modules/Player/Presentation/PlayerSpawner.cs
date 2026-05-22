using BillGameCore.Modules.Player.Application;
using BillGameCore.SharedPorts.Input;
using UnityEngine;
using BillGameCore.Core.ValueObjects;
namespace BillGameCore.Modules.Player.Presentation
{
    public sealed class PlayerSpawner
    {
        private readonly PlayerView _playerViewPrefab;
        private readonly float _moveSpeed;
        private readonly IInputCommandSource _inputCommandSource;

        public PlayerSpawner(PlayerView playerViewPrefab, float moveSpeed,
            IInputCommandSource inputCommandSource) //+
        {
            _playerViewPrefab = playerViewPrefab;
            _moveSpeed = moveSpeed;
            _inputCommandSource = inputCommandSource; //+
        }

        public PlayerRuntime Spawn(Vector2 spawnPosition)
        {
            var playerView = Object.Instantiate(_playerViewPrefab, spawnPosition, Quaternion.identity);

            var entityId = BillEntityId.New();
            
            var application = new PlayerApplication(_moveSpeed);
            var presenter = new PlayerPresenter(playerView, application, _inputCommandSource, entityId); //+

            var runtime = new PlayerRuntime(presenter, entityId);

            return runtime;
        }
    }
}