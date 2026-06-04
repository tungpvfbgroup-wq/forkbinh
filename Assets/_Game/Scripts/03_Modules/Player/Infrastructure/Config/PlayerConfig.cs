using BillGameCore.Modules.Player.Domain;
using UnityEngine;

namespace BillGameCore.Modules.Player.Infrastructure.Config
{
    [CreateAssetMenu(
        fileName = "PlayerConfig",
        menuName = "BillGameCore/Player/PlayerConfig")]
    public sealed class PlayerConfig : ScriptableObject
    {
        [field: SerializeField, Min(0f)]
        public float MoveSpeed { get; private set; } = 5f;

        [field: SerializeField, Min(0f)]
        public float AttackDamage { get; private set; } = 1f;

        [field: SerializeField, Min(0f)]
        public float AttackCooldown { get; private set; } = 0.2f;

        public PlayerDefinition ToDefinition()
        {
            return new PlayerDefinition(MoveSpeed, AttackDamage, AttackCooldown);
        }
    }
}