using BillGameCore.Core.Interaction;
using System;
using UnityEngine;
using BillGameCore.Modules.InteractionGroup.Chest.Domain;
using BillGameCore.Modules.InteractionGroup.Chest.Infrastructure.Config;
using BillGameCore.Modules.InteractionGroup.Chest.Application;
namespace BillGameCore.Modules.InteractionGroup.Chest.Presentation
{
    public sealed class ChestBinder : MonoBehaviour, IInteractable
    {
        public Action<ChestOpenResult> OpenedCallback { get; set; }
        private ChestPresenter _presenter;
        [SerializeField] private ChestView _view;
        [SerializeField] private ChestConfig _config;
        private void Awake()
        {
            ValidateConfiguration();
            _presenter = BuildChestPresenter();
            ApplyInitialViewState();
        }
        private void ApplyInitialViewState()
        {
            EnsureViewReady();

            if (GetRequiredPresenter().HasInteracted)
            {
                _view.ShowOpenedState();
            }
        }
        private ChestPresenter BuildChestPresenter()
        {
            var definition = BuildDefinition();
            var state = new ChestState(definition);
            var application = new ChestApplication(state, definition);
            var presenter = new ChestPresenter(application);
            presenter.OpenedCallback = HandleOpened;
            return presenter;
        }
        private ChestDefinition BuildDefinition()
        {
            return _config.ToDefinition();
        }
        private void ValidateConfiguration()
        {
            EnsureViewReady();
            if (_config == null)
            {
                throw new InvalidOperationException("ChestBinder requires a ChestConfig reference.");
            }
        }
        private ChestPresenter GetRequiredPresenter()
        {
            if (_presenter == null)
            {
                throw new InvalidOperationException("ChestBinder is not initialized.");
            }

            return _presenter;
        }
        private void EnsureViewReady()
        {
            if (_view == null)
            {
                throw new InvalidOperationException("ChestBinder requires a ChestView reference.");
            }
        }
        public bool CanInteract()
        {
            return GetRequiredPresenter().CanInteract();
        }

        public void Interact()
        {
            GetRequiredPresenter().Interact();
        }
        private void HandleOpened(ChestOpenResult result)
        {
            EnsureViewReady();
            _view.ShowOpenedState();
            OpenedCallback?.Invoke(result);
        }
        
    }
}