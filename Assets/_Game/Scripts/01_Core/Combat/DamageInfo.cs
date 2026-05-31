using BillGameCore.Core.ValueObjects;

namespace BillGameCore.Core.Combat
{
    public readonly struct DamageInfo
    {
        public float Amount { get; }
        public BillEntityId SourceId { get; }
        public bool IsCritical { get; }

        public DamageInfo(float amount, BillEntityId sourceId, bool isCritical)
        {
            Amount = amount;
            SourceId = sourceId;
            IsCritical = isCritical;
        }
    }
}