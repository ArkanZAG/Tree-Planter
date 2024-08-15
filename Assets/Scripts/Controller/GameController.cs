using System;
using System.Collections.Generic;
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
            
            if (SaveSystem.HasSaveData())
            {
                playerData = SaveSystem.Load();
            }
            else
            {
                playerData = new(0, String.Empty, new Dictionary<string, GridData>());
                RegisterNewGrid();
            }

            var gridToLoad = playerData.grids[playerData.currentGridId];
            gridController.Generate(gridToLoad);
        }

        private void RegisterNewGrid()
        {
            var grid = new GridData(VERSION, defaultWidth, defaultDepth, Array.Empty<PlantData>());

            var id = ShortUid.New();

            playerData.currentGridId = id;
            playerData.grids[id] = grid;
        }

        public void AddOxygen(int value)
        {
            oxygen += value;
        }
    }
}
