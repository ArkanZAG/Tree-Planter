using System;
using Controller;
using Data;
using GridSystem;
using UnityEngine;

namespace Plants
{
    public class TreePassive : MonoBehaviour, IPlant
    {
        public GameObject GameObject => gameObject;
        public Tile Tile => currentTile;
        public string Id => prefabId;
        [SerializeField] private string prefabId;
        [SerializeField] private BiomeType biomeType;
        [Header("Stats")]
        [SerializeField] [Tooltip("Base Oxygen Per Generation")]private int baseOxygen;
        [SerializeField] [Tooltip("Base Speed Oxygen Generation Per Second")] private float baseSpeed;
        [SerializeField] [Tooltip("Base Oxygen Per Tap")]private int baseTapOxygen;
        [SerializeField] private float basePrice;
        [Header("Modifier")]
        [SerializeField] private float speedPerGenerationLevel = 0.1f;

        private int treeLevel = 0;
        private int generationLevel = 0;
        private int tapLevel = 0;

        private float currentTimer;

        private bool initialized = false;
            
        private Tile currentTile;

        private GameController game;
        private EffectsController effects;

        public int BaseOxygen => baseOxygen;
        public float BaseSpeed => baseSpeed;
        public int BaseTapOxygen => baseTapOxygen;
        public float BasePrice => basePrice;
        
        public void Initialize(Tile tile, Tile[] neighbours, GameController gameController, GridController gridController, EffectsController effectsController)
        {
            currentTile = tile;
            game = gameController;
            effects = effectsController;
            initialized = true;
        }

        public void OnClick()
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
            Debug.Log("Current Time : " + currentTimer + " Target Time : " + targetTime);
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


        #region Calculations
        
        public float GetGenerationPerSecond()
            => baseSpeed + (generationLevel * speedPerGenerationLevel);
        
        public int GetPassiveOxygenGeneration()
            => Mathf.RoundToInt(baseOxygen * (1 + treeLevel * 0.5f));
        
        private int GetTapOxygenGeneration()
            => Mathf.RoundToInt(baseTapOxygen + (1 + tapLevel * 0.2f));

        private int GetTreeLevelUpgradeCost()
            => Mathf.RoundToInt(GetPassiveOxygenGeneration() * (treeLevel / 2f));

        private int GetGenLevelUpgradeCost()
            => Mathf.RoundToInt(50 + Mathf.Pow(generationLevel, 1.2f));

        private int GetTapLevelUpgradeCost()
            => Mathf.RoundToInt(GetTapOxygenGeneration() * (tapLevel / 2f));

        #endregion

        #region Upgrades

        public UpgradeDefinition[] GetUpgrades()
        {
            var treeLvlCost = GetTreeLevelUpgradeCost();
            
            var genLvlCost = GetGenLevelUpgradeCost();
            
            var tapLvlCost = GetTapLevelUpgradeCost();
            
            return new UpgradeDefinition[]
            {
                new("Tree Level", treeLevel, treeLvlCost, OnTreeLevelUpgrade),
                new("Generation Level", generationLevel, genLvlCost, OnGenLevelUpgrade),
                new("Tap Level", tapLevel, tapLvlCost, OnTapLevelUpgrade),
            };
        }

        private void OnTreeLevelUpgrade() => treeLevel++;
        private void OnGenLevelUpgrade() => generationLevel++;
        private void OnTapLevelUpgrade() => tapLevel++;

        #endregion
    }
}