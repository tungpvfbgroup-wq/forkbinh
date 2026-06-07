using BillGameCore.Core.Inventory;
using System;

namespace BillGameCore.Modules.Enemy.Domain
{
    public sealed class EnemyDefinition
    {
        public float MaxHealth { get; }
        public float AttackDamage { get; } 
        public float AttackCooldown { get; } 
        public int GoldReward { get; }
        public int ExperienceReward { get; }
        public ItemStack ItemDrop { get; }
        public bool HasItemDrop => ItemDrop.IsValid;
        public EnemyDefinition(
            float maxHealth,
            float attackDamage,
            float attackCooldown,
            int goldReward,
            int experienceReward,
            string droppedItemId,
            int droppedItemAmount)
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
            if (droppedItemAmount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(droppedItemAmount), "EnemyDefinition requires droppedItemAmount >= 0.");
            }

            var hasItemId = !string.IsNullOrWhiteSpace(droppedItemId);

            if (!hasItemId && droppedItemAmount > 0)
            {
                throw new ArgumentException("EnemyDefinition requires a non-empty droppedItemId when droppedItemAmount > 0.", nameof(droppedItemId));
            }

            if (hasItemId && droppedItemAmount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(droppedItemAmount), "EnemyDefinition requires droppedItemAmount > 0 when droppedItemId is set.");
            }
            MaxHealth = maxHealth;
            AttackDamage = attackDamage;
            AttackCooldown = attackCooldown;
            GoldReward = goldReward;
            ExperienceReward = experienceReward;
            ItemDrop = hasItemId
    ? new ItemStack(droppedItemId, droppedItemAmount)
    : default;
        }
    }
}