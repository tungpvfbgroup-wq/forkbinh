using BillGameCore.Core.Inventory;
using BillGameCore.Core.ValueObjects;

namespace BillGameCore.SharedPorts.Messages
{
    // Publish bởi loot/pickup flow sau khi nhặt item thành công.
    public readonly struct ItemPickedUpMessage
    {
        public ItemPickedUpMessage(EntityId pickerId, ItemStack stack)
        {
            PickerId = pickerId;
            Stack = stack;
        }

        public EntityId PickerId { get; }
        public ItemStack Stack { get; }
    }
}