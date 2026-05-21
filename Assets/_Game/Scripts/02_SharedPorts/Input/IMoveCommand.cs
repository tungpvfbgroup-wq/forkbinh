namespace BillGameCore.SharedPorts.Input
{
    public interface IMoveCommand : ICommand
    {
        float X { get; }

        float Y { get; }

        bool IsMoving { get; }
    }
}