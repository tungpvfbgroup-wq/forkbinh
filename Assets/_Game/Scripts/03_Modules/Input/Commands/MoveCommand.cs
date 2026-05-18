using BillGameCore.Core.ValueObjects;
using BillGameCore.SharedPorts.Input;

namespace BillGameCore.Modules.Input.Commands
{
    public readonly struct MoveCommand : IMoveCommand
    {
        public MoveCommand(EntityId controlledEntityId, float dirX, float dirY, bool isMoving, float timestamp)
        {
            ControlledEntityId = controlledEntityId;
            DirX = dirX;
            DirY = dirY;
            IsMoving = isMoving;
            Timestamp = timestamp;
        }

        public EntityId ControlledEntityId { get; }
        public CommandType Type => CommandType.Move;
        public float Timestamp { get; }

        public float DirX { get; }
        public float DirY { get; }
        public bool IsMoving { get; }
    }
}