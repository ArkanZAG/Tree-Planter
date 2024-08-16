using System;
using System.Collections.Generic;
using Controller;
using GridSystem;
using Plants;
using UnityEngine;

namespace UI.Plant
{
    public class PlantUI : MonoBehaviour
    {
        [SerializeField] private GameObject elementTreeBuffPrefab;
        [SerializeField] private GameObject elementTreePassivePrefab;
        [SerializeField] private GameObject holder;

        [SerializeField] private Transform parent;

        [SerializeField] private PlantDatabase plantDatabase;

        [SerializeField] private GridController gridController;
        [SerializeField] private UIController uiController;
        [SerializeField] private GameController gameController;

        private List<GameObject> spawnedElement = new();
        
        public void SpawnElements(Tile tile)
        {
            ClearElement();
            for (int i = 0; i < plantDatabase.PlantPrefabs.Length; i++)
            {
                var plant = plantDatabase.PlantPrefabs[i].GetComponent<IPlant>();
                if (plant == null)
                {
                    throw new Exception("IPlant Not Found");
                }

                if (plant is TreeBuff treeBuff)
                {
                    var obj = Instantiate(elementTreeBuffPrefab, parent);
                    var treeBuffElement = obj.GetComponent<PlantUIElementTreeBuff>();
                    treeBuffElement.Display(treeBuff, uiController, gridController, tile, gameController);
                    spawnedElement.Add(obj);
                }
                else if (plant is TreePassive treePassive)
                {
                    var obj = Instantiate(elementTreePassivePrefab, parent);
                    var treePassiveElement = obj.GetComponent<PlantUIElementTreePassive>();
                    treePassiveElement.Display(treePassive, uiController, gridController, tile, gameController);
                    spawnedElement.Add(obj);
                }
            }
        }

        public void Show(bool isShowing)
        {
            holder.SetActive(isShowing);
        }

        private void ClearElement()
        {
            for (int i = 0; i < spawnedElement.Count; i++)
            {
                Destroy(spawnedElement[i]);
            }
            spawnedElement.Clear();
        }
    }
}