using Controller;
using Data;
using UnityEngine;

namespace Achievements.Definitions
{
    [CreateAssetMenu(menuName = "Achievements/BiomeInSingleLevel")]
    public class BiomeInSingleLevel : AchievementDefinition
    {
        [SerializeField] private BiomeType biome;
        
        public override bool IsCompleted(GameController game, GridController grid)
        {
            return grid.GetPlantCount(biome) >= goals;
        }

        public override int GetProgress(GameController game, GridController grid)
        {
            return grid.GetPlantCount(biome);
        }

        public override string AchievementTitle => $"Plant {goals} on {biome}";
    }
}