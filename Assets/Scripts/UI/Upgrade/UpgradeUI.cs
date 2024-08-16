using System;
using System.Collections.Generic;
using Controller;
using GridSystem;
using Plants;
using UnityEngine;

namespace UI.Upgrade
{
    public class UpgradeUI : MonoBehaviour
    {
        [SerializeField] private GameObject upgradeElementUIPrefabs;
        [SerializeField] private GameObject holder;
        [SerializeField] private Transform parent;

        private List<GameObject> spawnedElement = new();

        public void Display(Tile tile)
        {
            ClearElements();
            foreach (var upgradeDefintion in tile.CurrentPlant.GetUpgrades())
            {
                var obj = Instantiate(upgradeElementUIPrefabs, parent);
                var uiUpgradeElement = obj.GetComponent<UpgradeUIElement>();
                uiUpgradeElement.Display(upgradeDefintion);
                spawnedElement.Add(obj);
            }
            
        }
        public void Show(bool isShowing)
        {
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