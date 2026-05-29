using System;
using UnityEngine;
using TMPro;
namespace BillGameCore.Scenes
{
    public sealed class WalletDebugView : MonoBehaviour
    {
        [SerializeField] private SceneBootstrapper _sceneBootstrapper;
        [SerializeField] private TMP_Text _goldText;

        private int _lastGold = int.MinValue;

        private void Awake()
        {
            if (_sceneBootstrapper == null)
            {
                throw new InvalidOperationException("WalletDebugView requires a SceneBootstrapper reference.");
            }
            if (_goldText == null)
            {
                throw new InvalidOperationException("WalletDebugView requires a TMP_Text reference.");
            }
        }

        private void Update()
        {
            var currentGold = _sceneBootstrapper.CurrentGold;

            if (_lastGold == currentGold)
            {
                return;
            }

            _lastGold = currentGold;
            _goldText.text = $"Gold: {currentGold}";
            //Debug.Log($"Current Gold: {currentGold}", this);
        }
    }
}