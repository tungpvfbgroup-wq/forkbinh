using System;
using System.Text;
using TMPro;
using UnityEngine;

namespace BillGameCore.Scenes
{
    public sealed class InventoryHudView : MonoBehaviour
    {
        [SerializeField] private InventoryReadSource _inventoryReadSource;
        [SerializeField] private TMP_Text _itemsText;

        private void Awake()
        {
            if (_inventoryReadSource == null)
            {
                throw new InvalidOperationException("InventoryHudView requires an InventoryReadSource reference.");
            }

            if (_itemsText == null)
            {
                throw new InvalidOperationException("InventoryHudView requires a TMP_Text reference.");
            }

            RefreshItemsText();
        }

        private void OnEnable()
        {
            _inventoryReadSource.Changed += HandleInventoryChanged;
        }

        private void OnDisable()
        {
            _inventoryReadSource.Changed -= HandleInventoryChanged;
        }

        private void HandleInventoryChanged()
        {
            RefreshItemsText();
        }

        private void RefreshItemsText()
        {
            var items = _inventoryReadSource.CurrentItems;

            if (items.Count == 0)
            {
                _itemsText.text = "Items:\n-";
                return;
            }

            var builder = new StringBuilder();
            builder.Append("Items:");

            for (int i = 0; i < items.Count; i++)
            {
                builder.Append('\n');
                builder.Append(items[i].ItemId);
                builder.Append(" x");
                builder.Append(items[i].Amount);
            }

            _itemsText.text = builder.ToString();
        }
    }
}