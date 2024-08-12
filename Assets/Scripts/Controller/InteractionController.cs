using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Controller
{
    public class InteractionController : MonoBehaviour
    {
        [SerializeField] private GridController gridController;
        [SerializeField] private float minDragDistance = 10f;
        [SerializeField] private float zoomPinchModifier = 1f;
        [SerializeField] private GameObject plantUI;
        //[SerializeField] private UpgradeUI upgradeUI;
        [SerializeField] private CameraController cameraController;
        [SerializeField] private Toggle topDownToggle;
    
    private Camera camera;
    private bool canStartDragging = false;
    private bool isDragging = false;
    private Vector3 startDragPos, latestDragPos;
    
    private void Start()
    {
        camera = Camera.main;
    }
    
    private void Update()
    {
        if (DetectMouseZoom()) return;
    
        if (IsOverUI()) return;
        if (DetectTouchZoom()) return;
        if (DetectTile()) return;
        if (DetectDrag()) return;
    }
    
    private bool DetectTouchZoom()
    {
        if (Input.touchCount != 2) return false;
        var touchZero = Input.touches[0];
        var touchOne = Input.touches[1];
    
        var zeroPrevPos = touchZero.position - touchZero.deltaPosition;
        var onePrevPos = touchOne.position - touchOne.deltaPosition;
    
        var prevMagnitude = (zeroPrevPos - onePrevPos).magnitude;
        var currentMagnitude = (touchZero.position - touchOne.position).magnitude;
    
        var difference = currentMagnitude - prevMagnitude;
        cameraController.AddZoom(difference * zoomPinchModifier);
        return true;
    }
    
    private bool DetectMouseZoom()
    {
        if (Input.mouseScrollDelta.magnitude <= 0f) return false;
        cameraController.AddZoom(Input.mouseScrollDelta.y);
        return true;
    }
    
    bool IsOverUI()
    {
        var data = new PointerEventData(EventSystem.current);
        data.position = Input.mousePosition;
        var list = new List<RaycastResult>();
        
        EventSystem.current.RaycastAll(data, list);
        
        return list.Count > 0;
    }
    
    bool DetectDrag()
    {
        if (!Input.GetMouseButton(0))
        {
            isDragging = false;
            canStartDragging = false;
            return false;
        }
        
        if (!canStartDragging)
        {
            canStartDragging = true;
            startDragPos = Input.mousePosition;
            latestDragPos = startDragPos;
            return true;
        }
    
        Vector3 startDelta = Input.mousePosition - startDragPos;
        //Debug.Log($"MAGNITUDE IS {startDelta.magnitude}");
    
        if (startDelta.magnitude < minDragDistance) return false;
    
        isDragging = true;
        
        Vector3 delta = Input.mousePosition - latestDragPos;
        cameraController.MoveAnchor(delta);
        latestDragPos = Input.mousePosition;
    
        return true;
    }
    
    bool DetectTile()
    {
        if (isDragging) return false;
        if (!Input.GetMouseButtonUp(0)) return false;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
    
        var hits = Physics.RaycastAll(ray);
    
        if (hits.Length <= 0)
        {
            DisableUI();
            gridController.TryDeselectTile();
            return false;
        }
    
        foreach (var hit in hits)
        {
            // if (!topDownToggle.isOn && hit.collider.TryGetComponent<IPlantable>(out var plantable))
            // {
            //     plantable.OnClick();
            //     var plantTile = plantable.Tile;
            //     if (plantTile.Equals(gridController.SelectedTile)) return true;
            //
            //     DisableUI();
            //     OpenUI(plantTile);
            //     gridController.SelectTile(plantTile);
            //
            //     return true;
            // }
            //
            // Tile tile = hit.collider.GetComponent<Tile>();
            // if (tile == null) continue; 
            //
            // DisableUI();
            // OpenUI(tile);
            // gridController.SelectTile(tile);
            // return true;
        }
    
        return false;
    }
    
    private void OpenUI(Tile tile)
    {
        // if (!tile.HasPlant)
        // {
        //     plantUI.SetActive(true);
        //     cameraController.SetIsUIOffset(true);
        //     return;
        // }
        //
        // if (upgradeUI.TryShowUpgradeUI(tile)) cameraController.SetIsUIOffset(true);
    }
    
    public void DisableUI()
    {
        // Debug.Log("CLOSE UI");
        // upgradeUI.Hide();
        // plantUI.SetActive(false);
        // cameraController.SetIsUIOffset(false);
    }
    }
}