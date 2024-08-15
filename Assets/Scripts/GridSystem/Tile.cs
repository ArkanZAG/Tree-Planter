using DG.Tweening;
using Plants;
using UnityEngine;

namespace GridSystem
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private Outline outline;
        [SerializeField] private GameObject visual;

        private int x, y;

        public int X => x;
        public int Y => y;
    
        private Tween selectTween;
        private IPlant currentPlant = null;

        public IPlant CurrentPlant => currentPlant;

        public void SetHighlight(bool isHighlighted)
        {
            outline.enabled = isHighlighted;
        }

        public void AnimatePop()
        {
            if (selectTween != null) DOTween.Kill(selectTween);

            var originalY = visual.transform.localPosition.y;
        
            var pos = visual.transform.localPosition;
            pos.y = originalY + 0.2f;
            visual.transform.localPosition = pos;
        
            selectTween = visual.transform.DOLocalMoveY(originalY, 0.15f).SetEase(Ease.OutCirc);
        }

        public void Initialize(int newX, int newY)
        {
            x = newX;
            y = newY;
        }

        public void SetPlant(IPlant plant)
        {
            currentPlant = plant;
        }
    }
}