using BillGameCore.Modules.Player.Application;
using UnityEngine;

namespace BillGameCore.Modules.Player.Presentation
{
    public sealed class PlayerPresenter
    {
        private readonly PlayerView _view;
        private readonly PlayerApplication _application;

        public PlayerPresenter(PlayerView view, PlayerApplication application)
        {
            _view = view;
            _application = application;
        }

        public void TickMove(Vector2 moveInput) 
        {
            _application.ComputeMoveVelocity(
                moveInput.x,
                moveInput.y,
                out float velocityX,
                out float velocityY);

            _view.SetMoveVelocity(new Vector2(velocityX, velocityY));
        }

        public void Stop()
        {
            _view.SetMoveVelocity(Vector2.zero);
        }
    }
}