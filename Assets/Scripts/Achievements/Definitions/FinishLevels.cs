using Controller;
using UnityEngine;

namespace Achievements.Definitions
{
    [CreateAssetMenu(menuName = "Achievements/FinishLevels")]
    public class FinishLevels : AchievementDefinition
    {
        [SerializeField] private int levelCount;

        public override bool IsCompleted(GameController game, GridController grid)
        {
            return game.GetFinishedLevelCount() >= levelCount;
        }
    }
}