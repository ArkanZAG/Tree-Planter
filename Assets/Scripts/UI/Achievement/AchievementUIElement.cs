using System;
using System.Collections;
using System.Collections.Generic;
using Achievements;
using Controller;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AchievementUIElement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI achievementTitle;
    [SerializeField] private TextMeshProUGUI progressionText;
    [SerializeField] private TextMeshProUGUI rewardText;
    [SerializeField] private Slider slider;
    [SerializeField] private Button button;
    
    [SerializeField] private int currentProgressionNumber;

    private AchievementDefinition achievementDefinition;
    private GameController gameController;

    private void Awake()
    {
        button.onClick.AddListener(OnClick);
        button.interactable = false;
    }

    public void Display(AchievementDefinition achievementDef, GameController gameCont, GridController grid)
    {
        gameController = gameCont;
        achievementDefinition = achievementDef;
        achievementTitle.text = achievementDef.AchievementTitle;
        var goal = achievementDef.Goals;
        progressionText.text = $" {achievementDef.GetProgress(gameCont,grid)} / {goal}";
        rewardText.text = achievementDef.OxygenReward.ToString();
    }

    public void OnClick()
    {
        if (currentProgressionNumber <= achievementDefinition.Goals)
        {
            gameController.AddOxygen(achievementDefinition.OxygenReward);
        }
    }

    private void Update()
    {
        if (currentProgressionNumber <= achievementDefinition.Goals)
        {
            button.interactable = true;
        }
    }
}
