namespace BillGameCore.Modules.InteractionGroup.Chest.Domain
{
    public sealed class ChestState
    {
        public bool HasInteracted { get; private set; }
        public ChestState(ChestDefinition definition)
        {
            HasInteracted = definition.StartsOpened;
        }
        public void MarkInteracted()
        {
            HasInteracted = true;
        }
    }
}