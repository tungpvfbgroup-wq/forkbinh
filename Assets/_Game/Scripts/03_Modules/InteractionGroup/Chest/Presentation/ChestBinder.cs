using BillGameCore.Core.Interaction;
using UnityEngine;

namespace BillGameCore.Modules.InteractionGroup.Chest.Presentation
{
    public sealed class ChestBinder : MonoBehaviour, IInteractable
    {
        private ChestPresenter _presenter;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Collider2D _interactionTrigger;
        private void Awake()
        {
            _presenter = new ChestPresenter();
            _presenter.InteractedCallback = HandleInteracted;
        }
       
        public bool CanInteract()
        {
            return _presenter.CanInteract();
        }

        public void Interact()
        {
            _presenter.Interact();
        }
        private void HandleInteracted()
        {
            if (_interactionTrigger != null)
            {
                _interactionTrigger.enabled = false;
            }

            if (_spriteRenderer != null)
            {
                _spriteRenderer.color = Color.gray;
            }
            Debug.Log("Chest interacted.", this);
        }
    }
}