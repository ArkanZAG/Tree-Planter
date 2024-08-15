using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace ObjectRendering
{
    public static class ObjectRenderer
    {
        private static ObjectRenderingRoot _root;

        [RuntimeInitializeOnLoadMethod]
        static void Init()
        {
            var obj = new GameObject("Object Renderer Root");
            _root = obj.AddComponent<ObjectRenderingRoot>();
            
            _root.Initialize(CreateRenderCamera());
        }

        public static RenderReference Render(GameObject prefab, int width, int height) =>
            _root.Render(prefab, width, height);

        public static void Stop(RenderReference renderReference)
            => _root.Stop(renderReference);

        
        static Camera CreateRenderCamera()
        {
            Camera renderCamera = new GameObject("Render Camera").AddComponent<Camera>();
            renderCamera.nearClipPlane = 2.5f;
            renderCamera.depth = -10;
            renderCamera.fieldOfView = 10;
            renderCamera.clearFlags = CameraClearFlags.Depth;
            renderCamera.allowHDR = false;
            renderCamera.allowMSAA = false;
            renderCamera.cullingMask = LayerMask.GetMask("Renderable");
            renderCamera.forceIntoRenderTexture = true;
            renderCamera.enabled = false;
            return renderCamera;
        }
    }
}