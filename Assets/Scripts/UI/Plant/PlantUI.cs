using System;
using System.Collections.Generic;
using Biomes;
using Controller;
using Data;
using GridSystem;
using Plants;
using Unity.VisualScripting;
using UnityEngine;

namespace UI.Plant
{
    public class PlantUI : MonoBehaviour
    {
        [SerializeField] private GameObject elementTreeBuffPrefab;
        [SerializeField] private GameObject elementTreePassivePrefab;
        [SerializeField] private GameObject elementBiome;
        [SerializeField] private GameObject holder;

        [SerializeField] private Transform parent;
        [SerializeField] private Transform biomeParent;

        [SerializeField] private PlantDatabase plantDatabase;
        [SerializeField] private BiomeDatabase biomeDatabase;

        [SerializeField] private GridController gridController;
        [SerializeField] private UIController uiController;
        [SerializeField] private GameController gameController;
        [SerializeField] private SoundController soundController;

        private BiomeType currentBiome = BiomeType.Forest;
        private Tile currentTile;

        private List<GameObject> spawnedElement = new();
        private List<BiomeUIElement> spawnedBiomeElement = new();

        public void SetBiome(BiomeType biome)
        {
            currentBiome = biome;

            foreach (var element in spawnedBiomeElement)
            {
                element.SetHighlighted(currentBiome == element.Biome);
            }
        }

        public void SpawnElements(Tile tile)
        {
            currentTile = tile;
            ClearElement();
            
            for (int i = 0; i < plantDatabase.PlantPrefabs.Length; i++)
            {
                var plant = plantDatabase.PlantPrefabs[i].GetComponent<IPlant>();
                
                if (plant == null)
                {
                    throw new Exception("IPlant Not Found");
                }
                
                if (plant.Biome != currentBiome) continue;
                
                if (plant is TreeBuff treeBuff)
                {
                    var obj = Instantiate(elementTreeBuffPrefab, parent);
                    var treeBuffElement = obj.GetComponent<PlantUIElementTreeBuff>();
                    treeBuffElement.Display(treeBuff, uiController, gridController, tile, gameController, soundController);
                    spawnedElement.Add(obj);
                }
                else if (plant is TreePassive treePassive)
                {
                    var obj = Instantiate(elementTreePassivePrefab, parent);
                    var treePassiveElement = obj.GetComponent<PlantUIElementTreePassive>();
                    treePassiveElement.Display(treePassive, uiController, gridController, tile, gameController, this, soundController);
                    spawnedElement.Add(obj);
                }
            }
        }

        public void SpawnBiomeElement()
        {
            ClearBiomeElements();
            
            foreach (var biome in Enum.GetValues(typeof(BiomeType)))
            {
                if (biome is not BiomeType biomeType) continue;
                if (biomeType == BiomeType.None) continue;
                var obj = Instantiate(elementBiome, biomeParent);
                var biomeElement = obj.GetComponent<BiomeUIElement>();
                
                var data = biomeDatabase.Get(biomeType);
                biomeElement.Display(biomeType, data, this, currentTile);
                biomeElement.SetHighlighted(currentBiome == biomeElement.Biome);

                spawnedBiomeElement.Add(biomeElement);
            }
        }

        private void ClearBiomeElements()
        {
            for (int i = 0; i < spawnedBiomeElement.Count; i++)
            {
                Destroy(spawnedBiomeElement[i].gameObject);
            }
            spawnedBiomeElement.Clear();
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