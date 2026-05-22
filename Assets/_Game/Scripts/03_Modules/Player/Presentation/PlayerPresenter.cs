using BillGameCore.Modules.Player.Application;
using BillGameCore.SharedPorts.Input;
using System;
using UnityEngine;
using BillGameCore.Core.ValueObjects;
namespace BillGameCore.Modules.Player.Presentation
{
    public sealed class PlayerPresenter
    {
        private readonly PlayerView _view;
        private readonly PlayerApplication _application;
        private readonly IInputCommandSource _inputCommandSource;
        private BillEntityId _entityId;
        public PlayerPresenter(PlayerView view, PlayerApplication application,
            IInputCommandSource inputCommandSource, BillEntityId entityId)
        {
            _view = view;
            _application = application;
            _inputCommandSource = inputCommandSource;
            _entityId = entityId;
        }

        public void Tick()
        {
            IMoveCommand latestMoveCommand = null;

            while (_inputCommandSource.TryDequeue(out var command))
            {
                if (command.Type != CommandType.Move)
                {
                    continue;
                }

                if (command.ControlledEntityId != _entityId)
                {
                    continue;
                }

                if (command is not IMoveCommand moveCommand)
                {
                    continue;
                }

                latestMoveCommand = moveCommand;
            }

            if (latestMoveCommand == null)
            {
                return;
            }

            _application.ComputeMoveVelocity(
                latestMoveCommand.DirX,
                latestMoveCommand.DirY,
                out var velocityX,
                out var velocityY);

            _view.SetMoveVelocity(new Vector2(velocityX, velocityY));
        }

        public Action OnDiedCallback { get; set; }

        public void Stop()
        {
            _view.SetMoveVelocity(Vector2.zero);
        }
    }
}