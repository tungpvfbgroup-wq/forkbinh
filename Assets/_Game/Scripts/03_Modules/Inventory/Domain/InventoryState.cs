using System;
using System.Collections.Generic;
using BillGameCore.Core.Inventory;

namespace BillGameCore.Modules.Inventory.Domain
{
    public sealed class InventoryState
    {
        private readonly List<ItemStack> _items = new();

        public IReadOnlyList<ItemStack> Items => _items;

        public bool HasItem(string itemId, int minAmount = 1)
        {
            if (string.IsNullOrWhiteSpace(itemId))
            {
                throw new ArgumentException("InventoryState requires a non-empty item id.", nameof(itemId));
            }

            if (minAmount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(minAmount), "InventoryState requires minAmount > 0.");
            }

            for (int i = 0; i < _items.Count; i++)
            {
                var item = _items[i];

                if (string.Equals(item.ItemId, itemId, StringComparison.Ordinal) &&
                    item.Amount >= minAmount)
                {
                    return true;
                }
            }

            return false;
        }

        public void AddItem(ItemStack itemStack)
        {
            if (!itemStack.IsValid)
            {
                throw new ArgumentException("InventoryState requires a valid item stack.", nameof(itemStack));
            }

            for (int i = 0; i < _items.Count; i++)
            {
                var current = _items[i];

                if (!string.Equals(current.ItemId, itemStack.ItemId, StringComparison.Ordinal))
                {
                    continue;
                }

                _items[i] = new ItemStack(current.ItemId, current.Amount + itemStack.Amount);
                return;
            }

            _items.Add(itemStack);
        }

        public bool RemoveItem(string itemId, int amount)
        {
            if (string.IsNullOrWhiteSpace(itemId))
            {
                throw new ArgumentException("InventoryState requires a non-empty item id.", nameof(itemId));
            }

            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "InventoryState requires amount > 0.");
            }

            for (int i = 0; i < _items.Count; i++)
            {
                var current = _items[i];

                if (!string.Equals(current.ItemId, itemId, StringComparison.Ordinal))
                {
                    continue;
                }

                if (current.Amount < amount)
                {
                    return false;
                }

                var remainingAmount = current.Amount - amount;

                if (remainingAmount == 0)
                {
                    _items.RemoveAt(i);
                    return true;
                }

                _items[i] = new ItemStack(current.ItemId, remainingAmount);
                return true;
            }

            return false;
        }
    }
}