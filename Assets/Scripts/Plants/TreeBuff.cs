using System;
using System.Collections.Generic;
using Biomes;
using Controller;
using Data;
using GridSystem;
using UnityEngine;

namespace Plants
{
    public class TreeBuff : MonoBehaviour, IPlant
    {
        public GameObject GameObject => gameObject;
        public Tile Tile => currentTile;
        public BiomeType Biome => biomeType;
        public event Action OnPlantUpdated;
        public string Id => prefabId;
        
        [SerializeField] private string prefabId;
        [SerializeField] private BiomeType biomeType;
        [Header("Stats")] 
        [SerializeField] private float baseOxygenBuff;
        [SerializeField] private float baseSpeedBuff;
        [SerializeField] private int basePrice;
        [SerializeField] private int maxOxygenLevel = 1;
        [SerializeField] private int maxSpeedLevel = 1;
        [SerializeField] private float bonusSpeedPerLevel = 0.1f;
        
        private Tile currentTile;

        private int speedLevel = 1;
        private int oxygenLevel = 1;

        private int a = 1;

        public float BaseOxygenBuff => baseOxygenBuff;
        public float BaseSpeedBuff => baseSpeedBuff;
        public int BasePrice => basePrice;

        private bool IsOxygenBuff => baseOxygenBuff > 0f;
        private bool IsSpeedBuff => baseSpeedBuff > 0f;
        
        public void Initialize(Tile tile, Tile[] neighbours, GameController gameController, GridController gridController, EffectsController effectsController)
        {
            currentTile = tile;
        }

        #region Save
        
        public void SetValues(Dictionary<string, int> values, int version)
        {
            oxygenLevel = values.GetValueOrDefault("oxygenLevel", 1);
            speedLevel = values.GetValueOrDefault("speedLevel", 1);
        }

        public Dictionary<string, int> GetValues(int version)
        {
            return new Dictionary<string, int>()
            {
                {"oxygenLevel", oxygenLevel},
                {"speedLevel", speedLevel},
            };
        }
        
        #endregion
        
        public void OnClick()
        {
            
        }
        
        public bool IsMaxLevel()
        {
            //in case tree buff does both
            if (IsOxygenBuff && IsSpeedBuff)
                return oxygenLevel >= maxOxygenLevel && speedLevel >= maxSpeedLevel; 

            if (IsOxygenBuff) return oxygenLevel >= maxOxygenLevel;
            if (IsSpeedBuff) return speedLevel >= maxSpeedLevel;

            throw new Exception($"Buff Tree with id {prefabId} has 0 Oxygen and Speed buff?");
        }

        public float GetNormalizedVisualProgress()
        {
            if (IsOxygenBuff) return (float) oxygenLevel / maxOxygenLevel;
            if (IsSpeedBuff) return (float) speedLevel / maxSpeedLevel;
            return 0f;
        }

        public UpgradeDefinition[] GetUpgrades()
        {
            var list = new List<UpgradeDefinition>();

            if (IsOxygenBuff)
                list.Add(new UpgradeDefinition("Upgrade Oxygen Buff", GetOxygenLevel, GetTotalOxygenUpgradeCost,
                    GetOxygenMaxLevel, OnOxygenUpgrade));
            if (IsSpeedBuff)
                list.Add(new UpgradeDefinition("Upgrade Speed Buff", GetSpeedLevel, GetTotalSpeedUpgradeCost,
                    GetSpeedMaxLevel, OnSpeedUpgrade));

            return list.ToArray();
        }

        #region Calculations
        
        
        private int GetSpeedUpgradeCost(int level) => Mathf.RoundToInt(Mathf.Pow(level, 1.2f));        
        private int GetOxygenUpgradeCost(int level) => Mathf.RoundToInt(Mathf.Pow(level, 1.2f));

        private int GetTotalSpeedUpgradeCost(int levelAmount)
        {
            int total = 0;
            for (int i = speedLevel; i < speedLevel + levelAmount; i++)
                total += GetSpeedUpgradeCost(i);
            return total;
        }    
        
        private int GetTotalOxygenUpgradeCost(int levelAmount)
        {
            int total = 0;
            for (int i = oxygenLevel; i < oxygenLevel + levelAmount; i++)
                total += GetOxygenUpgradeCost(i);
            return total;
        }

        public int GetOxygenBonus() => IsOxygenBuff ? oxygenLevel : 0;
        public float GetSpeedBonus() => IsSpeedBuff ? speedLevel * bonusSpeedPerLevel : 0;
        private int GetOxygenLevel() => oxygenLevel;
        private int GetOxygenMaxLevel() => maxOxygenLevel;
        private int GetSpeedMaxLevel() => maxSpeedLevel;
        private void OnOxygenUpgrade(int amount)
        {
            oxygenLevel += amount;
            OnPlantUpdated?.Invoke();
        }
        private int GetSpeedLevel() => speedLevel;

        private void OnSpeedUpgrade(int amount)
        {
            speedLevel += amount;
            OnPlantUpdated?.Invoke();
        } 

        #endregion
    }
}