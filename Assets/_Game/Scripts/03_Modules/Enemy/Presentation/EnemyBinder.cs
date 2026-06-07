using System;
using BillGameCore.Core.Rewards;
using UnityEngine;
using BillGameCore.Core.Combat;
using BillGameCore.Core.ValueObjects;
namespace BillGameCore.Modules.Enemy.Presentation
{
    public sealed class EnemyBinder : MonoBehaviour, IDamageReceiver
    {
        [SerializeField] private EnemyConfig _config;
        [SerializeField] private EnemyView _view;
        [SerializeField] private Collider2D _targetCollider;
        [SerializeField] private EnemyAttackSensor _attackSensor;
        [SerializeField] private float _debugDamageAmount = 1f;
        public EnemyConfig Config => _config;
        private EnemyRuntime _runtime;
        private float _nextAttackTime;
        public Action<BillEntityId, RewardBundle, Vector2> DiedCallback { get; set; }

        private void Awake()
        {
            if (_config == null)
            {
                throw new InvalidOperationException("EnemyBinder requires an EnemyConfig reference.");
            }

            if (_view == null)
            {
                throw new InvalidOperationException("EnemyBinder requires an EnemyView reference.");
            }
            if (_targetCollider == null)
            {
                throw new InvalidOperationException("EnemyBinder requires a Collider2D reference.");
            }
            if (_attackSensor == null)
            {
                throw new InvalidOperationException("EnemyBinder requires an EnemyAttackSensor reference.");
            }
        }
        public void InitializeRuntime(EnemyRuntime runtime)
        {
            if (runtime == null)
            {
                throw new ArgumentNullException(nameof(runtime));
            }
            _runtime?.Dispose();
            _runtime = null;
            _view.ShowAliveState();
            _targetCollider.enabled = true;
            _attackSensor.gameObject.SetActive(true);
            _nextAttackTime = 0f;
            _runtime = runtime;
            _runtime.SetDiedCallback(HandleDied);
            var health = _runtime.GetHealth();
        }
        private void Update()
        {
            TryAttackCurrentTarget();
        }

        private void TryAttackCurrentTarget()
        {
            if (_runtime == null)
            {
                return;
            }

            if (_config.AttackDamage <= 0f)
            {
                return;
            }

            var target = _attackSensor.CurrentTarget;

            if (target == null)
            {
                return;
            }

            if (Time.time < _nextAttackTime)
            {
                return;
            }

            var damageInfo = new DamageInfo(_config.AttackDamage, _runtime.Id, false);
            var result = target.ReceiveDamage(damageInfo);

            _nextAttackTime = Time.time + _config.AttackCooldown;


            if (result.AppliedDamage <= 0f)
            {
                return;
            }
        }
        private void HandleDied(RewardBundle reward)
        {
            if (_runtime == null)
            {
                throw new InvalidOperationException("EnemyBinder requires a runtime before handling death.");
            }
            _view.ShowDeadState();
            _targetCollider.enabled = false;
            _attackSensor.gameObject.SetActive(false);
            DiedCallback?.Invoke(_runtime.Id, reward, _view.WorldPosition);
        }
        public DamageResult ReceiveDamage(DamageInfo damageInfo)
        {
            if (_runtime == null)
            {
                throw new InvalidOperationException("EnemyBinder has not created an EnemyRuntime.");
            }
            var result = _runtime.ReceiveDamage(damageInfo);

            if (result.AppliedDamage > 0f && !result.JustDied)
            {
                _view.ShowHitState();
            }

            return result;
        }
        private void OnDestroy()
        {
            _runtime?.Dispose();
            _runtime = null;
        }
        [ContextMenu("Debug/Reset Enemy")]
        private void DebugResetEnemy()
        {
            InitializeRuntime(new EnemyRuntimeFactory().Create(_config));
        }
        [ContextMenu("Debug/Apply Damage")]
        private void DebugApplyDamage()
        {
            if (_runtime == null)
            {
                throw new InvalidOperationException("EnemyBinder has not created an EnemyRuntime.");
            }
            if (_debugDamageAmount <= 0f)
            {
                throw new InvalidOperationException("EnemyBinder requires a debug damage amount greater than 0.");
            }
            var damageInfo = new DamageInfo(_debugDamageAmount, BillEntityId.Invalid, false);
            var result = _runtime.ReceiveDamage(damageInfo);

        }
    }
}