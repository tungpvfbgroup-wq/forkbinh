namespace BillGameCore.SharedPorts.Input
{
    public interface IMoveCommand
    {
        float X { get; }

        float Y { get; }

        bool IsMoving { get; }
    }
}