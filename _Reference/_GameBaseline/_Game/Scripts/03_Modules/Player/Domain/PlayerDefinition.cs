using System;

namespace BillGameCore.Modules.Player.Domain
{
    public sealed class PlayerDefinition
    {
        public float MaxHealth { get; }
        public float MoveSpeed { get; }
        public float AttackDamage { get; }
        public float AttackCooldown { get; }

        public PlayerDefinition(float maxHealth, float moveSpeed, float attackDamage, float attackCooldown)
        {
            if (maxHealth <= 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(maxHealth), "PlayerDefinition requires maxHealth > 0.");
            }
            if (moveSpeed <= 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(moveSpeed), "PlayerDefinition requires moveSpeed > 0.");
            }

            if (attackDamage < 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(attackDamage), "PlayerDefinition requires attackDamage >= 0.");
            }

            if (attackCooldown < 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(attackCooldown), "PlayerDefinition requires attackCooldown >= 0.");
            }

            MaxHealth = maxHealth;
            MoveSpeed = moveSpeed;
            AttackDamage = attackDamage;
            AttackCooldown = attackCooldown;
        }
    }
}