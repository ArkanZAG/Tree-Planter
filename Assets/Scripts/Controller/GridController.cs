using System;
using System.Collections.Generic;
using System.Linq;
using Biomes;
using Data;
using GridSystem;
using Plants;
using Unity.VisualScripting;
using UnityEngine;
using Utilities;

namespace Controller
{
    public class GridController : MonoBehaviour
    {
        [SerializeField] private Tile tilePrefab;
        [SerializeField] private MeshGrid grid;
        [SerializeField] private PlantDatabase plantDataBase;
        [SerializeField] private BiomeDatabase biomeDataBase;
        [SerializeField] private GameController gameController;
        [SerializeField] private EffectsController effectsController;
        [SerializeField] private AchievementUI achievementUI;
        private Vector3 startPos;

        private Tile[][] spawnedTile;
        private Tile currentTile = null;

        private int width, depth;
        private string gridName;

        private Dictionary<BiomeType, int> biomeTileCount = new();

        public event Action OnTilesUpdated;

        public Tile SelectedTile => currentTile;
        public Vector3 StartPos => startPos;

        public void Generate(GridData data)
        {
            gridName = data.name;
            width = data.width;
            depth = data.depth; 
            
            spawnedTile = new Tile[width][];
            for (int x = 0; x < width; x++)
                spawnedTile[x] = new Tile[depth];
            
            var offset = 0.5f;

            var startValueX = width / 2f - offset;
            var startValueZ = depth / 2f - offset;
            startPos = new Vector3(-startValueX, 0f, -startValueZ);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < depth; y++)
                {
                    var pos = startPos + new Vector3(x, 0f, y);
                    var tile = Instantiate(tilePrefab, pos, Quaternion.identity);
                    tile.Initialize(biomeDataBase, x, y);
                    tile.OnTileUpdated += OnAnyTileUpdated;
                    spawnedTile[x][y] = tile;
                }
            }
        
            grid.Generate(width, depth);

            SpawnPlants(data.plants);

            EvaluatePlantCount();
            
            OnTilesUpdated?.Invoke();
        }

        private void OnAnyTileUpdated()
        {
            OnTilesUpdated?.Invoke();

            EvaluatePlantCount();

            gameController.SaveProgress();
        }

        private void SpawnPlants(PlantData[] dataPlants)
        {
            for (int i = 0; i < dataPlants.Length; i++)
            {
                var data = dataPlants[i];
                var tile = spawnedTile[data.x][data.y];
                var prefab = plantDataBase.GetPrefab(data.id);
                PlantToTile(prefab, tile, false);
            }
        }

        public void PlantToTile(GameObject prefab, Tile tile, bool invokeUpdate = true)
        {
            var obj = Instantiate(prefab, tile.transform);
            var iPlant = obj.GetComponent<IPlant>();
            var neighbours = GetNeighbours(tile.X, tile.Y);
                
            iPlant.Initialize(tile, neighbours.ToArray(), gameController, this, effectsController);
            tile.SetPlant(iPlant, invokeUpdate);
        }

        public void GenerateAllPassive()
        {
            foreach (var tileArray in spawnedTile)
            {
                foreach (var tile in tileArray)
                {
                    if (tile.CurrentPlant is not TreePassive treePassive) continue;
                    treePassive.TapGenerate();
                }
            }
        }

        private IEnumerable<Tile> GetNeighbours(int x, int y)
        {
            var tiles = new List<Tile>();
    
            // Check left
            if (x > 0) 
                tiles.Add(spawnedTile[x - 1][y]);
    
            // Check right
            if (x < spawnedTile.Length - 1) 
                tiles.Add(spawnedTile[x + 1][y]);
    
            // Check top
            if (y > 0) 
                tiles.Add(spawnedTile[x][y - 1]);
    
            // Check bottom
            if (y < spawnedTile[x].Length - 1) 
                tiles.Add(spawnedTile[x][y + 1]);

            return tiles;
        }

        public float GetTotalIncomePerSecond()
        {
            float total = 0;
            
            foreach (var tileArray in spawnedTile)
                foreach (var tile in tileArray)
                {
                    if (tile.CurrentPlant is not TreePassive treePassive) continue;
                    var gps = treePassive.GetGenerationPerSecond();
                    var amount = treePassive.GetPassiveOxygenGeneration();
                    total += gps * amount;
                }

            return total;
        }

        private void EvaluatePlantCount()
        {
            biomeTileCount = new();
            foreach (var tileArray in spawnedTile)
            foreach (var tile in tileArray)
            {
                if (tile.CurrentPlant == null) continue;
                if (tile.CurrentPlant.Biome == BiomeType.None) continue;
                
                var biome = tile.CurrentPlant.Biome;
                
                if (biomeTileCount.TryGetValue(biome, out var count)) biomeTileCount[biome] = count + 1;
                else biomeTileCount[biome] = 0;
            }
        }

        public int GetPlantCount(BiomeType biome)
        {
            if (biomeTileCount.TryGetValue(biome, out var count)) return count;
            
            return 0;
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

        private bool IsGridCompleted()
        {
            foreach (var tiles in spawnedTile)
            foreach (var tile in tiles)
            {
                if (tile.CurrentPlant == null) return false;
                if (!tile.CurrentPlant.IsMaxLevel()) return false;
            }

            return true;
        }

        public GridData GetCurrentAsData(int version)
        {
            var plantList = new List<PlantData>();

            foreach (var tiles in spawnedTile)
                foreach (var tile in tiles)
                {
                    if (tile.CurrentPlant == null) continue;
                    var data = tile.GetPlantData(version);
                    plantList.Add(data);
                }

            return new GridData(gridName, version, width, depth, plantList.ToArray(), IsGridCompleted());
        }
    }
}