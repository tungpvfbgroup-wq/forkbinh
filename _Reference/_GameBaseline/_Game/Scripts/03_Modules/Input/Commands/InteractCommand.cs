using BillGameCore.Core.ValueObjects;
using BillGameCore.SharedPorts.Input;

namespace BillGameCore.Modules.Input.Commands
{
    public sealed class InteractCommand : IInteractCommand
    {
        public InteractCommand(BillEntityId controlledEntityId)
        {
            ControlledEntityId = controlledEntityId;
        }

        public CommandType Type => CommandType.Interact;

        public BillEntityId ControlledEntityId { get; }
    }
}