using UnityEngine;
using UnityEngine.UI;

namespace Utilities
{
    public class BoolSpriteSwitcher : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Sprite onTrue, onFalse;

        public void Set(bool state)
        {
            image.sprite = state ? onTrue : onFalse;
        }
    }
}