using System;
using Controller;
using GridSystem;
using ObjectRendering;
using Plants;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.Plant

{
    public class PlantUIElementTreeBuff : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI buffText;
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private RenderImage renderImage;
        [SerializeField] private Button button;
        [SerializeField] private Image iconImage;

        [SerializeField] private Sprite oxygenSprite;
        [SerializeField] private Sprite speedSprite;
        
        
        private GridController gridController;
        private UIController uiController;
        private GameObject prefab;
        private Tile tile;
        private TreeBuff tree;
        private GameController gameController;

        private void Awake()
        {
            button.onClick.AddListener(OnClick);
        }

        public void Display(TreeBuff buff, UIController ui, GridController grid, Tile injectTile, GameController game)
        {
            tree = buff;
            gameController = game;
            uiController = ui;
            gridController = grid;
            tile = injectTile;
            prefab = buff.gameObject;
            
            Render(prefab);
            
            if (buff.BaseOxygenBuff != 0)
            {
                var oxygen = buff.BaseOxygenBuff;
                buffText.text = $"{oxygen}/s";
                iconImage.sprite = oxygenSprite;
            }
            else if (buff.BaseSpeedBuff != 0)
            {
                var speed = buff.BaseSpeedBuff;
                buffText.text = $"{speed}/s";
                iconImage.sprite = speedSprite;
            }
            priceText.text = buff.BasePrice.ToString();
        }
        private void Render(GameObject obj)
        {
            renderImage.Render(obj);
            var spin = renderImage.RenderObject.AddComponent<SpinForever>();
            spin.SetSpeed(10f);
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
    }
}