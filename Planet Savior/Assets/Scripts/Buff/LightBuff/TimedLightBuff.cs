using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuffSystem;

public class TimedLightBuff : TimedBuff
{
    private LightBuff lightBuff;

    private PlayerController player;

    public TimedLightBuff(float duration, ABuff buff, GameObject obj) : base(duration, buff, obj)
    {
        lightBuff = (LightBuff)buff;
        player = obj.GetComponent<PlayerController>();
    }

    public override void Activate()
    {
        player.SetLightDecrease(0);
    }

    public override void End()
    {
        player.ResetLightDecrease();
    }
}
