using System;
using System.Collections.Generic;
using System.Linq;
using Achievements;
using Data;
using Plants;
using Save;
using UnityEngine;
using Utilities;

namespace Controller
{
    public class GameController : MonoBehaviour
    {
        private const int VERSION = 1;

        [SerializeField] private int defaultWidth = 10;
        [SerializeField] private int defaultDepth = 10;
        [SerializeField] private GridController gridController;
        [SerializeField] private PlantDatabase plantDataBase;
        [SerializeField] private int oxygen;
        
        private PlayerData playerData;

        public int Oxygen => oxygen;

        private void Start()
        {
            plantDataBase.Initialize();
            Achievement.Initialize();
            
            if (SaveSystem.HasSaveData())
            {
                playerData = SaveSystem.Load();
                oxygen = playerData.oxygen;
            }
            else
            {
                playerData = CreateEmptyStartingData();
                RegisterNewGrid();
                SaveSystem.Save(playerData);
            }

            var gridToLoad = playerData.grids[playerData.currentGridId];
            gridController.Generate(gridToLoad);
        }

        private PlayerData CreateEmptyStartingData()
            => new(oxygen, String.Empty, new Dictionary<string, GridData>(), new HashSet<string>());

        private void RegisterNewGrid()
        {
            var grid = new GridData(VERSION, defaultWidth, defaultDepth, Array.Empty<PlantData>(), false);

            var id = ShortUid.New();

            playerData.currentGridId = id;
            playerData.grids[id] = grid;
        }

        public void AddOxygen(int value)
        {
            oxygen += value;
        }

        public void SaveProgress()
        {
            playerData.oxygen = oxygen;
            playerData.grids[playerData.currentGridId] = gridController.GetCurrentAsData(VERSION);

            SaveSystem.Save(playerData);
        }

        public int GetTotalPlantCount()
        {
            int total = 0;
            
            foreach (var grid in playerData.grids.Values)
                total += grid.plants.Length;

            return total;
        }

        public int GetFinishedLevelCount()
        {
            var total = 0;

            foreach (var grid in playerData.grids.Values)
                if (grid.isCompleted) total++;
            
            return total;
        }

        public bool HasAchieved(string id) => playerData.completedAchievements.Contains(id);
        public void AddAchievement(string id) => playerData.completedAchievements.Add(id);
    }
}
