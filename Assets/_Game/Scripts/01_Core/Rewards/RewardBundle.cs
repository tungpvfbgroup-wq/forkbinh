namespace BillGameCore.Core.Rewards
{
    public readonly struct RewardBundle
    {
        public int Gold { get; }
        public int Experience { get; }
        public RewardBundle(int gold, int experience)
        {
            Gold = gold;
            Experience = experience;
        }
    }
}