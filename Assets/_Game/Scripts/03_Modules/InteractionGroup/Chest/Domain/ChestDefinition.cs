namespace BillGameCore.Modules.InteractionGroup.Chest.Domain
{
    public sealed class ChestDefinition
    {
        public bool StartsOpened { get; }
        public int GoldReward { get; }
        public int ExperienceReward {  get; }

        public ChestDefinition(bool startsOpened, int goldReward, int experienceReward)
        {
            StartsOpened = startsOpened;
            GoldReward = goldReward;
            ExperienceReward = experienceReward;
        }
    }
}