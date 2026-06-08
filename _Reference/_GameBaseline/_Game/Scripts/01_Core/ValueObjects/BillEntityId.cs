using System;

namespace BillGameCore.Core.ValueObjects
{
    public readonly struct BillEntityId : IEquatable<BillEntityId>
    {
        private readonly Guid _value;

        private BillEntityId(Guid value)
        {
            _value = value;
        }

        public static BillEntityId Invalid => default;

        public bool IsValid => _value != Guid.Empty;

        public static BillEntityId New()
        {
            return new BillEntityId(Guid.NewGuid());
        }

        public bool Equals(BillEntityId other)
        {
            return _value.Equals(other._value);
        }

        public override bool Equals(object obj)
        {
            return obj is BillEntityId other && Equals(other);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public override string ToString()
        {
            if (!IsValid)
            {
                return "Invalid";
            }

            return _value.ToString("N")[..8];
        }

        public static bool operator ==(BillEntityId left, BillEntityId right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(BillEntityId left, BillEntityId right)
        {
            return !left.Equals(right);
        }
    }
}
