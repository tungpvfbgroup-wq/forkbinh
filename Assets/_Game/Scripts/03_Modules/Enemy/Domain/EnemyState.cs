namespace BillGameCore.Modules.Enemy.Domain
{
    public sealed class EnemyState
    {
        public float CurrentHealth { get; private set; }
        public bool IsDead { get; private set; }

        public EnemyState(EnemyDefinition definition)
        {
            CurrentHealth = definition.MaxHealth;
            IsDead = false;
        }

        public void SetCurrentHealth(float currentHealth)
        {
            CurrentHealth = currentHealth;
        }

        public void MarkDead()
        {
            IsDead = true;
        }
    }
}