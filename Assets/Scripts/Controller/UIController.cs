﻿using System;
using GridSystem;
using UI.Plant;
using UI.Upgrade;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Controller
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private PlantUI plantUI;
        [SerializeField] private UpgradeUI upgradeUI;
        [SerializeField] private AchievementUI achievementUI;
        [SerializeField] private ArchiveUI archiveUI;
        [SerializeField] private CameraController cameraController;

        [SerializeField] private Button achievementsButton;
        [SerializeField] private Button archiveButton;

        private void Awake()
        {
            DisableAllUI();
            achievementsButton.onClick.AddListener(achievementUI.Display);
            archiveButton.onClick.AddListener(archiveUI.Display);
        }

        private void DisableAllUI()
        {
            plantUI.Show(false);
            upgradeUI.Show(false);
            achievementUI.Show(false);
            archiveUI.Show(false);
            cameraController.SetIsUIOffset(false);
        }

        public void HideTray()
        {
            DisableAllUI();
        }

        public void ShowTray(Tile tile)
        {
            DisableAllUI();
            
            cameraController.SetIsUIOffset(true);
            
            if (tile.CurrentPlant == null)
            {
                plantUI.SpawnElements(tile);
                plantUI.SpawnBiomeElement();
                plantUI.Show(true);
            }
            else
            {
                upgradeUI.Display(tile);
                upgradeUI.Show(true);
            }
        }
    }
}