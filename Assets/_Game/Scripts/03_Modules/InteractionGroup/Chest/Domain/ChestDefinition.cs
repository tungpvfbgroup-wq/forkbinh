namespace BillGameCore.Modules.InteractionGroup.Chest.Domain
{
    public sealed class ChestDefinition
    {
        public bool StartsOpened { get; }

        public ChestDefinition(bool startsOpened)
        {
            StartsOpened = startsOpened;
        }
    }
}