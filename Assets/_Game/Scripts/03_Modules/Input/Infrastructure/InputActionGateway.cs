using System;
using BillGameCore.Modules.Input.Context;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BillGameCore.Modules.Input.Infrastructure
{
    public sealed class InputActionGateway
    {
        private readonly InputActionMap _playerActionMap;
        private readonly InputAction _moveAction;

        public InputActionGateway(InputActionAsset actions)
        {
            if (actions == null)
            {
                throw new InvalidOperationException("InputActionGateway requires an InputActionAsset.");
            }

            _playerActionMap = actions.FindActionMap(InputContextNames.Player, throwIfNotFound: true);
            _moveAction = _playerActionMap.FindAction("Move", throwIfNotFound: true);
        }

        public void EnablePlayer()
        {
            _playerActionMap.Enable();
        }

        public void DisablePlayer()
        {
            _playerActionMap.Disable();
        }

        public Vector2 ReadMove()
        {
            return _moveAction.ReadValue<Vector2>();
        }
    }
}