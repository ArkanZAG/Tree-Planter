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
using Utilities;

public class BiomeUIElement : MonoBehaviour
{
    [SerializeField] private Button button;

    [SerializeField] private Image image;

    [SerializeField] private ButtonScaleAnimation animation;

    [SerializeField] private BoolSpriteSwitcher boolSpriteSwitcher;

    private BiomeData biomeData;
    private BiomeType biomeType;
    private PlantUI plantUi;
    private Tile tile;

    public BiomeType Biome => biomeType;

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
        animation.StartAnimation();
        plantUi.SetBiome(biomeType);
        plantUi.SpawnElements(tile);
    }

    public void SetHighlighted(bool on) => boolSpriteSwitcher.Set(on);
}
