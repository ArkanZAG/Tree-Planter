using Data;
using GridSystem;
using UnityEngine;
using Utilities;

namespace Controller
{
    public class GridController : MonoBehaviour
    {
        [SerializeField] private Tile tilePrefab;
        [SerializeField] private MeshGrid grid;
    
        private Tile[][] spawnedTile;
        private Tile currentTile = null;

        public Tile SelectedTile => currentTile;
        public Vector3 startPos;

        public void Generate(GridData data)
        {
            spawnedTile = new Tile[data.width][];
            for (int x = 0; x < data.width; x++)
                spawnedTile[x] = new Tile[data.depth];
            
            var offset = 0.5f;

            var startValueX = data.width / 2f - offset;
            var startValueZ = data.depth / 2f - offset;
            startPos = new Vector3(-startValueX, 0f, -startValueZ);

            for (int x = 0; x < data.width; x++)
            {
                for (int y = 0; y < data.depth; y++)
                {
                    var pos = startPos + new Vector3(x, 0f, y);
                    var tile = Instantiate(tilePrefab, pos, Quaternion.identity);
                    spawnedTile[x][y] = tile;
                }
            }
        
            grid.Generate(data.width, data.depth);

            SpawnPlants(data.plants);
        }
        
        //SPAWN PLANTS ACCORDING TO DATA, GET FROM PLANT DATABASE
        private void SpawnPlants(PlantData[] dataPlants)
        {
            
        }

        public void SelectTile(Tile tile)
        {
            foreach (var tileArray in spawnedTile)
            foreach (var t in tileArray)
            {
                t.SetHighlight(false);
            }
            
            tile.SetHighlight(true);
            tile.AnimatePop();
            currentTile = tile;
        }
        
        public void TryDeselectTile()
        {
            if (currentTile == null) return;
            currentTile.SetHighlight(false);
            currentTile = null;
        }
    }
}