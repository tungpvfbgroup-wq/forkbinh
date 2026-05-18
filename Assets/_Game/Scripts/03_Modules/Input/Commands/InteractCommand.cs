using BillGameCore.Core.ValueObjects;
using BillGameCore.SharedPorts.Input;

namespace BillGameCore.Modules.Input.Commands
{
    public readonly struct InteractCommand : IInteractCommand
    {
        public InteractCommand(EntityId controlledEntityId, float timestamp)
        {
            ControlledEntityId = controlledEntityId;
            Timestamp = timestamp;
        }

        public EntityId ControlledEntityId { get; }
        public CommandType Type => CommandType.Interact;
        public float Timestamp { get; }
    }
}