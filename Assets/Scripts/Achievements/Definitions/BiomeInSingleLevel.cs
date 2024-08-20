using Controller;
using Data;
using UnityEngine;

namespace Achievements.Definitions
{
    public class BiomeInSingleLevel : AchievementDefinition
    {
        [SerializeField] private BiomeType biome;
        [SerializeField] private int plantCount;
        
        public override bool IsCompleted(GameController game, GridController grid)
        {
            return grid.GetPlantCount(biome) >= plantCount;
        }
    }
}