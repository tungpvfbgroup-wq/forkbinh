namespace BillGameCore.Modules.InteractionGroup.Loot.Domain
{
    public sealed class LootState
    {
        public bool IsCollected { get; private set; }

        public void MarkCollected()
        {
            IsCollected = true;
        }
    }
}