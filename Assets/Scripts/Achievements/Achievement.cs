using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Achievements
{
    public static class Achievement
    {
        private static Dictionary<string, AchievementDefinition> _cache = new();

        public static void Initialize()
        {
            var achievements = Resources.LoadAll<AchievementDefinition>("Achievements");
            foreach (var achievement in achievements)
            {
                _cache[achievement.Id] = achievement;
            }
        }

        public static AchievementDefinition[] GetAll() => _cache.Values.ToArray();
    }
}