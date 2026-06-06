using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace BillGameCore.Scenes
{
    public sealed class PlayerDeathHudView : MonoBehaviour
    {
        [SerializeField] private GameObject _messageRoot;
        [SerializeField] private TMP_Text _messageText;
        [SerializeField] private Button _restartButton;
        public event Action RestartRequested;
        private void Awake()
        {
            if (_messageRoot == null)
            {
                throw new InvalidOperationException("PlayerDeathHudView requires a message root reference.");
            }

            if (_messageText == null)
            {
                throw new InvalidOperationException("PlayerDeathHudView requires a TMP_Text reference.");
            }
            if (_restartButton == null)
            {
                throw new InvalidOperationException("PlayerDeathHudView requires a restart button reference.");
            }

            Hide();
        }
        private void OnEnable()
        {
            _restartButton.onClick.AddListener(HandleRestartButtonClicked);
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveListener(HandleRestartButtonClicked);
        }
        public void ShowPlayerDied()
        {
            _messageText.text = "PLAYER DIED";
            _messageRoot.SetActive(true);
            var eventSystem = EventSystem.current;
            if (eventSystem != null)
            {
                eventSystem.SetSelectedGameObject(null);
                eventSystem.SetSelectedGameObject(_restartButton.gameObject);
            }
        }
        private void HandleRestartButtonClicked()
        {
            RestartRequested?.Invoke();
        }
        public void Hide()
        {
            _messageRoot.SetActive(false);
        }
    }
}