using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace ObjectRendering
{
    public class ObjectRenderingRoot : MonoBehaviour
    {
        private bool initialized = false;
        private float accumulator = 0f;
        private Camera camera;

        private HashSet<RenderReference> renderedObjects = new();

        public void Initialize(Camera cam)
        {
            camera = cam;
            initialized = true;
        }

        public RenderReference Render(GameObject prefab, int width, int height)
        {
            if (!prefab.TryGetComponent(out RenderReference _))
                throw new Exception($"Trying to render prefab {prefab.name} but it does NOT have RenderReference");

            var obj = Instantiate(prefab);

            var renderObject = obj.GetComponent<RenderReference>();
            
            renderedObjects.Add(renderObject);
            
            RenderTexture rt = CreateRenderTexture(width, height);
            SetGameLayerRecursive(renderObject.gameObject, LayerMask.NameToLayer("Renderable"));

            renderObject.SetTexture(rt);
            
            ArrangeRenderObjects();

            return renderObject;
        }
        
        private void SetGameLayerRecursive(GameObject obj, int layer)
        {
            obj.layer = layer;
            foreach (Transform child in obj.transform)
            {
                SetGameLayerRecursive(child.gameObject, layer);
            }
        }

        public void Stop(RenderReference renderReference)
        {
            renderedObjects.Remove(renderReference);
            DestroyImmediate(renderReference.gameObject);
        }

        void Update()
        {
            if (!initialized) return;

            float target = Math.Max(30, (float)Screen.currentResolution.refreshRateRatio.value);
            
            var time = 1f / target;
            accumulator += Time.deltaTime;
            
            bool hasRenderedThisFrame = false;

            while (accumulator > time)
            {
                accumulator -= time;
                
                if (hasRenderedThisFrame) continue;
                
                hasRenderedThisFrame = true;

                foreach (var obj in renderedObjects)
                    MoveToCapture(obj);
            }
        }
        
        void MoveToCapture(RenderReference obj)
        {
            if (obj == null) return;
            Vector3 pos = obj.transform.position;
            Transform camTransform = camera.transform;

            camTransform.position = pos + obj.PositionOffset;

            Vector3 dir = (pos - camTransform.position).normalized;
            Vector3 lr = Vector3.zero;

            if (dir != Vector3.zero) lr = Quaternion.LookRotation(dir, Vector3.up).eulerAngles;

            camTransform.rotation = Quaternion.Euler(lr + obj.RotationOffset);
            camera.targetTexture = obj.CurrentTexture;
            camera.Render();
        }
        
        void ArrangeRenderObjects()
        {
            int i = 0;
            foreach (var obj in renderedObjects)
            {
                obj.transform.SetParent(transform);
                obj.transform.position = new Vector3(i * 10 + 1000, 0, 0);
                i++;
            }
        }
        
        static RenderTexture CreateRenderTexture(int width, int height)
        {
            RenderTexture result = new(width, height, 16, DefaultFormat.LDR);
            result.wrapMode = TextureWrapMode.Clamp;
            result.antiAliasing = 1;
            return result;
        }
    }
}