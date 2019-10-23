using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using BuffSystem;

public class LifeRocket : MonoBehaviour
{
    [SerializeField] float maxCapacity = 8;
    [SerializeField] TextMeshProUGUI capacityDisplay;
    [SerializeField] LightBuff lightBuff;
    float currentCapacity = 0;
    bool isFull = false;

    private void OnTriggerEnter(Collider other)
    {
        CollectPeople(other.gameObject);
        
    }

    private void OnTriggerStay(Collider other)
    {
        RefillLight(other.gameObject);
    }

    private void RefillLight(GameObject player)
    {
        if(player.tag == "Player")
        {
            BuffableEntity buffs = player.GetComponent<BuffableEntity>();
            PlayerController control = player.GetComponent<PlayerController>();
            buffs.AddBuff(new TimedLightBuff(lightBuff.duration, lightBuff, player.gameObject));
            control.ReFillLight();
        }
    }

    private void CollectPeople(GameObject player)
    {
        if (player.tag == "Player")
        {
            PlayerController controller = player.GetComponent<PlayerController>();
            if (currentCapacity + controller.GetCurrentCapacity() >= maxCapacity)
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
