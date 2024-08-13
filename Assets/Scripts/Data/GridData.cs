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

        public GridData(int version, int width, int depth, PlantData[] plants)
        {
            this.version = version;
            this.width = width;
            this.depth = depth;
            this.plants = plants;
        }
    }
}