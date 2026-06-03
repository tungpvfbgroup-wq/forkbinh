using System.Collections.Generic;
using BillGameCore.Core.Interaction;
using UnityEngine;

namespace BillGameCore.Modules.Player.Presentation
{
    public sealed class PlayerInteractSensor : MonoBehaviour
    {
        private readonly List<IInteractable> _targets = new();

        public IInteractable CurrentTarget
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

                    if (!target.CanInteract())
                    {
                        _targets.RemoveAt(i);
                        continue;
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
            var interactable = other.GetComponent<IInteractable>();

            if (interactable == null)
            {
                return;
            }

            if (_targets.Contains(interactable))
            {
                return;
            }

            _targets.Add(interactable);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            var interactable = other.GetComponent<IInteractable>();

            if (interactable == null)
            {
                return;
            }

            _targets.Remove(interactable);
        }
    }
}