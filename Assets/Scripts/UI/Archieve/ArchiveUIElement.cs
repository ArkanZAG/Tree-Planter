using System;
using System.Collections;
using System.Collections.Generic;
using Controller;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArchiveUIElement : MonoBehaviour
{
    [SerializeField] private Button button;

    [SerializeField] private TextMeshProUGUI levelText;

    private GameController gameController;
    private GridController gridController;
    private void Awake()
    {
        button.onClick.AddListener(OnClick);
    }
    
    public void Display(GameController gameCont, GridController gridCont)
    {
        gameController = gameCont;
        gridController = gridCont;

        for (int i = 0; i < gameCont.GetAllGridData().Length; i++)
        {
            
        }
    }

    private void OnClick()
    {
        
    }
}
