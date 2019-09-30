using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSaver : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Player");
        }
    }
}
