using System;
using System.Collections.Generic;

namespace Data
{
    [Serializable]
    public class PlayerData
    {
        public int oxygen;
        public string currentGridId;
        public Dictionary<string, GridData> grids;
        public HashSet<string> completedAchievements; //HashSet is more efficient than List for unique values -arrakh

        public PlayerData(int oxygen, string currentGridId, Dictionary<string, GridData> grids, HashSet<string> completedAchievements)
        {
            this.oxygen = oxygen;
            this.currentGridId = currentGridId;
            this.grids = grids;
            this.completedAchievements = completedAchievements;
        }
    }
}