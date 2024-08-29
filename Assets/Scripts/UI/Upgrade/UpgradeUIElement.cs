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
        [SerializeField] private UpgradeButton first, second, third;

        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private AudioClip audioClipUpgrade;

        
        private UpgradeDefinition upgradeDefinition;
        private GameController gameController;
        private SoundController soundController;

        public void Display(UpgradeDefinition upgrade, GameController game, SoundController soundCont)
        {
            upgradeDefinition = upgrade;
            gameController = game;
            soundController = soundCont;
            
            UpdateStatus();
        }
        
        public void UpdateStatus()
        {
            var currentLevel = upgradeDefinition.getCurrentLevel.Invoke();
            var maxLevel = upgradeDefinition.getMaxLevel.Invoke();
            var levelToMaxLevel = maxLevel - currentLevel;
            var lastLevel = Mathf.Min(50, Mathf.Clamp(levelToMaxLevel, 11, int.MaxValue));
            Debug.Log($"UPGRADE: {upgradeDefinition.title} LAST LEVEL IS {lastLevel}");
            
            titleText.text = $"{upgradeDefinition.title} Lv.{currentLevel}<size=60%> /{maxLevel}";

            first.Display(gameController, upgradeDefinition, 1, currentLevel, maxLevel, OnLevelUp);
            second.Display(gameController, upgradeDefinition, 10, currentLevel, maxLevel, OnLevelUp);
            third.Display(gameController, upgradeDefinition, lastLevel, currentLevel, maxLevel, OnLevelUp);
        }

        private void OnLevelUp(int levelAmount)
        {
            var cost = upgradeDefinition.getCost.Invoke(levelAmount);

            if (gameController.Oxygen < cost) return;
            
            soundController.PlaySfx(audioClipUpgrade);
            upgradeDefinition.OnUpgraded?.Invoke(levelAmount);
            gameController.AddOxygen(-cost);
            UpdateStatus();
        }
    }
}