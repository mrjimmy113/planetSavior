using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private float sorce = 0;

    public void IncreaseScore(float amount)
    {
        sorce += amount;
    }

    public float GetSorce()
    {
        return sorce;
    }
}
