using UnityEngine;

namespace BillGameCore.Modules.InteractionGroup.Chest.Presentation
{
    public sealed class ChestView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Collider2D _interactionTrigger;

        public void ShowOpenedState()
        {
            if (_interactionTrigger != null)
            {
                _interactionTrigger.enabled = false;
            }

            if (_spriteRenderer != null)
            {
                _spriteRenderer.color = Color.gray;
            }
        }
    }
}