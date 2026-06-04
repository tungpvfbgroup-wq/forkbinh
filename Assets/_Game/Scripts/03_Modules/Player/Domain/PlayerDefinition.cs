using System;

namespace BillGameCore.Modules.Player.Domain
{
    public sealed class PlayerDefinition
    {
        public float MoveSpeed { get; }
        public float AttackDamage { get; }
        public float AttackCooldown { get; }

        public PlayerDefinition(float moveSpeed, float attackDamage, float attackCooldown)
        {
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

            MoveSpeed = moveSpeed;
            AttackDamage = attackDamage;
            AttackCooldown = attackCooldown;
        }
    }
}