using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI engineText;
    [SerializeField] TextMeshProUGUI scoreText;



    public void SetEngineStatus(bool IsEngineOn)
    {
        if (IsEngineOn) engineText.text = "ON";
        else engineText.text = "OFF";
    }
    
    public void SetScore(float score)
    {
        scoreText.text = String.Format("{0}", score);
    }
}
