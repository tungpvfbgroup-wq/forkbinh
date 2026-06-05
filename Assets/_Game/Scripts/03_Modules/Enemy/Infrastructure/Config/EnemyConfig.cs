using BillGameCore.Modules.Enemy.Domain;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "BillGameCore/Enemy/Enemy Config")]
public class EnemyConfig : ScriptableObject
{
    [field: SerializeField] public float MaxHealth { get; private set; } = 10f;
    [field: SerializeField] public float AttackDamage { get; private set; } = 1f;
    [field: SerializeField] public float AttackCooldown { get; private set; } = 1f;
    [field: SerializeField] public int GoldReward { get; private set; }
    [field: SerializeField] public int ExperienceReward { get; private set; }
    

    public EnemyDefinition ToDefinition()
    {
        return new EnemyDefinition(MaxHealth, AttackDamage, AttackCooldown, GoldReward, ExperienceReward);
    }

}
