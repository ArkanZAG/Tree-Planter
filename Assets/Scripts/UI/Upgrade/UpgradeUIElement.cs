using System;
using Controller;
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
        private GameController gameController;

        private void Awake()
        {
            button.onClick.AddListener(OnClick);
        }

        public void Display(UpgradeDefinition upgrade, GameController game)
        {
            upgradeDefinition = upgrade;
            gameController = game;

            levelText.text = upgrade.getCurrentLevel.Invoke().ToString();
            priceText.text = upgrade.getCurrentCost.Invoke().ToString();
            titleText.text = upgrade.title;

        }

        private void OnClick()
        {
            var cost = upgradeDefinition.getCurrentCost.Invoke();
            
            if (gameController.Oxygen >= cost)
            {
                upgradeDefinition.OnUpgraded?.Invoke();
                Display(upgradeDefinition, gameController);
                gameController.AddOxygen(-cost);
            }
            
        }
    }
}