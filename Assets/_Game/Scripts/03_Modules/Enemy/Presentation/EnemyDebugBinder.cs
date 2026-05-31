using System;
using BillGameCore.Core.Rewards;
using UnityEngine;

namespace BillGameCore.Modules.Enemy.Presentation
{
    public sealed class EnemyDebugBinder : MonoBehaviour
    {
        [SerializeField] private EnemyConfig _config;
        [SerializeField] private EnemyView _view;

        private EnemyRuntime _runtime;

        public Action<RewardBundle> DiedCallback { get; set; }

        private void Awake()
        {
            if (_config == null)
            {
                throw new InvalidOperationException("EnemyDebugBinder requires an EnemyConfig reference.");
            }

            if (_view == null)
            {
                throw new InvalidOperationException("EnemyDebugBinder requires an EnemyView reference.");
            }
            BuildRuntime();
        }
        private void BuildRuntime()
        {
            _view.ShowAliveState();

            var enemySpawner = new EnemySpawner(_config);
            _runtime = enemySpawner.Spawn();
            _runtime.SetDiedCallback(HandleDied);
        }
        private void HandleDied(RewardBundle reward)
        {
            _view.ShowDeadState();
            DiedCallback?.Invoke(reward);
        }
        [ContextMenu("Debug/Reset Enemy")]
        private void DebugResetEnemy()
        {
            BuildRuntime();
        }
        [ContextMenu("Debug/Kill Enemy")]
        private void DebugKillEnemy()
        {
            if (_runtime == null)
            {
                throw new InvalidOperationException("EnemyDebugBinder has not created an EnemyRuntime.");
            }

            _runtime.ReceiveDamage(9999f);
        }
    }
}