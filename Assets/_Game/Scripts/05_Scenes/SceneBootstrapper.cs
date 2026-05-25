using BillGameCore.Modules.Input.Application;
using BillGameCore.Modules.Input.Commands;
using BillGameCore.Modules.Input.Infrastructure;
using BillGameCore.Modules.Player.Presentation;
using BillGameCore.SharedPorts.Input;
using System;
using UnityEngine;

namespace BillGameCore.Scenes
{
    public sealed class SceneBootstrapper : MonoBehaviour
    {
        [SerializeField] private InputReader _inputReader;
        [SerializeField] private PlayerView _playerViewPrefab;
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private Vector2 _spawnPosition = Vector2.zero;
        [SerializeField] private SceneController _sceneController;
        private PlayerRuntime _playerRuntime;

        [ContextMenu("Debug/Switch Context To Player")]
        private void DebugSwitchContextToPlayer()
        {
            _inputReader.SwitchContext(InputContext.Player);
        }

        [ContextMenu("Debug/Switch Context To UI")]
        private void DebugSwitchContextToUI()
        {
            _inputReader.SwitchContext(InputContext.UI);
        }

        private void Awake()
        {
            var commandBuffer = new CommandBuffer(32);
            _inputReader.SetCommandBuffer(commandBuffer);

            var inputCommandSource = new InputCommandDispatcher(commandBuffer);
            var playerSpawner = new PlayerSpawner(_playerViewPrefab, _moveSpeed, inputCommandSource);
            _playerRuntime = playerSpawner.Spawn(_spawnPosition);
            _inputReader.SetControlledEntity(_playerRuntime.EntityId);

            _playerRuntime.SetOnDiedCallback(_sceneController.HandlePlayerDied);

            if (_sceneController == null)
            { throw new InvalidOperationException("SceneBootstrapper requires a SceneController reference."); }
            var rewardGrantService = new DebugRewardGrantService();
            _sceneController.SetRewardGrantService(rewardGrantService);
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