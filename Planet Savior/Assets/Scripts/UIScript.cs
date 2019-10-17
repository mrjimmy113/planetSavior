using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI capacity;

    
    public void SetScore(float score)
    {
        scoreText.text = String.Format("{0}", score);
    }

    public void SetCapacity(float capacity)
    {
        this.capacity.text = String.Format("{0}/{1}", capacity, 8);
    }
}
