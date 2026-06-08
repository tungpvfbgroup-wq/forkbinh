using System.Collections.Generic;
using BillGameCore.Core.Combat;
using UnityEngine;

namespace BillGameCore.Modules.Enemy.Presentation
{
    public sealed class EnemyAttackSensor : MonoBehaviour
    {
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
            if (other.isTrigger)
            {
                return;
            }

            var target = other.GetComponent<IDamageReceiver>();

            if (target == null)
            {
                return;
            }

            if (_targets.Contains(target))
            {
                return;
            }

            _targets.Add(target);
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.isTrigger)
            {
                return;
            }

            var target = other.GetComponent<IDamageReceiver>();

            if (target == null)
            {
                return;
            }

            _targets.Remove(target);
        }
    }
}