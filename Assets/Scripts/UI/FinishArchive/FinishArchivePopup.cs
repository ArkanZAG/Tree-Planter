using System;
using TMPro;
using UI.Animations;
using UnityEngine;
using UnityEngine.UI;

namespace UI.FinishArchive
{
    public class FinishArchivePopup : MonoBehaviour
    {
        [SerializeField] private Button doneButton;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private PopupAnimation popupAnimation;
        [SerializeField] private GameObject holder;

        private void Awake()
        {
            doneButton.onClick.AddListener(OnSubmit);
            holder.SetActive(false);
        }

        private void OnSubmit()
        {
            onPopupDone?.Invoke(inputField.text);
            holder.SetActive(false);
        }

        private void Update()
        {
            doneButton.interactable = CanSubmit();
        }

        private bool CanSubmit()
        {
            return !string.IsNullOrEmpty(inputField.text);
        }

        private Action<string> onPopupDone;
        
        public void Display(Action<string> onDone)
        {
            holder.SetActive(true);
            popupAnimation.StartAnimation();
            onPopupDone = onDone;
        }
    }
}