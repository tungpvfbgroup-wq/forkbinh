using System;

namespace BillGameCore.Core.Inventory
{
    public readonly struct ItemStack : IEquatable<ItemStack>
    {
        public string ItemId { get; }
        public int Amount { get; }

        public bool IsValid =>
            !string.IsNullOrWhiteSpace(ItemId) &&
            Amount > 0;

        public ItemStack(string itemId, int amount)
        {
            if (string.IsNullOrWhiteSpace(itemId))
            {
                throw new ArgumentException("ItemStack requires a non-empty item id.", nameof(itemId));
            }

            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "ItemStack requires amount > 0.");
            }

            ItemId = itemId;
            Amount = amount;
        }

        public bool Equals(ItemStack other)
        {
            return string.Equals(ItemId, other.ItemId, StringComparison.Ordinal) &&
                   Amount == other.Amount;
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