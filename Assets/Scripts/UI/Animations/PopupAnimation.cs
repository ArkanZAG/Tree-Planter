using DG.Tweening;
using UnityEngine;

namespace UI.Animations
{
    public class PopupAnimation : MonoBehaviour
    {
        [SerializeField] private RectTransform holder;
        [SerializeField] private float duration = 0.3f;
        [SerializeField] private AnimationCurve curveX, curveY;

        private Tween tweenX, tweenY;
        
        public void StartAnimation()
        {
            if (tweenX != null) DOTween.Kill(tweenX);
            if (tweenY != null) DOTween.Kill(tweenY);
            
            holder.localScale = new Vector3(0f, 0f , 1f);

            tweenX = holder.DOScaleX(1f, duration).SetEase(curveX);
            tweenY = holder.DOScaleY(1f, duration).SetEase(curveY);
        } 
    }
}