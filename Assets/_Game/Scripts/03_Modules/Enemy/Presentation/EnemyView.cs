using System;
using UnityEngine;

namespace BillGameCore.Modules.Enemy.Presentation
{
    public sealed class EnemyView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private void Awake()
        {
            if (_spriteRenderer == null)
            {
                throw new InvalidOperationException("EnemyView requires a SpriteRenderer reference.");
            }
        }
        public void ShowDeadState()
        {
            _spriteRenderer.enabled = false;
        }
        public void ShowAliveState()
        {
            _spriteRenderer.enabled = true;
        }
    }
}