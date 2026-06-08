namespace BillGameCore.Modules.Enemy.Application
{
    public readonly struct EnemyHealthReadModel
    {
        public float CurrentHealth { get; }
        public bool IsDead { get; }

        public EnemyHealthReadModel(float currentHealth, bool isDead)
        {
            CurrentHealth = currentHealth;
            IsDead = isDead;
        }
    }
}