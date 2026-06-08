using BillGameCore.Core.ValueObjects;
using BillGameCore.SharedPorts.Input;

namespace BillGameCore.Modules.Input.Commands
{
    public sealed class AttackCommand : IAttackCommand
    {
        public AttackCommand(BillEntityId controlledEntityId, bool isHeld, float heldDuration)
        {
            ControlledEntityId = controlledEntityId;
            IsHeld = isHeld;
            HeldDuration = heldDuration;
        }

        public CommandType Type => CommandType.Attack;

        public BillEntityId ControlledEntityId { get; }

        public bool IsHeld { get; }

        public float HeldDuration { get; }
    }
}