using System;
using UnityEngine;

namespace BillGameCore.Modules.Player.Presentation
{
    public sealed class PlayerView : MonoBehaviour
    {
       
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private PlayerAttackSensor _attackSensor;
        [SerializeField] private PlayerInteractSensor _interactSensor;
        [SerializeField] private Vector3 _attackSensorUpOffset = new Vector3(0f, 0.3f, 0f);
        [SerializeField] private Vector3 _attackSensorDownOffset = new Vector3(0f, -0.3f, 0f);
        [SerializeField] private Vector3 _attackSensorLeftOffset = new Vector3(-0.3f, 0f, 0f);
        [SerializeField] private Vector3 _attackSensorRightOffset = new Vector3(0.3f, 0f, 0f);

        public Vector2 WorldPosition => _rigidbody2D.position;
        public PlayerAttackSensor AttackSensor => _attackSensor;
        public PlayerInteractSensor InteractSensor => _interactSensor;

        public void SetAttackSensorDirection(Vector2 direction)
        {
            if (_attackSensor == null)
            {
                throw new InvalidOperationException("PlayerView requires a PlayerAttackSensor reference.");
            }

            var sensorTransform = _attackSensor.transform;

            if (direction.x > 0f)
            {
                sensorTransform.localPosition = _attackSensorRightOffset;
                return;
            }

            if (direction.x < 0f)
            {
                sensorTransform.localPosition = _attackSensorLeftOffset;
                return;
            }

            if (direction.y > 0f)
            {
                sensorTransform.localPosition = _attackSensorUpOffset;
                return;
            }

            if (direction.y < 0f)
            {
                sensorTransform.localPosition = _attackSensorDownOffset;
            }
        }

        public void SetMoveVelocity(Vector2 velocity)
        {
            if (_rigidbody2D == null)
            {
                return;
            }
            _rigidbody2D.linearVelocity = velocity;
        }
        public void SetDeadState()
        {
            if (_rigidbody2D == null)
            {
                return;
            }
            _rigidbody2D.linearVelocity = Vector2.zero;
            var bodyCollider = GetComponent<Collider2D>();
            if (bodyCollider != null)
            {
                bodyCollider.enabled = false;
            }
            var spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {  
                spriteRenderer.color = Color.gray;
            }
            var combatReceiver = GetComponent<PlayerCombatReceiver>();
            if (combatReceiver != null)
            {
                combatReceiver.enabled = false;
            }

            if (_attackSensor != null)
            {
                _attackSensor.gameObject.SetActive(false);
            }

            if (_interactSensor != null)
            {
                _interactSensor.gameObject.SetActive(false);
            }
        }
    }
}