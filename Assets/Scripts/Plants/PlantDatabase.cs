using System;
using System.Collections.Generic;
using UnityEngine;

namespace Plants
{
    [CreateAssetMenu(menuName = "Plant Database")] 
    public class PlantDatabase : ScriptableObject
    {
        [SerializeField] private GameObject[] plantPrefabs;
        private Dictionary<string, GameObject> mappedPrefabs = new();
        private bool initialize;

        public GameObject[] PlantPrefabs => plantPrefabs;

        public void Initialize()
        {
            foreach (var prefab in plantPrefabs)
            {
                var iPlant = prefab.GetComponent<IPlant>();
                if (iPlant == null)
                {
                    throw new Exception($"Trying to initialize Plant Database but {prefab.name} is not IPlant");
                }
                mappedPrefabs[iPlant.Id] = prefab;
            }
        }

        public GameObject GetPrefab(string id)
        {
            return mappedPrefabs[id];
        }
    }
}