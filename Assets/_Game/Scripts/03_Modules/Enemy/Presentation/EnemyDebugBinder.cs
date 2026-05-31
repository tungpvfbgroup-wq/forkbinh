using System;
using BillGameCore.Core.Rewards;
using UnityEngine;
using BillGameCore.Core.Combat;
using BillGameCore.Core.ValueObjects;
namespace BillGameCore.Modules.Enemy.Presentation
{
    public sealed class EnemyDebugBinder : MonoBehaviour
    {
        [SerializeField] private EnemyConfig _config;
        [SerializeField] private EnemyView _view;
        [SerializeField] private float _debugDamageAmount = 1f;
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
        [ContextMenu("Debug/Apply Damage")]
        private void DebugApplyDamage()
        {
            if (_runtime == null)
            {
                throw new InvalidOperationException("EnemyDebugBinder has not created an EnemyRuntime.");
            }
            if (_debugDamageAmount <= 0f)
            {
                throw new InvalidOperationException("EnemyDebugBinder requires a debug damage amount greater than 0.");
            }
            var damageInfo = new DamageInfo(_debugDamageAmount, BillEntityId.Invalid, false);
            _runtime.ReceiveDamage(damageInfo);
        }
    }
}