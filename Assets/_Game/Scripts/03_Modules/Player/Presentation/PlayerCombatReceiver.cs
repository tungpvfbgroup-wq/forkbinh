using System;
using BillGameCore.Core.Combat;
using UnityEngine;

namespace BillGameCore.Modules.Player.Presentation
{
    public sealed class PlayerCombatReceiver : MonoBehaviour, IDamageReceiver
    {
        private PlayerRuntime _runtime;

        public void Bind(PlayerRuntime runtime)
        {
            _runtime = runtime ?? throw new ArgumentNullException(nameof(runtime));
        }

        public DamageResult ReceiveDamage(DamageInfo damageInfo)
        {
            if (_runtime == null)
            {
                throw new InvalidOperationException("PlayerCombatReceiver has not been bound to a PlayerRuntime.");
            }

            return _runtime.ReceiveDamage(damageInfo);
        }
    }
}