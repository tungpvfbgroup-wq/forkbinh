using BillGameCore.Core.ValueObjects;
using System;

namespace BillGameCore.Modules.Player.Presentation
{
    public sealed class PlayerRuntime : IDisposable
    {
        private readonly PlayerPresenter _presenter;
        private bool _isDisposed;
        public BillEntityId EntityId { get; }
        public PlayerRuntime(PlayerPresenter presenter, BillEntityId entityId)
        {
            _presenter = presenter;
            EntityId = entityId;
        }

        public void Tick ()    //(Vector2 moveInput)
        {
            if (_isDisposed)
            {
                return;
            }

            _presenter.Tick(); //(moveInput);
        }

        public void SetOnDiedCallback(Action onDiedCallback)
        {
            _presenter.OnDiedCallback = onDiedCallback;
        }

        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            _isDisposed = true;
            _presenter.Stop();
        }
    }
}