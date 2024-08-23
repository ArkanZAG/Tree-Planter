using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using GridSystem;
using Plants;
using UI.Plant;
using UnityEngine;
using UnityEngine.UI;

public class BiomeUIElement : MonoBehaviour
{
    [SerializeField] private Sprite forestBiomeSprite;
    [SerializeField] private Sprite savannaBiomeSprite;
    [SerializeField] private Sprite tundraBiomeSprite;

    [SerializeField] private Button button;

    [SerializeField] private PlantDatabase plantDataBase;

    [SerializeField] private Image image;

    private BiomeType biomeType;
    private PlantUI plantUi;
    private Tile tile;

    private void Awake()
    {
        button.onClick.AddListener(OnClick);
    }
    

    public void Display(BiomeType biome, PlantUI pUI, Tile tl)
    {
        biomeType = biome;
        plantUi = pUI;
        tile = tl;
        
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

    public void OnClick()
    {
        plantUi.SetBiome(biomeType);
        plantUi.SpawnElements(tile);
    }
}
