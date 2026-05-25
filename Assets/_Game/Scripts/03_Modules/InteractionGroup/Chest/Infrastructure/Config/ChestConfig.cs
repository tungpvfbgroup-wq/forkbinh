using UnityEngine;
using BillGameCore.Modules.InteractionGroup.Chest.Domain;
namespace BillGameCore.Modules.InteractionGroup.Chest.Infrastructure.Config
{
    [CreateAssetMenu(
        fileName = "ChestConfig",
        menuName = "BillGameCore/InteractionGroup/Chest Config")]
    public sealed class ChestConfig : ScriptableObject
    {
        [field: SerializeField] public bool StartsOpened { get; private set; }
        [field: SerializeField] public int GoldReward { get; private set; }
        public ChestDefinition ToDefinition()
        {
            return new ChestDefinition(StartsOpened, GoldReward);
        }
    }
 }