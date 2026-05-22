using BillGameCore.Core.ValueObjects;
using BillGameCore.SharedPorts.Input;
namespace BillGameCore.Modules.Input.Commands
{
    public sealed class MoveCommand : IMoveCommand
    {
        public CommandType Type => CommandType.Move;
        public BillEntityId ControlledEntityId { get; }
        public MoveCommand(BillEntityId controlledEntityId,float dirX, float dirY)
        {
            DirX = dirX;
            DirY = dirY;
            ControlledEntityId = controlledEntityId;
        }
        
        public float DirX { get; }

        public float DirY { get; }
        

        public bool IsMoving => DirX != 0f || DirY != 0f;
    }
}