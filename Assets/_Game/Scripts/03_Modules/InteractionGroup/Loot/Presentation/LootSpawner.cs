using System;
using BillGameCore.Core.Rewards;
using UnityEngine;

namespace BillGameCore.Modules.InteractionGroup.Loot.Presentation
{
    public sealed class LootSpawner
    {
        private readonly LootBinder _lootBinderPrefab;

        public LootSpawner(LootBinder lootBinderPrefab)
        {
            _lootBinderPrefab = lootBinderPrefab ?? throw new ArgumentNullException(nameof(lootBinderPrefab));
        }

        public LootBinder Spawn(Vector2 worldPosition, RewardBundle reward)
        {
            var lootBinder = UnityEngine.Object.Instantiate(_lootBinderPrefab, worldPosition, Quaternion.identity);
            lootBinder.Initialize(reward);
            return lootBinder;
        }
    }
}