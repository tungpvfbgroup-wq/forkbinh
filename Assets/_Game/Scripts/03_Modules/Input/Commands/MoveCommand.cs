using BillGameCore.SharedPorts.Input;
namespace BillGameCore.Modules.Input.Commands
{
    public sealed class MoveCommand : IMoveCommand
    {
        public CommandType Type => CommandType.Move;
        public MoveCommand(float x, float y)
        {
            X = x;
            Y = y;
        }
        
        public float X { get; }

        public float Y { get; }

        public bool IsMoving => X != 0f || Y != 0f;
    }
}