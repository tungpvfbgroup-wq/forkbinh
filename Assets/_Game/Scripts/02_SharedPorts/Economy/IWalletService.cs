using System;

namespace BillGameCore.SharedPorts.Economy
{
    public interface IWalletService
    {
        event Action Changed;
        int Gold { get; }
        int Experience { get; }
    }
}