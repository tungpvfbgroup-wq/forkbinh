using BillGameCore.Core.Combat;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
namespace BillGameCore.Modules.Player.Presentation
{
    public sealed class PlayerAttackSensor : MonoBehaviour
    {
        // public IDamageReceiver CurrentTarget { get; private set; }
        private readonly List<IDamageReceiver> _targets = new();

        public IDamageReceiver CurrentTarget
        {
            get
            {
                for (int i = _targets.Count - 1; i >= 0; i--)
                {
                    var target = _targets[i];

                    if (target == null)
                    {
                        _targets.RemoveAt(i);
                        continue;
                    }

                    if (target is Behaviour behaviour)
                    {
                        if (!behaviour.isActiveAndEnabled)
                        {
                            _targets.RemoveAt(i);
                            continue;
                        }

                        var collider = behaviour.GetComponent<Collider2D>();

                        if (collider != null && !collider.enabled)
                        {
                            _targets.RemoveAt(i);
                            continue;
                        }
                    }
                }

                if (_targets.Count == 0)
                {
                    return null;
                }

                return _targets[0];
            }
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            var damageReceiver = other.GetComponent<IDamageReceiver>();

            if (damageReceiver == null)
            {
                return;
            }
            if (_targets.Contains(damageReceiver))
            {
                return;
            }

            _targets.Add(damageReceiver);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            var damageReceiver = other.GetComponent<IDamageReceiver>();

            if (damageReceiver == null)
            {
                return;
            }

            _targets.Remove(damageReceiver);
        }
    }
}