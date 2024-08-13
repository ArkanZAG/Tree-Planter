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
        [SerializeField] [Tooltip("Base Oxygen Per Generation")]private float baseOxygen;
        [SerializeField] [Tooltip("Base Speed Oxygen Generation Per Second")] private float baseSpeed;
        [SerializeField] [Tooltip("Base Oxygen Per Tap")]private float baseTapOxygen;
        [Header("Modifier")]
        [SerializeField] private float speedPerGenerationLevel = 0.1f;

        private int treeLevel = 0;
        private int generationLevel = 0;
        private int tapLevel = 0;

        private float currentTimer;
            
        private Tile currentTile;
        
        public void Initialize(PlantData data, Tile tile, GameController gameController, GridController gridController)
        {
            currentTile = tile;
        }

        public void OnClick()
        {
            
        }

        private void Update()
        {
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
            Debug.Log("$$$");
        }
    }
}