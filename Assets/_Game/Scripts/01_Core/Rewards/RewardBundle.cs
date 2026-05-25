namespace BillGameCore.Core.Rewards
{
    public readonly struct RewardBundle
    {
        public int Gold { get; }

        public RewardBundle(int gold)
        {
            Gold = gold;
        }
    }
}