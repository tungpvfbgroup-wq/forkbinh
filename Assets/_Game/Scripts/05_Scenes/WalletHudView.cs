using System;
using UnityEngine;
using TMPro;
namespace BillGameCore.Scenes
{
    public sealed class WalletHudView : MonoBehaviour
    {
        [SerializeField] private WalletReadSource _walletReadSource;
        [SerializeField] private TMP_Text _goldText;

        private int _lastGold = int.MinValue;
        private int _lastExperience = int.MinValue;
        private void Awake()
        {
            if (_walletReadSource == null)
            {
                throw new InvalidOperationException("WalletHudView requires a WalletReadSource reference.");
            }
            if (_goldText == null)
            {
                throw new InvalidOperationException("WalletHudView requires a TMP_Text reference.");
            }
            _lastGold = _walletReadSource.CurrentGold; // Đọc ngay số vàng hiện tại (là 0)
            _lastExperience = _walletReadSource.CurrentExperience;
            RefreshGoldText(); // Ép ô chữ biến thành "Gold: 0" NGAY LẬP TỨC
        }

        private void RefreshGoldText()
        {// Toàn bộ game chỉ có đúng MỘT dòng này lo việc in chữ.
            _goldText.text = $"Gold: {_walletReadSource.CurrentGold}\nExp: {_walletReadSource.CurrentExperience}";
        }

        private void Update()
        {
            var currentGold = _walletReadSource.CurrentGold;
            var currentExperience = _walletReadSource.CurrentExperience;
            if (_lastGold == currentGold && _lastExperience == currentExperience)
            {
                return;
            }

            _lastGold = currentGold;
            _lastExperience = currentExperience;
            RefreshGoldText();
        }
    }
}