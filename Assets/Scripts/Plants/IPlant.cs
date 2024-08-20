using System.Collections.Generic;
using Controller;
using Data;
using GridSystem;
using UnityEngine;

namespace Plants
{
    public interface IPlant
    {
        public GameObject GameObject { get; }
        public Tile Tile { get; }
        public BiomeType Biome { get; }
        public string Id { get; }
        
        public void Initialize(Tile tile, Tile[] neighbours, GameController gameController, GridController gridController, EffectsController effectsController);

        public void SetValues(Dictionary<string, int> values, int version);
        public Dictionary<string, int> GetValues(int version);
        
        public void OnClick();

        public UpgradeDefinition[] GetUpgrades();

        public bool IsMaxLevel();
    }
}