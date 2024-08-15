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
        
        public void Display(TreeBuff treeBuff)
        {
            Render(treeBuff.gameObject);
            
            if (treeBuff.BaseOxygenBuff != 0)
            {
                var oxygen = treeBuff.BaseOxygenBuff;
                buffText.text = $"{oxygen} / s";
            }
            else if (treeBuff.BaseSpeedBuff != 0)
            {
                var speed = treeBuff.BaseSpeedBuff;
                buffText.text = $"{speed} / s";
            }
            priceText.text = treeBuff.BasePrice.ToString();
            
        }
        
        private void Render(GameObject obj)
        {
            renderImage.Render(obj);
            var spin = renderImage.RenderObject.AddComponent<SpinForever>();
            spin.SetSpeed(10f);
        }
    }
}