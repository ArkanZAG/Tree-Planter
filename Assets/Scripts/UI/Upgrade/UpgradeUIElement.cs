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
        [SerializeField] private AudioClip audioClip;

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

            levelText.text = upgrade.getCurrentLevel.Invoke().ToString();
            priceText.text = upgrade.getCurrentCost.Invoke().ToString();
            titleText.text = upgrade.title;

        }

        private void OnClick()
        {
            var cost = upgradeDefinition.getCurrentCost.Invoke();
            
            if (gameController.Oxygen >= cost)
            {
                soundController.PlaySfx(audioClip);
                upgradeDefinition.OnUpgraded?.Invoke();
                Display(upgradeDefinition, gameController,soundController);
                gameController.AddOxygen(-cost);
            }
            
        }
    }
}