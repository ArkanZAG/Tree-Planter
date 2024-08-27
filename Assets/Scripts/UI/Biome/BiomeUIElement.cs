using System;
using System.Collections;
using System.Collections.Generic;
using Biomes;
using Data;
using GridSystem;
using Plants;
using UI.Plant;
using UnityEngine;
using UnityEngine.UI;

public class BiomeUIElement : MonoBehaviour
{
    [SerializeField] private Button button;

    [SerializeField] private PlantDatabase plantDataBase;

    [SerializeField] private Image image;

    private BiomeData biomeData;
    private BiomeType biomeType;
    private PlantUI plantUi;
    private Tile tile;

    private void Awake()
    {
        button.onClick.AddListener(OnClick);
    }
    

    public void Display(BiomeType biome, BiomeData data, PlantUI pUI, Tile tl)
    {
        biomeData = data;
        biomeType = biome;
        plantUi = pUI;
        tile = tl;

        image.sprite = data.BiomeIcon;
    }

    public void OnClick()
    {
        plantUi.SetBiome(biomeType);
        plantUi.SpawnElements(tile);
    }
}
