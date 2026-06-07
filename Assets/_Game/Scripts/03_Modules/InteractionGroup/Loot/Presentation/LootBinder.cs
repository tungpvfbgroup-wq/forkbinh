using BillGameCore.Core.Interaction;
using BillGameCore.Core.Inventory;
using BillGameCore.Core.Rewards;
using BillGameCore.Modules.InteractionGroup.Loot.Application;
using BillGameCore.Modules.InteractionGroup.Loot.Domain;
using System;
using UnityEngine;

namespace BillGameCore.Modules.InteractionGroup.Loot.Presentation
{
    public sealed class LootBinder : MonoBehaviour, IInteractable
    {
        [SerializeField] private LootView _view;
        [SerializeField] private Collider2D _targetCollider;

        private LootPresenter _presenter;

        public Action<LootCollectResult> CollectedCallback { get; set; }

        private void Awake()
        {
            ValidateConfiguration();
            _view.ShowCollectedState();
            _targetCollider.enabled = false;
        }

       
        public void Initialize(RewardBundle reward)
        {
            InitializeDefinition(new LootDefinition(reward));
        }
        public void Initialize(ItemStack itemStack)
        {
            InitializeDefinition(new LootDefinition(itemStack));
        }
        private void InitializeDefinition(LootDefinition definition)
        {
            if (_presenter != null)
            {
                throw new InvalidOperationException("LootBinder is already initialized.");
            }

            var state = new LootState();
            var application = new LootApplication(state, definition);
            var presenter = new LootPresenter(application);
            presenter.CollectedCallback = HandleCollected;

            _presenter = presenter;
            _view.ShowAvailableState();
            _targetCollider.enabled = true;
        }
        public bool CanInteract()
        {
            return GetRequiredPresenter().CanInteract();
        }

        public void Interact()
        {
            GetRequiredPresenter().Interact();
        }

        private void HandleCollected(LootCollectResult result)
        {
            _view.ShowCollectedState();
            _targetCollider.enabled = false;
            CollectedCallback?.Invoke(result);
            Destroy(gameObject);
        }

        private LootPresenter GetRequiredPresenter()
        {
            if (_presenter == null)
            {
                throw new InvalidOperationException("LootBinder has not been initialized.");
            }

            return _presenter;
        }

        private void ValidateConfiguration()
        {
            if (_view == null)
            {
                throw new InvalidOperationException("LootBinder requires a LootView reference.");
            }

            if (_targetCollider == null)
            {
                throw new InvalidOperationException("LootBinder requires a Collider2D reference.");
            }
            if (!_targetCollider.isTrigger)
            {
                throw new InvalidOperationException("LootBinder requires a trigger Collider2D reference.");
            }
        }
    }
}