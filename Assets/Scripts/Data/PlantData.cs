using System;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.Universal;

namespace Data
{
    [Serializable]
    public class PlantData
    {
        public int x;
        public int y;
        public string id;
        public Dictionary<string, int> values;

        public PlantData(int x, int y, string id, Dictionary<string, int> values)
        {
            this.x = x;
            this.y = y;
            this.id = id;
            this.values = values;
        }
    }
}