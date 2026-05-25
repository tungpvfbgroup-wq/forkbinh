namespace BillGameCore.Modules.InteractionGroup.Chest.Domain
{
    public sealed class ChestDefinition
    {
        public bool StartsOpened { get; }
        public int GoldReward { get; }

        public ChestDefinition(bool startsOpened, int goldReward)
        {
            StartsOpened = startsOpened;
            GoldReward = goldReward;
        }
    }
}