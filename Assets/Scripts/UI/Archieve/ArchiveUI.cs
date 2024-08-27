using System.Collections;
using System.Collections.Generic;
using Controller;
using UnityEngine;

public class ArchiveUI : MonoBehaviour
{
    [SerializeField] private GameObject elementUiPrefabs;
    [SerializeField] private Transform parent;

    [SerializeField] private GameController gameController;
    [SerializeField] private GridController gridController;

    private List<GameObject> spawnedObject;

    private void DIsplay()
    {
        ClearElements();
        
        for (int i = 0; i < gameController.GetTotalLevel(); i++)
        {
            var obj = Instantiate(elementUiPrefabs, parent);
            var archieveElement = obj.GetComponent<ArchiveUIElement>();
            archieveElement.Display(gameController, gridController);
            spawnedObject.Add(obj);
        }
    }

    private void ClearElements()
    {
        for (int i = 0; i < spawnedObject.Count; i++)
        {
            Destroy(spawnedObject[i]);
        }
        spawnedObject.Clear();
    }
}
