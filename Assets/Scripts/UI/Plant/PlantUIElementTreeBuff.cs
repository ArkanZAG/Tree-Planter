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
        private TreeBuff treeBuff;
        private Tile tile;
        public void Display(TreeBuff buff, GridController grid, Tile injectTile)
        {
            Render(buff.gameObject);
            
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
            gridController = grid;
            tile = injectTile;
        }
        private void Render(GameObject obj)
        {
            renderImage.Render(obj);
            var spin = renderImage.RenderObject.AddComponent<SpinForever>();
            spin.SetSpeed(10f);
        }

        private void OnClick()
        {
            gridController.PlantToTile(treeBuff.gameObject, tile);
        }
    }
}