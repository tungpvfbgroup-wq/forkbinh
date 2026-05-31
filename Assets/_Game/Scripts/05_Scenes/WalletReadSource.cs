using BillGameCore.SharedPorts.Economy;
using System;
using UnityEngine;
namespace BillGameCore.Scenes
{
    public sealed class WalletReadSource : MonoBehaviour
    {
        private IWalletService _walletService;
        public event Action Changed;
        public int CurrentGold => _walletService == null ? 0 : _walletService.Gold;
        public int CurrentExperience => _walletService == null ? 0 : _walletService.Experience;
        public void SetWalletService(IWalletService walletService)
        {
            if (_walletService != null)
            {
                _walletService.Changed -= HandleWalletChanged;
            }
            _walletService = walletService ?? throw new ArgumentNullException(nameof(walletService));
            _walletService.Changed += HandleWalletChanged;
            Changed?.Invoke();
        }
        private void HandleWalletChanged()
        {
            Changed?.Invoke();
        }
    }
}