using UnityEngine;
using UnityEngine.UI;

namespace ObjectRendering
{
    public class RenderImage : RawImage
    {
        private RenderReference reference;

        public GameObject RenderObject => reference.gameObject;
        
        public void Render(GameObject prefab)
        {
            if (reference) ObjectRenderer.Stop(reference);

            var width = Mathf.RoundToInt(rectTransform.rect.width);
            var height = Mathf.RoundToInt(rectTransform.rect.height);
            reference = ObjectRenderer.Render(prefab, width, height);
            texture = reference.CurrentTexture;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (reference) ObjectRenderer.Stop(reference);
        }
    }
}