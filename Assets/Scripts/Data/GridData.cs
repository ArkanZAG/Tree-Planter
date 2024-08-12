using System;

namespace Data
{
    [Serializable]
    public class GridData
    {
        public int version;
        public int width;
        public int depth;
        public PlantData[] plants;
    }
}