using System;
using GridSystem;
using UI.Plant;
using Unity.VisualScripting;
using UnityEngine;

namespace Controller
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private PlantUI plantUI;

        private void Awake()
        {
            plantUI.Show(false);
        }

        public void HideTray()
        {
            plantUI.Show(false);
        }

        public void ShowTray(Tile tile)
        {
            if (tile.CurrentPlant == null)
            {
                plantUI.SpawnElements(tile);
                plantUI.Show(true);
            }
        }
    }
}