using System;
using Controller;
using Plants;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.Upgrade
{
    public class UpgradeUIElement : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private AudioClip audioClipUpgrade;

        [SerializeField] private Button button;
        
        private UpgradeDefinition upgradeDefinition;
        private GameController gameController;
        private SoundController soundController;

        private void Awake()
        {
            button.onClick.AddListener(OnClick);
        }

        public void Display(UpgradeDefinition upgrade, GameController game, SoundController soundCont)
        {
            upgradeDefinition = upgrade;
            gameController = game;
            soundController = soundCont;

            var currentLevel = upgrade.getCurrentLevel.Invoke();
            var maxLevel = upgrade.getMaxLevel.Invoke();


            levelText.text = $"{currentLevel} / {maxLevel}";
            priceText.text = upgrade.getCurrentCost.Invoke().ToString();
            titleText.text = upgrade.title;
            
            UpdateStatus();
        }
        
        public void UpdateStatus()
        {
            var currentLevel = upgradeDefinition.getCurrentLevel.Invoke();
            var maxLevel = upgradeDefinition.getMaxLevel.Invoke();
            
            if (currentLevel >= maxLevel)
            {
                priceText.text = "MAX";
                button.interactable = false;
            }
        }

        private void OnClick()
        {
            var cost = upgradeDefinition.getCurrentCost.Invoke();
            UpdateStatus();
            
            if (gameController.Oxygen >= cost)
            {
                soundController.PlaySfx(audioClipUpgrade);
                upgradeDefinition.OnUpgraded?.Invoke();
                Display(upgradeDefinition, gameController,soundController);
                soundController.PlaySfx(audioClipUpgrade);
                gameController.AddOxygen(-cost);
            }
            
        }
    }
}