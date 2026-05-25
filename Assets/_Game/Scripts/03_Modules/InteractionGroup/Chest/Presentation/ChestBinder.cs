using BillGameCore.Core.Interaction;
using System;
using UnityEngine;
namespace BillGameCore.Modules.InteractionGroup.Chest.Presentation
{
    public sealed class ChestBinder : MonoBehaviour, IInteractable
    {
        private ChestPresenter _presenter;
        [SerializeField] private ChestView _view;
        private void Awake()
        {
            ValidateConfiguration();
            _presenter = BuildPresenter();
        }
        private ChestPresenter BuildPresenter()
        {
            var presenter = new ChestPresenter();
            presenter.InteractedCallback = HandleInteracted;
            return presenter;
        }
        private void ValidateConfiguration()
        {
            EnsureViewReady();
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
        private void HandleInteracted()
        {
            EnsureViewReady();
            _view.ShowOpenedState();
        }
        
    }
}