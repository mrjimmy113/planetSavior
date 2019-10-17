using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class LifeRocket : MonoBehaviour
{
    [SerializeField] float maxCapacity = 8;
    [SerializeField] TextMeshProUGUI capacityDisplay;
    float currentCapacity = 0;
    bool isFull = false;

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Player")
        {
            PlayerController controller = other.GetComponent<PlayerController>();
            if(currentCapacity + controller.GetCurrentCapacity() >= maxCapacity)
            {
                
                float remainCapcity = currentCapacity + controller.GetCurrentCapacity() - maxCapacity;
                controller.SetCurrentCapacity(remainCapcity);
                currentCapacity = 8;
                
                //Deploy Life Rocket
                GameObject.Destroy(gameObject);
            }
            else
            {
                currentCapacity += controller.GetCurrentCapacity();
                controller.SetCurrentCapacity(0);
            }

            capacityDisplay.text = String.Format("{0}", currentCapacity);

        }
    }
}
