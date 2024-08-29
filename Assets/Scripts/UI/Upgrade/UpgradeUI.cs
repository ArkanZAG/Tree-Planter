using System;
using System.Collections.Generic;
using Controller;
using GridSystem;
using Plants;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.Upgrade
{
    public class UpgradeUI : MonoBehaviour
    {
        [SerializeField] private GameObject upgradeElementUIPrefabs;
        [SerializeField] private Button removeButton;
        [SerializeField] private GameObject holder;
        [SerializeField] private Transform parent;
        [SerializeField] private UIController uiController;
        [SerializeField] private GameController gameController;
        [SerializeField] private SoundController soundController;
        [SerializeField] private AudioClip audioClipRemovePlant;

        private List<GameObject> spawnedElement = new();

        private Tile tile;

        private bool isUIActive;

        private void Start()
        {
            removeButton.onClick.AddListener(RemovePlant);
        }

        public void Display(Tile clickedTile)
        {
            if (tile != null && tile.Equals(clickedTile)) return;
            tile = clickedTile;
            
            ClearElements();
            foreach (var upgradeDefintion in tile.CurrentPlant.GetUpgrades())
            {
                var obj = Instantiate(upgradeElementUIPrefabs, parent);
                var uiUpgradeElement = obj.GetComponent<UpgradeUIElement>();
                uiUpgradeElement.Display(upgradeDefintion, gameController, soundController);
                spawnedElement.Add(obj);
            }
        }

        public void RemovePlant()
        {
            soundController.PlaySfx(audioClipRemovePlant);
            tile.RemovePlant();
            uiController.HideTray();
        }
        
        public void Show(bool isShowing)
        {
            isUIActive = isShowing;
            holder.SetActive(isShowing);
        }

        public void ClearElements()
        {
            for (int i = 0; i < spawnedElement.Count; i++)
            {
                Destroy(spawnedElement[i]);
            }
            spawnedElement.Clear();
        }
    }
}