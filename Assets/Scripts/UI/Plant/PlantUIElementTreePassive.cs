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

        private int currentPlantedPlant;
        private int plantedPlant;
        public int PlantedPlant => plantedPlant;

        private UIController uiController;
        private GridController gridController;
        private GameObject prefab;
        private Tile tile;
        private TreePassive tree;
        private GameController gameController;
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
            }
        }

        public void Display(TreePassive treePassive, UIController ui, GridController grid, Tile injectTile, GameController game, PlantUI pUi)
        {
            prefab = treePassive.gameObject;
            gameController = game;
            tree = treePassive;
            uiController = ui;
            gridController = grid;
            tile = injectTile;
            plantUI = pUi;
            
            Render(prefab);
            
            var oxygen = treePassive.BaseOxygen;
            var speed = treePassive.BaseSpeed;
            var tap = treePassive.BaseTapOxygen;
            var price = treePassive.BasePrice;
            
            oxygenText.text = $"{oxygen} / s";
            speedText.text = $"{speed} s";
            tapText.text = $"{tap} / tap";
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