using System;
using System.Collections;
using System.Collections.Generic;
using Achievements;
using Controller;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AchievementUI : MonoBehaviour
{
    [SerializeField] private Transform parent;

    [SerializeField] private GameObject holder;
    [SerializeField] private GameObject elementPrefabs;

    [SerializeField] private GameController gameController;
    [SerializeField] private GridController gridController;

    private bool isShowing = false;

    private List<GameObject> spawnedObject = new();

    public void Display()
    {
        holder.SetActive(true);
        ClearElements();
        foreach (var achievement in Achievement.GetAll())
        {
            var obj = Instantiate(elementPrefabs, parent);
            var achievementElement = obj.GetComponent<AchievementUIElement>();
            achievementElement.Display(achievement,gameController,gridController);
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

    public void Show(bool isShowing)
    {
        holder.SetActive(isShowing);
    }
}
