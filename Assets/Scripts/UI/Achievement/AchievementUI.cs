using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private SoundController soundController;

    [SerializeField] private AudioClip openUiAudioClip;

    private bool isShowing = false;

    private List<GameObject> spawnedObject = new();

    public void Display()
    {
        soundController.PlaySfx(openUiAudioClip);
        holder.SetActive(true); 
        ClearElements();
        // Di sini kita sort order achievement berdasarkan HasClaimedAchievement, kalau false dia di atas, kalau true di bawah
        var allAchievement = Achievement.GetAll()
            .OrderBy(x => gameController.HasClaimedAchievement(x.Id));
        foreach (var achievement in allAchievement)
        {
            var obj = Instantiate(elementPrefabs, parent);
            var achievementElement = obj.GetComponent<AchievementUIElement>();
            achievementElement.Display(achievement,gameController,gridController, soundController);
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
