using System;
using Controller;
using Plants;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Upgrade
{
    public class UpgradeButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI priceText, levelIndicator;
        [SerializeField] private Button button;
        [SerializeField] private GameObject buttonGroup;

        private Action<int> onButtonClicked;
        private int level;
        private GameController gameController;
        private int cost;
        private bool isMax;

        private void Awake()
        {
            button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            onButtonClicked?.Invoke(level);
        }

        public void Display(GameController game, UpgradeDefinition definition, int levelAmount, int currentLevel, int maxLevel, Action<int> onClicked)
        {
            level = levelAmount;
            levelIndicator.text = $"+{this.level} Lvl";
            cost = definition.getCost.Invoke(levelAmount);
            gameController = game;

            bool shouldShow = currentLevel + levelAmount < maxLevel;

            buttonGroup.gameObject.SetActive(shouldShow);

            isMax = currentLevel >= maxLevel;

            if (isMax)
            {
                priceText.text = "MAX";
                return;
            }

            priceText.text = cost.ToString();

            onButtonClicked = onClicked;
        }

        private void Update()
        {
            bool canAfford = gameController.Oxygen >= cost;
            bool canUpgrade = canAfford && !isMax;
            button.interactable = canUpgrade;

            //only show button when you have oxygen that is half the cost. 
            /*bool shouldShow = gameController.Oxygen >= cost * 0.5f;
            buttonGroup.gameObject.SetActive(shouldShow);*/
        }
    }
}