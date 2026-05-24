using BillGameCore.Core.Interaction;
using UnityEngine;

namespace BillGameCore.Modules.InteractionGroup.Chest.Presentation
{
    public sealed class ChestBinder : MonoBehaviour, IInteractable
    {
        private bool _hasInteracted;

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
            Debug.Log("Chest interacted.", this);
        }
    }
}