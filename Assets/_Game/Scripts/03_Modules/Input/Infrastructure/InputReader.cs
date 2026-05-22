using BillGameCore.Modules.Input.Commands;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using BillGameCore.Core.ValueObjects;
using BillGameCore.Modules.Input.Context;
namespace BillGameCore.Modules.Input.Infrastructure
{
    public sealed class InputReader : MonoBehaviour
    {
        [SerializeField] private InputActionAsset _actions;

        //private InputActionMap _playerActionMap;
        //private InputAction _moveAction;
        private CommandBuffer _commandBuffer;
        private BillEntityId _controlledEntityId;
        private InputActionGateway _inputActionGateway;
        public void SetControlledEntity(BillEntityId entityId)
        {
            if (!entityId.IsValid)
            {
                throw new InvalidOperationException("InputReader requires a valid controlled entity id.");
            }

            _controlledEntityId = entityId;
        }
        private void Awake()
        {
            //ValidateConfiguration();
            //CacheActions();
            _inputActionGateway = EnsureGateway();
        }
        private InputActionGateway EnsureGateway()
        {
            if (_actions == null)
            {
                throw new InvalidOperationException("InputReader requires an InputActionAsset.");
            }

            _inputActionGateway ??= new InputActionGateway(_actions);
            return _inputActionGateway;
        }
        private void OnEnable()
        {
            _inputActionGateway.EnablePlayer();
        }

        private void OnDisable()
        {
            _inputActionGateway.DisablePlayer();
        }

        public void ValidateConfiguration()
        {
            //var playerActionMap = _actions.FindActionMap(InputContextNames.Player, throwIfNotFound: false);

            // if (playerActionMap == null)
            // {
            //    throw new InvalidOperationException("InputReader could not find action map 'Player'.");
            // }

            // var moveAction = playerActionMap.FindAction("Move", throwIfNotFound: false);

            // if (moveAction == null)
            // {
            //     throw new InvalidOperationException("InputReader could not find action 'Player/Move'.");
            // }
            _ = EnsureGateway();
        }
        public void SetCommandBuffer(CommandBuffer commandBuffer)
        {
            _commandBuffer = commandBuffer;
        }
        private void Update()
        {
            ReadPlayerMap();
        }
        private void ReadPlayerMap()
        { 
            if (_commandBuffer == null)
            {
                throw new InvalidOperationException("InputReader requires a CommandBuffer before Update runs.");
            }

            if (!_controlledEntityId.IsValid)
            {
                throw new InvalidOperationException("InputReader requires a controlled entity id before Update runs.");
            }

            var moveInput = _inputActionGateway.ReadMove();
            var dirX = moveInput.x;
            var dirY = moveInput.y;
            _commandBuffer.Enqueue(new MoveCommand(_controlledEntityId, dirX, dirY));
        }
        //public IMoveCommand ReadMoveCommand()
           // var moveInput = _moveAction.ReadValue<Vector2>();
          //  return new MoveCommand(moveInput.x, moveInput.y);

       // private void CacheActions()
       // {
           // _playerActionMap = _actions.FindActionMap(InputContextNames.Player, throwIfNotFound: true);
           // _moveAction = _playerActionMap.FindAction("Move", throwIfNotFound: true);
       // }
    }
}