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
        [SerializeField] private UpgradeUI upgradeUI;

        private void Awake()
        {
            plantUI.Show(false);
            upgradeUI.Show(false);
        }

        public void HideTray()
        {
            plantUI.Show(false);
            upgradeUI.Show(false);
        }

        public void ShowTray(Tile tile)
        {
            if (tile.CurrentPlant == null)
            {
                plantUI.SpawnElements(tile);
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