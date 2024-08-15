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
        
        public void Display(TreeBuff treeBuff)
        {
            if (treeBuff.BaseOxygenBuff != 0)
            {
                buffText.text = treeBuff.BaseOxygenBuff.ToString();
                priceText.text = treeBuff.BasePrice.ToString();
            }
            else if (treeBuff.BaseSpeedBuff != 0)
            {
                buffText.text = treeBuff.BaseSpeedBuff.ToString();
                priceText.text = treeBuff.BasePrice.ToString();
            }
            
        }
        
        
    }
}