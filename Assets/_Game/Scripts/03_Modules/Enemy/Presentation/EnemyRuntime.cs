using BillGameCore.Core.Combat;
using BillGameCore.Core.Rewards;
using BillGameCore.Modules.Enemy.Application;
using BillGameCore.Core.ValueObjects;
using System;
using UnityEngine;
namespace BillGameCore.Modules.Enemy.Presentation
{
    public class EnemyRuntime : IDisposable
    {
        private bool _isDisposed;
        public BillEntityId Id { get; }
        private readonly EnemyPresenter _presenter;
        public EnemyRuntime(EnemyPresenter presenter, BillEntityId entityId)
        {
            _presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
            if (!entityId.IsValid)
            {
                throw new ArgumentException("EnemyRuntime requires a valid entity id.", nameof(entityId));
            }
            Id = entityId;
        }
        public void SetDiedCallback(Action<RewardBundle> dieCallback)
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException(nameof(EnemyRuntime));
            }
            _presenter.DiedCallback = dieCallback;
        }
        public DamageResult ReceiveDamage(DamageInfo damageInfo)
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException(nameof(EnemyRuntime));
            }
            return _presenter.ReceiveDamage(damageInfo);
        }
        public EnemyHealthReadModel GetHealth()
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException(nameof(EnemyRuntime));
            }
            return _presenter.GetHealth();
        }

        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            _isDisposed = true;
            _presenter.Dispose();
        }
    }
}