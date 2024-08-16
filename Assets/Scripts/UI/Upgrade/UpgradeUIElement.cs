using System;
using Plants;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Upgrade
{
    public class UpgradeUIElement : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private TextMeshProUGUI titleText;

        [SerializeField] private Button button;
        
        private UpgradeDefinition upgradeDefinition;

        private void Awake()
        {
            button.onClick.AddListener(OnClick);
        }

        public void Display(UpgradeDefinition upgrade)
        {
            upgradeDefinition = upgrade;

            levelText.text = upgrade.currentLevel.ToString();
            priceText.text = upgrade.cost.ToString();
            titleText.text = upgrade.title;

        }

        private void OnClick()
        {
            upgradeDefinition.OnUpgraded?.Invoke();
        }
    }
}