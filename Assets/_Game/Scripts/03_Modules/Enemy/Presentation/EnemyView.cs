using System;
using UnityEngine;
using System.Collections;
namespace BillGameCore.Modules.Enemy.Presentation
{
    public sealed class EnemyView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Color _aliveColor = Color.white;
        [SerializeField] private Color _hitColor = Color.red;
        [SerializeField] private float _hitFlashDuration = 0.08f;

        private Coroutine _hitFlashCoroutine;
        private void Awake()
        {
            if (_spriteRenderer == null)
            {
                throw new InvalidOperationException("EnemyView requires a SpriteRenderer reference.");
            }
            _spriteRenderer.color = _aliveColor;
        }
        public void ShowDeadState()
        {
           // _spriteRenderer.enabled = true;
            _spriteRenderer.color = Color.gray;
        }
        public void ShowAliveState()
        {
            _spriteRenderer.enabled = true;
            _spriteRenderer.color = _aliveColor;
        }
        public void ShowHitState()
        {
            if (_hitFlashCoroutine != null)
            {
                StopCoroutine(_hitFlashCoroutine);
            }

            _hitFlashCoroutine = StartCoroutine(PlayHitFlash());
        }
        private IEnumerator PlayHitFlash()
        {
            _spriteRenderer.color = _hitColor;
            yield return new WaitForSeconds(_hitFlashDuration);
            _spriteRenderer.color = _aliveColor;
            _hitFlashCoroutine = null;
        }
    }
}