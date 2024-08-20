using Controller;
using UnityEngine;

namespace Achievements.Definitions
{
    [CreateAssetMenu(menuName = "Achievements/PlantTrees")]
    public class PlantTrees : AchievementDefinition
    {
        [SerializeField] private int plantTreeCount;
        
        public override bool IsCompleted(GameController game, GridController grid)
        {
            return game.GetTotalPlantCount() >= plantTreeCount;
        }
    }
}