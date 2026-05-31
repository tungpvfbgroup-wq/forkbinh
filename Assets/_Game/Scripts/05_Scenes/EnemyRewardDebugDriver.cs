using BillGameCore.Core.Combat;
using BillGameCore.Core.Rewards;
using BillGameCore.Core.ValueObjects;
using BillGameCore.Modules.Enemy.Presentation;
using System;
using UnityEngine;
namespace BillGameCore.Scenes
{
    public sealed class EnemyRewardDebugDriver : MonoBehaviour
    {
        [SerializeField] private EnemyConfig _enemyConfig;
        [SerializeField] private SceneController _sceneController;
        [SerializeField] private EnemyView _enemyView;
        private EnemyRuntime _enemyRuntime;

        private void Awake()
        {
            if (_enemyConfig == null)
            {
                throw new InvalidOperationException("EnemyRewardDebugDriver requires an EnemyConfig reference.");
            }

            if (_sceneController == null)
            {
                throw new InvalidOperationException("EnemyRewardDebugDriver requires a SceneController reference.");
            }
            if (_enemyView == null)
            {
                throw new InvalidOperationException("EnemyRewardDebugDriver requires an EnemyView reference.");
            }
            var enemySpawner = new EnemySpawner(_enemyConfig);
            _enemyRuntime = enemySpawner.Spawn();
            _enemyRuntime.SetDiedCallback(HandleEnemyDied);
        }

        private void HandleEnemyDied(RewardBundle reward)
        {
            _sceneController.HandleEnemyDied(reward);
            _enemyView.ShowDeadState();
        }

        [ContextMenu("Debug/Kill Enemy Reward Runtime")]
        private void DebugKillEnemyRewardRuntime()
        {
            if (_enemyRuntime == null)
            {
                throw new InvalidOperationException("EnemyRewardDebugDriver has not created an EnemyRuntime.");
            }
         // _enemyRuntime.ReceiveDamage(999 );
        }
    }
}