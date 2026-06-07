using System;
using System.Collections.Generic;
using BillGameCore.Core.Inventory;

namespace BillGameCore.SharedPorts.Inventory
{
    public interface IInventoryReadService
    {
        event Action Changed;
        IReadOnlyList<ItemStack> GetItems();
        bool HasItem(string itemId, int minAmount = 1);
    }
}