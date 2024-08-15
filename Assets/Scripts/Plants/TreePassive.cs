using System;
using Controller;
using Data;
using GridSystem;
using UnityEngine;

namespace Plants
{
    public class TreePassive : MonoBehaviour, IPlant
    {
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

        private GameController currentGameController;

        public int BaseOxygen => baseOxygen;
        public float BaseSpeed => baseSpeed;
        public int BaseTapOxygen => baseTapOxygen;
        public float BasePrice => basePrice;
        
        public void Initialize(Tile tile, Tile[] neighbours, GameController gameController, GridController gridController)
        {
            currentTile = tile;
            currentGameController = gameController;
            initialized = true;
        }

        public void OnClick()
        {
            if (!initialized) return;
            
            currentGameController.AddOxygen(baseOxygen);
        }

        private void Update()
        {
            if (!initialized) return;

            currentTimer += Time.deltaTime;
            var gps = baseSpeed + (generationLevel * 0.1f);
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
            
            currentGameController.AddOxygen(baseOxygen);
        }
    }
}