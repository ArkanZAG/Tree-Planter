using Controller;
using UnityEngine;

namespace Achievements.Definitions
{
    [CreateAssetMenu(menuName = "Achievements/FinishLevels")]
    public class FinishLevels : AchievementDefinition
    {
        public override int GetProgress(GameController game, GridController grid)
        {
            return game.GetFinishedLevelCount();
        }
        
        public override string AchievementTitle => $"Finish {goals} Leves";
    }
}