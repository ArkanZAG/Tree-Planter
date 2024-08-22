using System;
using GridSystem;
using UI.Plant;
using UI.Upgrade;
using Unity.VisualScripting;
using UnityEngine;

namespace Controller
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private PlantUI plantUI;
        [SerializeField] private PlantUI biomeUI;
        [SerializeField] private UpgradeUI upgradeUI;

        private void Awake()
        {
            DisableAllUI();
        }

        private void DisableAllUI()
        {
            plantUI.Show(false);
            upgradeUI.Show(false);
        }

        public void HideTray()
        {
            DisableAllUI();
        }

        public void ShowTray(Tile tile)
        {
            DisableAllUI();
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