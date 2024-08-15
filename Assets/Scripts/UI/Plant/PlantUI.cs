using System;
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
        
        public void SpawnElements()
        {
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
                    treeBuffElement.Display(treeBuff);
                }
                else if (plant is TreePassive treePassive)
                {
                    var obj = Instantiate(elementTreePassivePrefab, parent);
                    var treePassiveElement = obj.GetComponent<PlantUIElementTreePassive>();
                    treePassiveElement.Display(treePassive);
                }
            }
        }

        public void Show(bool isShowing)
        {
            holder.SetActive(isShowing);
        }
    }
}