using Controller;
using Data;
using GridSystem;
using UnityEngine;

namespace Plants
{
    public class TreeBuff : MonoBehaviour, IPlant
    {
        public Tile Tile => currentTile;
        public string Id => prefabId;
        [SerializeField] private string prefabId;
        [SerializeField] private BiomeType biomeType;
        private Tile currentTile;
        
        public void Initialize(PlantData data, Tile tile, Tile[] neighbours, GameController gameController, GridController gridController)
        {
            currentTile = tile;
        }

        public void OnClick()
        {
            
        }
    }
}