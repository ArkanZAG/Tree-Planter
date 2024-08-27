using System;
using System.Collections;
using System.Collections.Generic;
using Controller;
using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArchiveUIElement : MonoBehaviour
{
    [SerializeField] private Button button;

    [SerializeField] private TextMeshProUGUI levelText;

    private GameController gameController;
    private GridController gridController;
    private GridData gridData;
    private void Awake()
    {
        button.onClick.AddListener(OnClick);
    }
    
    public void Display(GridData data, GameController gameCont, GridController gridCont)
    {
        gameController = gameCont;
        gridController = gridCont;
        gridData = data;

        levelText.text = data.name;
    }

    private void OnClick()
    {
        gridController.Generate(gridData);
    }
}
