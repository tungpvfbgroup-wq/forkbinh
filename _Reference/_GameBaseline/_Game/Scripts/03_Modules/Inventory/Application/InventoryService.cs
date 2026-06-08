using System;
using System.Collections.Generic;
using BillGameCore.Core.Inventory;
using BillGameCore.Modules.Inventory.Domain;
using BillGameCore.SharedPorts.Inventory;

namespace BillGameCore.Modules.Inventory.Application
{
    public sealed class InventoryService : IInventoryReadService, IInventoryWriteService
    { 
        
        private readonly InventoryState _state = new();
        public event Action Changed;
        public IReadOnlyList<ItemStack> GetItems()
        {
            if (_state.Items.Count == 0)
            {
                return Array.Empty<ItemStack>();
            }

            var snapshot = new ItemStack[_state.Items.Count];

            for (int i = 0; i < _state.Items.Count; i++)
            {
                snapshot[i] = _state.Items[i];
            }

            return snapshot;
        }

        public bool HasItem(string itemId, int minAmount = 1)
        {
            return _state.HasItem(itemId, minAmount);
        }

        public void AddItem(ItemStack itemStack)
        {
            _state.AddItem(itemStack);
            Changed?.Invoke();
        }

        public bool RemoveItem(string itemId, int amount)
        {
            var wasRemoved = _state.RemoveItem(itemId, amount);

            if (wasRemoved)
            {
                Changed?.Invoke();
            }

            return wasRemoved;
        }
    }
}