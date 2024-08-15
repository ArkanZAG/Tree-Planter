using UnityEngine;
using UnityEngine.Pool;

namespace Effects
{
    public class FlyingTextHandler : MonoBehaviour
    {
        [SerializeField] private FlyingText prefab;

        private ObjectPool<FlyingText> pool;
        private Camera camera;

        private void Awake()
        {
            camera = Camera.main;
            pool = new(CreateFlyingText, OnGetFlyingText, OnReleaseFlyingText, OnDestroyFlyingText);
        }

        private void OnDestroyFlyingText(FlyingText obj) => Destroy(obj.gameObject);

        private void OnReleaseFlyingText(FlyingText obj) => obj.gameObject.SetActive(false);

        private void OnGetFlyingText(FlyingText obj) => obj.gameObject.SetActive(true);

        private FlyingText CreateFlyingText() => Instantiate(prefab);

        public void Create(string text, Vector3 worldPosition, float duration = 1f)
        {
            var flyingText = pool.Get();

            var screenPos = camera.WorldToScreenPoint(worldPosition);
            
            flyingText.Display(text, duration, screenPos, pool.Release);
        }
    }
}