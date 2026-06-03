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

        public Action<Collider2D> TriggerEnteredCallback { get; set; }
        public Action<Collider2D> TriggerExitedCallback { get; set; }
        public Vector2 WorldPosition => _rigidbody2D.position;
        public PlayerAttackSensor AttackSensor => _attackSensor;
        public PlayerInteractSensor InteractSensor => _interactSensor;
        private void OnTriggerEnter2D(Collider2D other)
        {
            TriggerEnteredCallback?.Invoke(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            TriggerExitedCallback?.Invoke(other);
        }

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
            _rigidbody2D.linearVelocity = velocity;
        }
    }
}