using System;
using System.Collections.Generic;
using Controller;
using Data;
using GridSystem;
using UI.Plant;
using UnityEngine;
using Utilities;

namespace Plants
{
    public class TreePassive : MonoBehaviour, IPlant
    {
        public GameObject GameObject => gameObject;
        public Tile Tile => currentTile;
        public BiomeType Biome => biomeType;
        public event Action OnPlantUpdated;
        public string Id => prefabId;
        [SerializeField] private string prefabId;
        [SerializeField] private BiomeType biomeType;
        [Header("Stats")]
        [SerializeField] [Tooltip("Base Oxygen Per Generation")]private int baseOxygen;
        [SerializeField] [Tooltip("Base Speed Oxygen Generation Per Second")] private float baseSpeed;
        [SerializeField] [Tooltip("Base Oxygen Per Tap")]private int baseTapOxygen;
        [SerializeField] private int basePrice;
        [SerializeField] private int maxTreeLevel = 200;
        [SerializeField] private int maxGenerationLevel = 200;
        [SerializeField] private int maxTapLevel = 200;
        [Header("Modifier")]
        [SerializeField] private float speedPerGenerationLevel = 0.1f;

        private int treeLevel = 0;
        private int generationLevel = 0;
        private int tapLevel = 0;

        private int totalOxygenBonus;
        private float totalSpeedBonus;

        private float currentTimer;

        private bool initialized = false;
            
        private Tile currentTile;
        private Tile[] currentNeighbours;

        private GameController game;
        private EffectsController effects;
        private PlantUIElementTreePassive uIElementTreePassive;
        private GridController gridController;

        public int BaseOxygen => baseOxygen;
        public float BaseSpeed => baseSpeed;
        public int BaseTapOxygen => baseTapOxygen;
        public int BasePrice => basePrice;
        
        public void Initialize(Tile tile, Tile[] neighbours, GameController gameController, GridController gridCtrl, EffectsController effectsController)
        {
            currentTile = tile;
            currentNeighbours = neighbours;
            game = gameController;
            effects = effectsController;
            gridController = gridCtrl;
            
            initialized = true;

            foreach (var neighbour in neighbours)
                neighbour.OnTileUpdated += CalculateTileBonuses;
            
            CalculateTileBonuses();
        }
        
        public void OnClick()
        {
            gridController.GenerateAllPassive();
        }

        public void TapGenerate()
        {
            if (!initialized) return;
            var oxygenGeneration = GetTapOxygenGeneration();
            game.AddOxygen(oxygenGeneration);
            effects.ShowFlyingText($"+{oxygenGeneration}", transform.position);
        }
        private void Update()
        {
            if (!initialized) return;

            currentTimer += Time.deltaTime;
            var gps = GetGenerationPerSecond();
            var targetTime = 1 / gps;
            //Debug.Log("Current Time : " + currentTimer + " Target Time : " + targetTime);
            while (currentTimer >= targetTime)
            {
                currentTimer -= targetTime;
                GeneratePassive();
            }
        }
        private void GeneratePassive()
        {
            if (!initialized) return;
            var oxygenGeneration = GetPassiveOxygenGeneration();
            game.AddOxygen(oxygenGeneration);
            effects.ShowFlyingText($"+{oxygenGeneration}", transform.position, 1f / GetGenerationPerSecond());
        }

        public bool IsMaxLevel()
        {
            return treeLevel >= maxTreeLevel
                   && generationLevel >= maxGenerationLevel
                   && tapLevel >= maxTapLevel;
        }

        #region Save

        public void SetValues(Dictionary<string, int> values, int version)
        {
            treeLevel = values.GetValueOrDefault("treeLevel", 0);
            generationLevel = values.GetValueOrDefault("generationLevel", 0);
            tapLevel = values.GetValueOrDefault("tapLevel", 0);
        }

        public Dictionary<string, int> GetValues(int version)
        {
            return new Dictionary<string, int>()
            {
                {"treeLevel", treeLevel},
                {"generationLevel", generationLevel},
                {"tapLevel", tapLevel},
            };
        }
        
        #endregion

        #region Calculations

        private void CalculateTileBonuses()
        {
            totalOxygenBonus = 0;
            totalSpeedBonus = 0f;
            
            foreach (var tile in currentNeighbours)
            {
                if (tile.CurrentPlant is not TreeBuff treeBuff) continue;
                
                totalOxygenBonus += treeBuff.GetOxygenBonus();
                totalSpeedBonus += treeBuff.GetSpeedBonus();
            }
        }

        private int GetSavannaBonus() => gridController.GetPlantCount(BiomeType.Forest).FloorTo(10);
        private float GetTundraBonus() => 1f + (0.1f * gridController.GetPlantCount(BiomeType.Forest).FloorTo(10));
        private float GetForestBonus() => 1f + (0.1f * gridController.GetPlantCount(BiomeType.Forest).FloorTo(10));

        public float GetGenerationPerSecond()
            => (baseSpeed + (generationLevel * speedPerGenerationLevel) + totalSpeedBonus) * GetTundraBonus();

        public int GetPassiveOxygenGeneration()
            => Mathf.RoundToInt((baseOxygen * (1 + treeLevel * 0.4f) + totalOxygenBonus) * GetForestBonus()) ;
        
        private int GetTapOxygenGeneration()
            => Mathf.RoundToInt(baseTapOxygen + (1 + tapLevel * 0.5f)) + GetSavannaBonus();

        private int GetTreeLevelUpgradeCost()
            => Mathf.RoundToInt(GetPassiveOxygenGeneration() * (treeLevel / 2f));

        private int GetGenLevelUpgradeCost()
            => Mathf.RoundToInt(50 + Mathf.Pow(generationLevel, 1.8f));

        private int GetTapLevelUpgradeCost()
            => Mathf.RoundToInt(GetTapOxygenGeneration() * (tapLevel / 2f));

        #endregion

        #region Upgrades

        public UpgradeDefinition[] GetUpgrades()
        {
            return new UpgradeDefinition[]
            {
                new("Tree Level", GetTreeLevel, GetTreeLevelUpgradeCost, OnTreeLevelUpgrade),
                new("Generation Level", GetGenerationLevel, GetGenLevelUpgradeCost, OnGenLevelUpgrade),
                new("Tap Level", GetTapLevel, GetTapLevelUpgradeCost, OnTapLevelUpgrade),
            };
        }

        public int GetTreeLevel() => treeLevel;
        public int GetGenerationLevel() => generationLevel;
        public int GetTapLevel() => tapLevel;

        private void OnTreeLevelUpgrade()
        {
            treeLevel++;
            OnPlantUpdated?.Invoke();
        } 
        private void OnGenLevelUpgrade() 
        {
            generationLevel++;
            OnPlantUpdated?.Invoke();
        }
        private void OnTapLevelUpgrade()
        {
            tapLevel++;
            OnPlantUpdated?.Invoke();
        } 

        #endregion
    }
}