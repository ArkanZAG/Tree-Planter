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