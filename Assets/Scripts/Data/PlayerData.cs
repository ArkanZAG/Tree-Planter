using System.Collections.Generic;

namespace Data
{
    public class PlayerData
    {
        public int oxygen;
        public string currentGridId;
        public Dictionary<string, GridData> grids;

        public PlayerData(int oxygen, string currentGridId, Dictionary<string, GridData> grids)
        {
            this.oxygen = oxygen;
            this.currentGridId = currentGridId;
            this.grids = grids;
        }
    }
}