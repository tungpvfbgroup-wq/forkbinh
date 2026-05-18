using BillGameCore.Core.ValueObjects;
using BillGameCore.SharedPorts.Input;

namespace BillGameCore.Modules.Input.Commands
{
    public readonly struct SwitchContextCommand : ICommand
    {
        public SwitchContextCommand(EntityId controlledEntityId, InputContext nextContext, float timestamp)
        {
            ControlledEntityId = controlledEntityId;
            NextContext = nextContext;
            Timestamp = timestamp;
        }

        public EntityId ControlledEntityId { get; }
        public CommandType Type => CommandType.SwitchContext;
        public float Timestamp { get; }

        public InputContext NextContext { get; }
    }
}