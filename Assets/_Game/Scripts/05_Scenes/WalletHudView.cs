using System;
using UnityEngine;
using TMPro;
namespace BillGameCore.Scenes
{
    public sealed class WalletHudView : MonoBehaviour
    {
        [SerializeField] private SceneBootstrapper _sceneBootstrapper;
        [SerializeField] private TMP_Text _goldText;

        private int _lastGold = int.MinValue;

        private void Awake()
        {
            if (_sceneBootstrapper == null)
            {
                throw new InvalidOperationException("WalletHudView requires a SceneBootstrapper reference.");
            }
            if (_goldText == null)
            {
                throw new InvalidOperationException("WalletHudView requires a TMP_Text reference.");
            }
            _lastGold = _sceneBootstrapper.CurrentGold; // Đọc ngay số vàng hiện tại (là 0)
            RefreshGoldText(_lastGold); // Ép ô chữ biến thành "Gold: 0" NGAY LẬP TỨC
        }

        private void RefreshGoldText(int gold)
        {
            _goldText.text = $"Gold: {gold}";// Toàn bộ game chỉ có đúng MỘT dòng này lo việc in chữ.
        }
        
        private void Update()
        {
            var currentGold = _sceneBootstrapper.CurrentGold;

            if (_lastGold == currentGold)
            {
                return;
            }

            _lastGold = currentGold;
            RefreshGoldText(currentGold);
        }
    }
}