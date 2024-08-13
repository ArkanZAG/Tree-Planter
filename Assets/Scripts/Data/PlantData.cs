using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.Universal;

namespace Data
{
    public class PlantData
    {
        public int x;
        public int y;
        public string id;
        public Dictionary<string, int> data;

        public PlantData(int x, int y, string id)
        {
            this.x = x;
            this.y = y;
            this.id = id;
        }
    }
}