using System;
using BillGameCore.Core.Rewards;
using UnityEngine;
using BillGameCore.Core.Combat;
using BillGameCore.Core.ValueObjects;
namespace BillGameCore.Modules.Enemy.Presentation
{
    public sealed class EnemyDebugBinder : MonoBehaviour, IDamageReceiver
    {
        [SerializeField] private EnemyConfig _config;
        [SerializeField] private EnemyView _view;
        [SerializeField] private Collider2D _targetCollider;
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
            if (_targetCollider == null)
            {
                throw new InvalidOperationException("EnemyDebugBinder requires a Collider2D reference.");
            }
            BuildRuntime();
        }
        private void BuildRuntime()
        {
            _view.ShowAliveState();
            _targetCollider.enabled = true;
            var enemySpawner = new EnemySpawner(_config);
            _runtime = enemySpawner.Spawn();
            _runtime.SetDiedCallback(HandleDied);
            var health = _runtime.GetHealth();
            Debug.Log($"Enemy runtime ready. Health: {health.CurrentHealth}, IsDead: {health.IsDead}");
        }
        private void HandleDied(RewardBundle reward)
        {
            _view.ShowDeadState();
            _targetCollider.enabled = false;
            DiedCallback?.Invoke(reward);
        }
        public DamageResult ReceiveDamage(DamageInfo damageInfo)
        {
            if (_runtime == null)
            {
                throw new InvalidOperationException("EnemyDebugBinder has not created an EnemyRuntime.");
            }
            var result = _runtime.ReceiveDamage(damageInfo);

            if (result.AppliedDamage > 0f && !result.JustDied)
            {
                _view.ShowHitState();
            }
            Debug.Log(
    $"Enemy took damage. Applied: {result.AppliedDamage}, Remaining: {result.RemainingHealth}, JustDied: {result.JustDied}");

            return result;
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
            var result = _runtime.ReceiveDamage(damageInfo);

            Debug.Log(
                $"Enemy took damage. Applied: {result.AppliedDamage}, Remaining: {result.RemainingHealth}, JustDied: {result.JustDied}");
        }
    }
}