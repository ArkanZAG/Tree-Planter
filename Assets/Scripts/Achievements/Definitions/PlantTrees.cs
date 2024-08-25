using Controller;
using UnityEngine;

namespace Achievements.Definitions
{
    [CreateAssetMenu(menuName = "Achievements/PlantTrees")]
    public class PlantTrees : AchievementDefinition
    {
        public override bool IsCompleted(GameController game, GridController grid)
        {
            return game.GetTotalPlantCount() >= goals;
        }

        public override int GetProgress(GameController game, GridController grid)
        {
            return game.GetTotalPlantCount();
        }

        public override string AchievementTitle => $"Plant {goals} Trees";
    }
}