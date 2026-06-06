using System;
using UnityEngine;

namespace BillGameCore.Modules.InteractionGroup.Loot.Presentation
{
    public sealed class LootView : MonoBehaviour
    {
        [SerializeField] private GameObject _availableRoot;

        private void Awake()
        {
            if (_availableRoot == null)
            {
                throw new InvalidOperationException("LootView requires an available root reference.");
            }
        }

        public void ShowAvailableState()
        {
            _availableRoot.SetActive(true);
        }

        public void ShowCollectedState()
        {
            _availableRoot.SetActive(false);
        }
    }
}