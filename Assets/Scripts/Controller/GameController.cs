using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Achievements;
using Biomes;
using Data;
using Plants;
using Save;
using UI.FinishArchive;
using UI.Notice;
using UnityEngine;
using Utilities;

namespace Controller
{
    public class GameController : MonoBehaviour
    {
        private const int VERSION = 1;
        private const string IDLE_TIME_KEY = "idle-time";

        [SerializeField] private int defaultWidth = 10;
        [SerializeField] private int defaultDepth = 10;
        [SerializeField] private GridController gridController;
        [SerializeField] private PlantDatabase plantDataBase;
        [SerializeField] private BiomeDatabase biomeDatabase;
        [SerializeField] private int oxygen;
        [SerializeField] private NoticePopup noticePopup;
        [SerializeField] private FinishArchivePopup finishArchivePopup;
        [SerializeField] private SoundController soundController;
        [SerializeField] private AudioClip audioClipBGM;
        
        private PlayerData playerData;

        public int Oxygen => oxygen;
        private bool hasGameStarted = false;
        
        private void Start()
        {
            plantDataBase.Initialize();
            biomeDatabase.Initialize();
            Achievement.Initialize();
            soundController.PlayBgm(audioClipBGM, 0.6f);
            
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

            gridController.OnTilesUpdated += OnTilesUpdated;
            
            if (gridController.IsGridCompleted()) finishArchivePopup.Display(OnFinishArchiveSubmit);
            else SimulateIdleTime();
            
            hasGameStarted = true;
        }

        private void OnTilesUpdated()
        {
            if (!gridController.IsGridCompleted()) return;
            finishArchivePopup.Display(OnFinishArchiveSubmit);
        }

        private void OnFinishArchiveSubmit(string levelName)
        {
           var currentGrid = gridController.GetCurrentAsData(VERSION);
           currentGrid.name = levelName;
           playerData.grids[playerData.currentGridId] = currentGrid;
           
           RegisterNewGrid();
           
           var gridToLoad = playerData.grids[playerData.currentGridId];
           gridController.Generate(gridToLoad);
           
           SaveProgress();
        }

        private PlayerData CreateEmptyStartingData()
            => new(oxygen, String.Empty, new Dictionary<string, GridData>(), new HashSet<string>());

        private void RegisterNewGrid()
        {
            var grid = new GridData("Current",VERSION, defaultWidth, defaultDepth, Array.Empty<PlantData>(), false);

            var id = ShortUid.New();

            playerData.currentGridId = id;
            playerData.grids[id] = grid;
        }

        public void AddOxygen(int value)
        {
            oxygen += value;
        }

        public GridData[] GetAllGridData()
        {
            return playerData.grids.Values.ToArray();
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

        public bool HasClaimedAchievement(string id) => playerData.completedAchievements.Contains(id);
        public void ClaimAchievement(string id)
        { 
            playerData.completedAchievements.Add(id);
            SaveProgress();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (!hasGameStarted) return;
            if (!hasFocus) RecordIdleTime();
            else SimulateIdleTime();
        }

        private void OnApplicationQuit()
        {
            RecordIdleTime();
        }

        private void SimulateIdleTime()
        {
            if (!PlayerPrefs.HasKey(IDLE_TIME_KEY))
            {
                Debug.Log("PLAYER DOES NOT HAVE IDLE TIME KEY, will skip simulation");
                return;
            }

            var timeString = PlayerPrefs.GetString(IDLE_TIME_KEY);
            
            if (string.IsNullOrEmpty(timeString)){
                Debug.Log("PLAYER DOES NOT HAVE IDLE TIME KEY, will skip simulation");
                return;
            }
            
            var time = DateTime.ParseExact(timeString, "O", CultureInfo.InvariantCulture).ToUniversalTime();
            
            PlayerPrefs.SetString(IDLE_TIME_KEY, String.Empty);

            var timeDelta = DateTime.UtcNow - time;
            
            Debug.Log($"Time is {time}, now is {DateTime.UtcNow}, Time Delta is {timeDelta.TotalSeconds}");

            if (timeDelta.TotalSeconds < 1f) return;

            var totalIncome = gridController.GetTotalIncomePerSecond() * (float) timeDelta.TotalSeconds;
            var roundedIncome = Mathf.RoundToInt(totalIncome);

            if (roundedIncome <= 0) return;
            
            AddOxygen(roundedIncome);
            noticePopup.Display($"While you were away, you gained +{roundedIncome} Oxygen!");
            Debug.Log($"Displaying income {roundedIncome}");
        }

        private void RecordIdleTime()
        {
            var currentDate = DateTime.UtcNow.ToString("O", CultureInfo.InvariantCulture);
            PlayerPrefs.SetString(IDLE_TIME_KEY, currentDate);
            Debug.Log($"RECORDING IDLE TIME {currentDate}");
        }
    }
}
