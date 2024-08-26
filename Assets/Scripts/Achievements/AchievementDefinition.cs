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
        [SerializeField] private int oxygenReward;
        [SerializeField] protected int goals;

        public string Id => id;
        public virtual string AchievementTitle { get; }
        public int Goals => goals;
        public int OxygenReward => oxygenReward;

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(id)) id = ShortUid.New();
        }
        
        public abstract int GetProgress(GameController game, GridController grid);
    }
}