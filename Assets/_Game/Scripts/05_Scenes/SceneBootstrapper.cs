using BillGameCore.Modules.Input.Application;
using BillGameCore.Modules.Input.Commands;
using BillGameCore.Modules.Input.Infrastructure;
using BillGameCore.Modules.Player.Presentation;
using UnityEngine;

namespace BillGameCore.Scenes
{
    public sealed class SceneBootstrapper : MonoBehaviour
    {
        [SerializeField] private InputReader _inputReader;
        [SerializeField] private PlayerView _playerViewPrefab;
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private Vector2 _spawnPosition = Vector2.zero;

        private PlayerRuntime _playerRuntime;

        private void Awake()
        {
            var commandBuffer = new CommandBuffer(32);
            _inputReader.SetCommandBuffer(commandBuffer);

            var inputCommandSource = new InputCommandDispatcher(commandBuffer);
            var playerSpawner = new PlayerSpawner(_playerViewPrefab, _moveSpeed, inputCommandSource);
            _playerRuntime = playerSpawner.Spawn(_spawnPosition);
        }

        private void Update()
        {
            _playerRuntime.Tick();
        }

        private void OnDestroy()
        {
            _playerRuntime?.Dispose();
        }
    }
}