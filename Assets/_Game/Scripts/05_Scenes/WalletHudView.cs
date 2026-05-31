using System;
using UnityEngine;
using TMPro;
namespace BillGameCore.Scenes
{
    public sealed class WalletHudView : MonoBehaviour
    {
        [SerializeField] private WalletReadSource _walletReadSource;
        [SerializeField] private TMP_Text _goldText;
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
            RefreshGoldText(); // Ép ô chữ biến thành "Gold: 0" NGAY LẬP TỨC
        }
        private void OnEnable()
        {
            _walletReadSource.Changed += HandleWalletChanged;
        }
        private void OnDisable()
        {
            _walletReadSource.Changed -= HandleWalletChanged;
        }
        private void HandleWalletChanged()
        {
            RefreshGoldText();
        }
        private void RefreshGoldText()
        {// Toàn bộ game chỉ có đúng MỘT dòng này lo việc in chữ.
            _goldText.text = $"Gold: {_walletReadSource.CurrentGold}\nExp: {_walletReadSource.CurrentExperience}";
        }
    }
}