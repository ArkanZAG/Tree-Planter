using System;

namespace Data
{
    [Serializable]
    public class GridData
    {
        public int version;
        public int width;
        public int depth;
        public string name;
        public PlantData[] plants;
        public bool isCompleted;

        public GridData(string name, int version, int width, int depth, PlantData[] plants, bool isCompleted)
        {
            this.version = version;
            this.width = width;
            this.depth = depth;
            this.plants = plants;
            this.isCompleted = isCompleted;
            this.name = name;
        }
    }
}