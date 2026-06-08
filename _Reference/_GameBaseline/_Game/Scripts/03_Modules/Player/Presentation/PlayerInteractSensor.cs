using System;
using BillGameCore.Core.Interaction;
using UnityEngine;

namespace BillGameCore.Modules.Player.Presentation
{
    public sealed class PlayerInteractSensor : MonoBehaviour
    {
        private readonly Collider2D[] _overlapResults = new Collider2D[16];
        private Collider2D _sensorCollider;
        private void Awake()
        {
            _sensorCollider = GetComponent<Collider2D>();

            if (_sensorCollider == null)
            {
                throw new InvalidOperationException("PlayerInteractSensor requires a Collider2D on the same GameObject.");
            }
        }
        public IInteractable CurrentTarget
        {
            get
            {
                var filter = new ContactFilter2D();
                filter.useTriggers = true;
                filter.useLayerMask = false;
                filter.useDepth = false;
                filter.useNormalAngle = false;

                int count = Physics2D.OverlapCollider(_sensorCollider, filter, _overlapResults);
                IInteractable nearestTarget = null;
                float nearestDistanceSqr = float.MaxValue;

                for (int i = 0; i < count; i++)
                {
                    var hit = _overlapResults[i];
                    if (hit == null)
                    {
                        continue;
                    }
                    if (!hit.isTrigger)
                    {
                        continue;
                    }
                    var target = hit.GetComponentInParent<IInteractable>();
                    if (target == null)
                    {
                        continue;
                    }

                    if (!target.CanInteract())
                    {
                        continue;
                    }
                    if (target is not Component targetComponent)
                    {
                        continue;
                    }

                    float distanceSqr = (targetComponent.transform.position - transform.position).sqrMagnitude;

                    if (distanceSqr < nearestDistanceSqr)
                    {
                        nearestDistanceSqr = distanceSqr;
                        nearestTarget = target;
                    }
                }
                return nearestTarget;
            }
        }
    }
}