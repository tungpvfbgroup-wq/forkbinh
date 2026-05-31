namespace BillGameCore.Modules.Enemy.Domain
{
    public sealed class EnemyDefinition
    {
        public float MaxHealth { get; }
        public int GoldReward { get; }
        public int ExperienceReward { get; }

        public EnemyDefinition(float maxHealth, int goldReward, int experienceReward)
        {
            MaxHealth = maxHealth;
            GoldReward = goldReward;
            ExperienceReward = experienceReward;
        }
    }
}