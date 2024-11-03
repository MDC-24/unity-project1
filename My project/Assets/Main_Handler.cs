using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Main_Handler : MonoBehaviour
{
    
    [SerializeField] private TMP_Text day_TEXT;
    [SerializeField] private TMP_Text gold_TEXT;

    private int gold;
    void Start()
    {
        gold = 0;
    }

    
    void Update()
    {
        
    }

    public void Manual_Mine_Button()
    {
        gold += 1;
        gold_TEXT.text = "Gold: " + gold;
    }
    
}
