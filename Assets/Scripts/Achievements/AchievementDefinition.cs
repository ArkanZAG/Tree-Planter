using System;
using Controller;
using UnityEngine;
using Utilities;

namespace Achievements
{
    public abstract class AchievementDefinition : ScriptableObject
    {
        [Header("Id below will be used to track completion. MUST BE UNIQUE")]
        [SerializeField] private string id;

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(id)) id = ShortUid.New();
        }

        public abstract bool IsCompleted(GameController game, GridController grid);
    }
}