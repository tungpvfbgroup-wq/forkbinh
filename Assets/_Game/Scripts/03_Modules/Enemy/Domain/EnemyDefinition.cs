namespace BillGameCore.Modules.Enemy.Domain
{
    public sealed class EnemyDefinition
    {
        public float MaxHealth { get; }
        public float AttackDamage { get; } 
        public float AttackCooldown { get; } 
        public int GoldReward { get; }
        public int ExperienceReward { get; }
        
        public EnemyDefinition(float maxHealth, float attackDamage, float attackCooldown,
            int goldReward, int experienceReward
            )
        {
            if (maxHealth <= 0f)
            {
                throw new System.ArgumentOutOfRangeException(nameof(maxHealth), "EnemyDefinition requires maxHealth > 0.");
            }

            if (attackDamage < 0f)
            {
                throw new System.ArgumentOutOfRangeException(nameof(attackDamage), "EnemyDefinition requires attackDamage >= 0.");
            }

            if (attackCooldown < 0f)
            {
                throw new System.ArgumentOutOfRangeException(nameof(attackCooldown), "EnemyDefinition requires attackCooldown >= 0.");
            }
            MaxHealth = maxHealth;
            AttackDamage = attackDamage;
            AttackCooldown = attackCooldown;
            GoldReward = goldReward;
            ExperienceReward = experienceReward;
        }
    }
}