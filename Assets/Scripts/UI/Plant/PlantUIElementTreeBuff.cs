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
        
        private GridController gridController;
        private UIController uiController;
        private GameObject prefab;
        private Tile tile;

        private void Awake()
        {
            button.onClick.AddListener(OnClick);
        }

        public void Display(TreeBuff buff, UIController ui, GridController grid, Tile injectTile)
        {
            uiController = ui;
            gridController = grid;
            tile = injectTile;
            prefab = buff.gameObject;
            
            Render(prefab);
            
            if (buff.BaseOxygenBuff != 0)
            {
                var oxygen = buff.BaseOxygenBuff;
                buffText.text = $"{oxygen} / s";
            }
            else if (buff.BaseSpeedBuff != 0)
            {
                var speed = buff.BaseSpeedBuff;
                buffText.text = $"{speed} / s";
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
            gridController.PlantToTile(prefab, tile);
            uiController.HideTray();
        }
    }
}