using System;
using System.Collections;
using System.Collections.Generic;
using Controller;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArchieveUIElement : MonoBehaviour
{
    [SerializeField] private Button button;

    [SerializeField] private TextMeshProUGUI levelText;
    
    
    private void Awake()
    {
        button.onClick.AddListener(OnClick);
    }


    public void Display(GameController gameCont)
    {
        
    }

    private void OnClick()
    {
        
    }
}
