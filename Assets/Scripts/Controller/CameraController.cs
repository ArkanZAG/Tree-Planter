using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Controller
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera mainCamera;
        [SerializeField] private CinemachineVirtualCamera topDownCamera;
        [SerializeField] private Vector3 minZoomOffset, maxZoomOffset;
        [SerializeField] private Vector2 zoomOffsetRange = new Vector2(1f, 5f);
        [SerializeField] private Vector2 zoomSpeedFactor = new Vector2(0.2f, 1f);
        [SerializeField] private float zoomSpeed = 0.1f;
        [SerializeField] private float cameraSpeed = 3f;
        [SerializeField] private GameObject cameraAnchor, anchorOffset;
        [SerializeField] private GridController gridController;
        [SerializeField] private Slider zoomSlider;
        [SerializeField] private Toggle topDownToggle;
        
        private float currentZoomAlpha = 0.5f;
        private float smoothAlpha;
        private float smoothVelocity = 0.5f;
        private bool isUiOffset;
        
        private CinemachineTransposer mainTransposer;
        private CinemachineTransposer topDownTransposer;
        
        private void Awake()
        {
                mainTransposer = mainCamera.GetCinemachineComponent<CinemachineTransposer>();
                topDownTransposer = topDownCamera.GetCinemachineComponent<CinemachineTransposer>();
                zoomSlider.onValueChanged.AddListener(f => currentZoomAlpha = Mathf.Clamp01(f));
                zoomSlider.SetValueWithoutNotify(currentZoomAlpha);
        }
        
        public void AddZoom(float f)
        {
                currentZoomAlpha = Mathf.Clamp01(currentZoomAlpha + (f * zoomSpeed));
                zoomSlider.SetValueWithoutNotify(currentZoomAlpha);
        }
        
        public void Update()
        {
                topDownCamera.Priority = topDownToggle.isOn ? 99 : 0;
                
                ProcessAnchorOffset();
                ProcessZoom();
        }
        
        private void ProcessZoom()
        {
                smoothAlpha = Mathf.SmoothDamp(smoothAlpha, 1f - currentZoomAlpha, ref smoothVelocity, 0.1f);
                var followOffset = Vector3.Slerp(minZoomOffset, maxZoomOffset, smoothAlpha);
                mainTransposer.m_FollowOffset = followOffset;
                topDownTransposer.m_FollowOffset = new Vector3(0f, followOffset.y, 0f);
        }
        
        public void SetIsUIOffset(bool on) => isUiOffset = on;
        
        private void ProcessAnchorOffset()
        {
                var anchorPos = anchorOffset.transform.localPosition;
                anchorPos.z = isUiOffset ? Mathf.Lerp(zoomOffsetRange.x, zoomOffsetRange.y, smoothAlpha) : 0f;
                anchorOffset.transform.localPosition = anchorPos;
        }
        
        public void MoveAnchor(Vector3 delta)
        {
                var pos = cameraAnchor.transform.position;
        
                var mod = Time.deltaTime * cameraSpeed * Mathf.Lerp(zoomSpeedFactor.x, zoomSpeedFactor.y, smoothAlpha);
                pos.x -= delta.x * mod;
                pos.z -= delta.y * mod;
        
                var size = gridController.StartPos * 1.5f;
                pos.x = Mathf.Clamp(pos.x, size.x, -size.x);
                pos.z = Mathf.Clamp(pos.z, size.z, -size.z);
        
                cameraAnchor.transform.position = pos;
        }
    }
}