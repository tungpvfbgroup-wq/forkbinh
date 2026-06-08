using System;
using System.Collections.Generic;
using BillGameCore.Core.Inventory;
using BillGameCore.SharedPorts.Inventory;
using UnityEngine;

namespace BillGameCore.Scenes
{
    public sealed class InventoryReadSource : MonoBehaviour
    {
        private IInventoryReadService _inventoryReadService;

        public event Action Changed;

        public IReadOnlyList<ItemStack> CurrentItems =>
            _inventoryReadService == null
                ? Array.Empty<ItemStack>()
                : _inventoryReadService.GetItems();

        public void SetInventoryReadService(IInventoryReadService inventoryReadService)
        {
            if (_inventoryReadService != null)
            {
                _inventoryReadService.Changed -= HandleInventoryChanged;
            }

            _inventoryReadService = inventoryReadService ?? throw new ArgumentNullException(nameof(inventoryReadService));
            _inventoryReadService.Changed += HandleInventoryChanged;
            Changed?.Invoke();
        }

        private void HandleInventoryChanged()
        {
            Changed?.Invoke();
        }
    }
}