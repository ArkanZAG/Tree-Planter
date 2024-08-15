using System;
using Controller;
using TMPro;
using UnityEngine;

namespace UI.Player
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI oxygen;
        [SerializeField] private TextMeshProUGUI oxygenIncome;
        [SerializeField] private GameController gameController;
        [SerializeField] private GridController gridController;

        private void Awake()
        {
            gridController.OnTilesUpdated += OnTilesUpdated;
        }

        private void OnTilesUpdated()
        {
            oxygenIncome.text = $"+ {gridController.GetTotalIncomePerSecond():F1} / s";
        }

        public void Display()
        {
            oxygen.text = gameController.Oxygen.ToString();
        }

        private void Update()
        {
            Display();
        }
    }
}