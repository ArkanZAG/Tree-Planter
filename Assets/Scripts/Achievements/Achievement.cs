using System.Collections.Generic;
using UnityEngine;

namespace Achievements
{
    public static class Achievement
    {
        private static Dictionary<string, AchievementDefinition> _cache = new();

        public static void Initialize()
        {
            var achievements = Resources.LoadAll<AchievementDefinition>("Achievements");
        }
    }
}