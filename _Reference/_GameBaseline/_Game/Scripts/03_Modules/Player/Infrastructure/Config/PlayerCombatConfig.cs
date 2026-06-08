using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCombatConfig", 
    menuName = "BillGameCore/Player/PlayerCombatConfig")]
public class PlayerCombatConfig : ScriptableObject
{
    [field: SerializeField] public float AttackDamage;
    [field: SerializeField] public float AttackCooldown;
}
