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
    }
}