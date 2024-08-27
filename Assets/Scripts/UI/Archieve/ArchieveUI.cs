using System.Collections;
using System.Collections.Generic;
using Controller;
using UnityEngine;

public class ArchieveUI : MonoBehaviour
{
    [SerializeField] private GameObject elementUiPrefabs;
    [SerializeField] private Transform parent;

    [SerializeField] private GameController gameController;

    private void DIsplay()
    {
        for (int i = 0; i < UPPER; i++)
        {
            var obj = Instantiate(elementUiPrefabs, parent);
            var archieveElement = obj.GetComponent<ArchieveUIElement>();
            archieveElement.Display();
        }
        
    }
}
