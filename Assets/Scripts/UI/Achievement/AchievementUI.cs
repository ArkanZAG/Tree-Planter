using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementUI : MonoBehaviour
{
    [SerializeField] private GameObject elementPrefabs;
    [SerializeField] private Transform parent;

    private List<GameObject> spawnedObject;
    
    public void Display()
    {
        var obj = Instantiate(elementPrefabs, parent);
        var achievementElement = obj.GetComponent<AchievementUIElement>();
        achievementElement.Display();
        spawnedObject.Add(obj);
    }
}
