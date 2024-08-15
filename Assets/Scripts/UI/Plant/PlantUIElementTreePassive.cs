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

        private UIController uiController;
        private GridController gridController;
        private GameObject prefab;
        private Tile tile;

        private void Awake()
        {
            button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            gridController.PlantToTile(prefab, tile);
            uiController.HideTray();
        }

        public void Display(TreePassive treePassive, UIController ui, GridController grid, Tile injectTile)
        {
            prefab = treePassive.gameObject;
            uiController = ui;
            gridController = grid;
            tile = injectTile;
            
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