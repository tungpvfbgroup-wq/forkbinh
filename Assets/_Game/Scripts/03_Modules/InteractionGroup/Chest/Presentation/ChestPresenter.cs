using System;

namespace BillGameCore.Modules.InteractionGroup.Chest.Presentation
{
    public sealed class ChestPresenter
    {
        private bool _hasInteracted;

        public Action InteractedCallback { get; set; }

        public bool CanInteract()
        {
            return !_hasInteracted;
        }

        public void Interact()
        {
            if (_hasInteracted)
            {
                return;
            }

            _hasInteracted = true;
            InteractedCallback?.Invoke();
        }
    }
} 