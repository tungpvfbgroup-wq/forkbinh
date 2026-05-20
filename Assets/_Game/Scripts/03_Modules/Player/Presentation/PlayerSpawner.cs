using BillGameCore.Modules.Player.Application;
using UnityEngine;

namespace BillGameCore.Modules.Player.Presentation
{
    public sealed class PlayerSpawner
    {
        private readonly PlayerView _playerViewPrefab;
        private readonly float _moveSpeed;

        public PlayerSpawner(PlayerView playerViewPrefab, float moveSpeed)
        {
            _playerViewPrefab = playerViewPrefab;
            _moveSpeed = moveSpeed;
        }

        public PlayerRuntime Spawn(Vector2 spawnPosition)
        {
            var playerView = Object.Instantiate(_playerViewPrefab, spawnPosition, Quaternion.identity);

            var application = new PlayerApplication(_moveSpeed);
            var presenter = new PlayerPresenter(playerView, application);
            var runtime = new PlayerRuntime(presenter);

            return runtime;
        }
    }
}