using BillGameCore.Core.ValueObjects;
using BillGameCore.SharedPorts.Input;

namespace BillGameCore.Modules.Input.Commands
{
    public readonly struct AttackCommand : IAttackCommand
    {
        public AttackCommand(EntityId controlledEntityId, bool isHeld, float heldDuration, float timestamp)
        {
            ControlledEntityId = controlledEntityId;
            IsHeld = isHeld;
            HeldDuration = heldDuration;
            Timestamp = timestamp;
        }

        public EntityId ControlledEntityId { get; }
        public CommandType Type => CommandType.Attack;
        public float Timestamp { get; }

        public bool IsHeld { get; }
        public float HeldDuration { get; }
    }
}