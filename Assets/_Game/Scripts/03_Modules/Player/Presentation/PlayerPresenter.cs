using BillGameCore.Modules.Player.Application;
using BillGameCore.SharedPorts.Input;
using UnityEngine;

namespace BillGameCore.Modules.Player.Presentation
{
    public sealed class PlayerPresenter
    {
        private readonly PlayerView _view;
        private readonly PlayerApplication _application;
        private readonly IInputCommandSource _inputCommandSource;

        public PlayerPresenter(PlayerView view, PlayerApplication application,
            IInputCommandSource inputCommandSource)
        {
            _view = view;
            _application = application;
            _inputCommandSource = inputCommandSource;
        }

        public void Tick()    //(Vector2 moveInput) 
        {
            //var moveCommand = _inputCommandSource.ReadMoveCommand();
            if (!_inputCommandSource.HasCommands)
            {
                return;
            }

            if (!_inputCommandSource.TryDequeue(out var command))
            {
                return;
            }

            if (command.Type != CommandType.Move)
            {
                return;
            }

            if (command is not IMoveCommand moveCommand)
            {
                return;
            }

            _application.ComputeMoveVelocity(
                moveCommand.X,    //moveInput.x,
                moveCommand.Y,    //moveInput.y,
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