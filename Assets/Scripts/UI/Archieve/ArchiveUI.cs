using System.Collections;
using System.Collections.Generic;
using Controller;
using UnityEngine;

public class ArchiveUI : MonoBehaviour
{
    [SerializeField] private GameObject elementUiPrefabs;
    [SerializeField] private GameObject holder;
    [SerializeField] private Transform parent;

    [SerializeField] private GameController gameController;
    [SerializeField] private GridController gridController;

    private List<GameObject> spawnedObject;

    public void Display()
    {
        ClearElements();

        foreach (var data in gameController.GetAllGridData())
        {
            var obj = Instantiate(elementUiPrefabs, parent);
            var archieveElement = obj.GetComponent<ArchiveUIElement>();
            archieveElement.Display(data, gameController, gridController);
            spawnedObject.Add(obj);
        }
    }

    public void Show(bool isShowing)
    {
        holder.SetActive(isShowing);
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
