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
    private GridController gridController;

    private void Awake()
    {
        button.onClick.AddListener(OnClick);
        button.interactable = false;
    }

    public void Display(AchievementDefinition achievementDef, GameController gameCont, GridController grid)
    {
        gameController = gameCont;
        achievementDefinition = achievementDef;
        gridController = grid;
        achievementTitle.text = achievementDef.AchievementTitle;
        rewardText.text = achievementDef.OxygenReward.ToString();
        UpdateStatus();
        grid.OnTilesUpdated += OnTilesUpdated;
    }
    private void OnTilesUpdated()
    {
        UpdateStatus();
    }
    private void UpdateStatus()
    {
        var goal = achievementDefinition.Goals;
        float progress = achievementDefinition.GetProgress(gameController, gridController);
        progressionText.text = $" {progress} / {goal}";
        slider.value = progress / goal;
        button.interactable = progress >= goal;
        if (gameController.HasClaimedAchievement(achievementDefinition.Id))
        {
            rewardText.text = "Completed";
            button.interactable = false;
        }
    }

    private void OnDestroy()
    {
        gridController.OnTilesUpdated -= OnTilesUpdated;
    }

    public void OnClick()
    {
        gameController.AddOxygen(achievementDefinition.OxygenReward);
        gameController.ClaimAchievement(achievementDefinition.Id);
        UpdateStatus();
    }
}
