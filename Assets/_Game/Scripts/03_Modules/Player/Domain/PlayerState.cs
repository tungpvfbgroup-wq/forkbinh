namespace BillGameCore.Modules.Player.Domain
{
    public sealed class PlayerState
    {
        public PlayerState(float initialHealth)
        {  if (initialHealth <= 0f)
            {
                throw new System.ArgumentOutOfRangeException(nameof(initialHealth), "PlayerState requires initialHealth > 0.");
            }
            CurrentHealth = initialHealth;
        }
        public float MoveVelocityX { get; private set; }
        public float MoveVelocityY { get; private set; }
        public float CurrentHealth { get; private set; }
        public bool IsDead {  get; private set; }
        public bool IsMoving =>
            (MoveVelocityX * MoveVelocityX) + (MoveVelocityY * MoveVelocityY) > 0f;
        public void SetMoveVelocity(float velocityX, float velocityY)
        {
            MoveVelocityX = velocityX;
            MoveVelocityY = velocityY;
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