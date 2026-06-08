using BillGameCore.Core.Inventory;

namespace BillGameCore.SharedPorts.Inventory
{
    public interface IInventoryWriteService
    {
        void AddItem(ItemStack itemStack);
        bool RemoveItem(string itemId, int amount);
    }
}