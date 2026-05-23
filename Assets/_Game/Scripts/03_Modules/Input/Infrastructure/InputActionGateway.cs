using System;
using BillGameCore.Modules.Input.Context;
using BillGameCore.SharedPorts.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BillGameCore.Modules.Input.Infrastructure
{
    public sealed class InputActionGateway
    {
        private readonly InputActionMap _playerActionMap;
        private readonly InputAction _moveAction;
        public InputContext CurrentContext { get; private set; }
        public InputActionGateway(InputActionAsset actions)
        {
            if (actions == null)
            {
                throw new InvalidOperationException("InputActionGateway requires an InputActionAsset.");
            }

            _playerActionMap = actions.FindActionMap(InputContextNames.Player, throwIfNotFound: true);
            _moveAction = _playerActionMap.FindAction("Move", throwIfNotFound: true);

            CurrentContext = InputContext.Player;
        }
        public void SetContext(InputContext context)
        {
            switch (context)
            {
                case InputContext.Player:
                    CurrentContext = InputContext.Player;
                    return;

                case InputContext.UI:
                case InputContext.Vehicle:
                default:
                    throw new InvalidOperationException(
                        $"InputActionGateway does not support context '{context}'.");
            }
        }
        public void EnableCurrentContext()
        {
            GetCurrentActionMap().Enable();
        }

        public void DisableCurrentContext()
        {
            GetCurrentActionMap().Disable();
        }
        private InputActionMap GetCurrentActionMap()
        {
            return CurrentContext switch
            {
                InputContext.Player => _playerActionMap,
                InputContext.UI => throw new InvalidOperationException(
                    $"InputActionGateway does not support context '{CurrentContext}'."),
                InputContext.Vehicle => throw new InvalidOperationException(
                    $"InputActionGateway does not support context '{CurrentContext}'."),
                _ => throw new InvalidOperationException(
                    $"InputActionGateway does not support context '{CurrentContext}'.")
            };
        }
        public Vector2 ReadMove()
        {
            EnsurePlayerContext();
            return _moveAction.ReadValue<Vector2>();
        }
        private void EnsurePlayerContext()
        {
            if (CurrentContext != InputContext.Player)
            {
                throw new InvalidOperationException(
                    $"InputActionGateway requires Player context to read move, but current context is '{CurrentContext}'.");
            }
        }
    }
}