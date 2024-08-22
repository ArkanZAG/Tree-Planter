using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using Plants;
using UI.Plant;
using UnityEngine;
using UnityEngine.UI;

public class BiomeUIElement : MonoBehaviour
{
    [SerializeField] private Sprite forestBiomeSprite;
    [SerializeField] private Sprite savannaBiomeSprite;
    [SerializeField] private Sprite tundraBiomeSprite;

    [SerializeField] private PlantDatabase plantDataBase;

    [SerializeField] private Image image;

    public void Display(BiomeType biome)
    {
        switch (biome)
        {
            case BiomeType.Forest :
                image.sprite = forestBiomeSprite;
                break;
            case BiomeType.Savanna :
                image.sprite = savannaBiomeSprite;
                break;
            case BiomeType.Tundra :
                image.sprite = tundraBiomeSprite;
                break;
            case BiomeType.None:
            default:
                break;
        }
    }

    public void ChangePreFabs()
    {
        for (int i = 0; i < plantDataBase.PlantPrefabs.Length; i++)
        {
            var plant = plantDataBase.PlantPrefabs[i].GetComponent<IPlant>();

            if (plant == null)
            {
                throw new Exception("IPlant Not Found");
            }
        }
    }

    public void OnClick()
    {
        
    }
}
