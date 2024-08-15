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
            oxygenText.text = treePassive.BaseOxygen.ToString();
            speedText.text = treePassive.BaseSpeed.ToString();
            tapText.text = treePassive.BaseTapOxygen.ToString();
            priceText.text = treePassive.BasePrice.ToString();
        }
    }
}