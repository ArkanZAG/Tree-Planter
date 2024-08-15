using Plants;
using TMPro;
using UnityEngine;

namespace UI.Plant
{
    public class PlantUIElementTreePassive : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI oxygenText;
        [SerializeField] private TextMeshProUGUI speedText;
        [SerializeField] private TextMeshProUGUI tapText;
        [SerializeField] private TextMeshProUGUI priceText;
        
        public void Display(TreePassive treePassive)
        {
            var oxygen = treePassive.BaseOxygen;
            var speed = treePassive.BaseSpeed;
            var tap = treePassive.BaseTapOxygen;
            var price = treePassive.BasePrice;
            oxygenText.text = $"{oxygen} / s";
            speedText.text = $"{speed} s";
            tapText.text = $"{tap} / tap";
            priceText.text = $"{price}";
        }
    }
}