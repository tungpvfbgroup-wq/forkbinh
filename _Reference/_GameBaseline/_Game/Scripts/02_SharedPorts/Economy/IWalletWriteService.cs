namespace BillGameCore.SharedPorts.Economy
{
    public interface IWalletWriteService
    {
        void AddGold(int amount);
        void AddExperience(int amount);
    }
}