using BillGameCore.Modules.Input.Infrastructure;
using BillGameCore.Modules.Player.Presentation;
using UnityEngine;

namespace BillGameCore.Scenes
{
    public sealed class PlayerMoveTestBootstrap : MonoBehaviour
    {
        [SerializeField] private InputReader _inputReader;
        [SerializeField] private PlayerView _playerViewPrefab;
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private Vector2 _spawnPosition = Vector2.zero;

        private PlayerRuntime _playerRuntime;

        private void Awake()
        {
            var playerSpawner = new PlayerSpawner(_playerViewPrefab, _moveSpeed);
            _playerRuntime = playerSpawner.Spawn(_spawnPosition);
        }

        private void Update()
        {
            var moveCommand = _inputReader.ReadMoveCommand();
            _playerRuntime.Tick(new Vector2(moveCommand.X, moveCommand.Y));
        }

        private void OnDestroy()
        {
            _playerRuntime?.Dispose();
        }
    }
}