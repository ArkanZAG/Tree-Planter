using System;
using Controller;
using GridSystem;
using ObjectRendering;
using Plants;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Plant
{
    public class PlantUIElementTreePassive : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI oxygenText;
        [SerializeField] private TextMeshProUGUI speedText;
        [SerializeField] private TextMeshProUGUI tapText;
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private RenderImage renderImage;
        [SerializeField] private Button button;
        [SerializeField] private AudioClip audioClip;

        private int currentPlantedPlant;
        private int plantedPlant;
        public int PlantedPlant => plantedPlant;

        private UIController uiController;
        private GridController gridController;
        private GameObject prefab;
        private Tile tile;
        private TreePassive tree;
        private GameController gameController;
        private SoundController soundController;
        private PlantUI plantUI;

        private void Awake()
        {
            button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            if (gameController.Oxygen >= tree.BasePrice)
            {
                gridController.PlantToTile(prefab, tile);
                uiController.HideTray();
                gameController.AddOxygen(-tree.BasePrice);
                soundController.PlaySfx(audioClip);
            }
        }

        public void Display(TreePassive treePassive, UIController ui, GridController grid, Tile injectTile, GameController game,
            PlantUI pUi, SoundController soundCont)
        {
            prefab = treePassive.gameObject;
            gameController = game;
            tree = treePassive;
            uiController = ui;
            gridController = grid;
            tile = injectTile;
            soundController = soundCont;
            plantUI = pUi;
            
            Render(prefab);
            
            var oxygen = treePassive.BaseOxygen;
            var speed = treePassive.BaseSpeed;
            var tap = treePassive.BaseTapOxygen;
            var price = treePassive.BasePrice;
            
            oxygenText.text = $"{oxygen}";
            speedText.text = $"{speed}/s";
            tapText.text = $"{tap}<size=60%>\nper Tap";
            priceText.text = $"{price}";
        }

        private void Render(GameObject obj)
        {
            renderImage.Render(obj);
            var spin = renderImage.RenderObject.AddComponent<SpinForever>();
            spin.SetSpeed(10f);
        }
    }
}