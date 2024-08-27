using System;
using UnityEngine;

namespace Biomes
{
    [Serializable]
    public class BiomeData
    {
        [SerializeField] private BiomeType biome;
        [SerializeField] private Sprite biomeIcon;
        [SerializeField] private Gradient tileGradient;

        public Sprite BiomeIcon => biomeIcon;

        public Gradient TileGradient => tileGradient;

        public BiomeType Biome => biome;
    }
}