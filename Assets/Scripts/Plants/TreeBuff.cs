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
        [Header("Stats")] 
        [SerializeField] private float baseOxygenBuff;
        [SerializeField] private float baseSpeedBuff;
        [SerializeField] private float basePrice;
        
        private Tile currentTile;

        public float BaseOxygenBuff => baseOxygenBuff;
        public float BaseSpeedBuff => baseSpeedBuff;
        public float BasePrice => basePrice;
        
        public void Initialize(Tile tile, Tile[] neighbours, GameController gameController, GridController gridController)
        {
            currentTile = tile;
        }

        public void OnClick()
        {
            
        }
    }
}