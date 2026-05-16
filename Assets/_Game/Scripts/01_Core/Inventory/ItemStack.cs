using System;

namespace BillGameCore.Core.Inventory
{
    // Value object cho một loại item và số lượng của nó trong inventory/loot/reward.
    public readonly struct ItemStack : IEquatable<ItemStack>
    {
        public ItemStack(string itemId, int amount)
        {
            if (string.IsNullOrWhiteSpace(itemId))
            {
                throw new ArgumentException("Item id cannot be null or empty.", nameof(itemId));
            }

            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), amount, "Item amount must be greater than zero.");
            }

            ItemId = itemId;
            Amount = amount;
        }

        public string ItemId { get; }
        public int Amount { get; }

        public bool Equals(ItemStack other)
        {
            return ItemId == other.ItemId && Amount == other.Amount;
        }

        public override bool Equals(object obj)
        {
            return obj is ItemStack other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ItemId, Amount);
        }

        public override string ToString()
        {
            return $"{ItemId} x{Amount}";
        }

        public static bool operator ==(ItemStack left, ItemStack right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ItemStack left, ItemStack right)
        {
            return !left.Equals(right);
        }
    }
}