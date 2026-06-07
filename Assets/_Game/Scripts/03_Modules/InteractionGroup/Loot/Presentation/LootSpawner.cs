using BillGameCore.Core.Inventory;
using BillGameCore.Core.Rewards;
using System;
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
            var lootBinder = SpawnBinder(worldPosition);
            lootBinder.Initialize(reward);
            return lootBinder;
        }
        public LootBinder Spawn(Vector2 worldPosition, ItemStack itemStack)
        {
            var lootBinder = SpawnBinder(worldPosition);
            lootBinder.Initialize(itemStack);
            return lootBinder;
        }
        private LootBinder SpawnBinder(Vector2 worldPosition)
        {
            return UnityEngine.Object.Instantiate(_lootBinderPrefab, worldPosition, Quaternion.identity);
        }
    }
}