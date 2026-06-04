using BillGameCore.Core.ValueObjects;
using System;

namespace BillGameCore.Modules.Player.Presentation
{
    public sealed class PlayerRuntime : IDisposable
    {
        private bool _isDisposed;
        public PlayerPresenter Presenter { get; }
        public BillEntityId Id { get; }
        public PlayerRuntime(PlayerPresenter presenter, BillEntityId entityId)
        {
            Presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));

            if (!entityId.IsValid)
            {
                throw new ArgumentException("PlayerRuntime requires a valid entity id.", nameof(entityId));
            }
            Id = entityId;
        }

        public void Tick ()    
        {
            if (_isDisposed)
            {
                return;
            }

            Presenter.Tick(); 
        }
        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            _isDisposed = true;
            Presenter.Stop();
        }
    }
}