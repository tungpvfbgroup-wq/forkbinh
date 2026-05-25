using System;
using BillGameCore.Modules.InteractionGroup.Chest.Application;
namespace BillGameCore.Modules.InteractionGroup.Chest.Presentation
{
    public sealed class ChestPresenter
    {
        public bool HasInteracted => _application.HasInteracted;
        private readonly ChestApplication _application;
        public Action InteractedCallback { get; set; }
        public ChestPresenter(ChestApplication application)
        {
            _application = application;
        }
        public bool CanInteract()
        {
            return _application.CanInteract();
        }

        public void Interact()
        {
            if (!_application.TryInteract())
            {
                return;
            }
            InteractedCallback?.Invoke();
        }
    }
} 