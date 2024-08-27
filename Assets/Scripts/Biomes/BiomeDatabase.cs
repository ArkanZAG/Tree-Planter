using System;
using System.Collections.Generic;
using UnityEngine;

namespace Biomes
{
    [CreateAssetMenu (fileName = "Biome Database", menuName = "Biome Database")] 
    public class BiomeDatabase : ScriptableObject
    {
        [SerializeField] private BiomeData[] biomeData;

        private Dictionary<BiomeType, BiomeData> dict = new();

        public void Initialize()
        {
            foreach (var data in biomeData)
            {
                if (dict.ContainsKey(data.Biome))
                    throw new Exception($"BIOME DATABASE ERROR, DATA FOR {data.Biome} ALREADY EXIST");
                
                dict[data.Biome] = data;
            }
        }

        public BiomeData Get(BiomeType type) => dict[type];
    }
}