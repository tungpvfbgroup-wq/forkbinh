using BillGameCore.SharedPorts.Economy;
using System;

namespace BillGameCore.Scenes
{
    public sealed class WalletService : IWalletService, IWalletWriteService
    {
        public event Action Changed;
        public int Gold { get; private set; }
        public int Experience { get; private set; }
        public void AddGold(int amount)
        {
            Gold += amount;
            Changed?.Invoke();
        }
        public void AddExperience(int amount)
        {
            Experience += amount;
            Changed?.Invoke();
        }
    }
}