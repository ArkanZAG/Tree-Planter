using System;
using TMPro;
using UI.Animations;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Notice
{
    public class NoticePopup : MonoBehaviour
    {
        [SerializeField] private PopupAnimation popup;
        [SerializeField] private GameObject holder;
        [SerializeField] private TextMeshProUGUI noticeText;
        [SerializeField] private Button okButton, bgButton;

        private void Awake()
        {
            okButton.onClick.AddListener(OnClose);
            bgButton.onClick.AddListener(OnClose);
        }

        private void OnClose()
        {
            onPopupFinished?.Invoke();
            holder.SetActive(false);
        }

        private Action onPopupFinished;

        public void Display(string text, Action onClosed = null)
        {
            holder.SetActive(true);
            popup.StartAnimation();
            
            noticeText.text = text;

            onPopupFinished = onClosed;
        }
    }
}