using DefaultNamespace;
using UnityEngine;

namespace Controller
{
    public class GridController : MonoBehaviour
    {
        [SerializeField] private Tile tilePrefab;
        [SerializeField] private int startSize = 10;
        [SerializeField] private MeshGrid grid;
    
        private Tile[][] spawnedTile;
        private Tile currentTile = null;

        public Tile SelectedTile => currentTile;
        public Vector3 startPos;

        private void Start()
        {
            spawnedTile = new Tile[startSize][];
        
            var isEven = startSize % 2 == 0;
            var offset = isEven ? 0.5f : 0f;

            var startValue = startSize / 2f - offset;
            startPos = new Vector3(-startValue, 0f, -startValue);

            for (int x = 0; x < startSize; x++)
            {
                spawnedTile[x] = new Tile[startSize];

                for (int y = 0; y < startSize; y++)
                {
                    //we invert y and x so that position properly starts at bottom left and reaches top right
                    var pos = startPos + new Vector3(y, 0f, x);
                    var tile = Instantiate(tilePrefab, pos, Quaternion.identity);
                    spawnedTile[x][y] = tile;
                }
            }
        
            grid.Generate(startSize, startSize);
        }

        public void SelectTile(Tile tile)
        {
            // foreach (var tileArray in spawnedTile)
            // foreach (var t in tileArray)
            // {
            //     t.SetHighlight(false);
            // }
            //
            // tile.SetHighlight(true);
            // tile.AnimatePop();
            // currentTile = tile;
        }
        
        public void TryDeselectTile()
        {
            // if (currentTile == null) return;
            // currentTile.SetHighlight(false);
            // currentTile = null;
        }
    }
}