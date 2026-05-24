using System;
using UnityEngine;

namespace BillGameCore.Modules.Player.Presentation
{
    public sealed class PlayerView : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        public Action<Collider2D> TriggerEnteredCallback { get; set; }
        public Action<Collider2D> TriggerExitedCallback { get; set; }
        public Vector2 WorldPosition => _rigidbody2D.position;
        private void OnTriggerEnter2D(Collider2D other)
        {
            TriggerEnteredCallback?.Invoke(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            TriggerExitedCallback?.Invoke(other);
        }
        public void SetMoveVelocity(Vector2 velocity)
        {
            _rigidbody2D.linearVelocity = velocity;
        }
    }
}