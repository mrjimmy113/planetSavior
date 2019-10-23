using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuffSystem;

[CreateAssetMenu(menuName = "Buffs/LightBuff")]
public class LightBuff : ABuff
{

    public override TimedBuff InitializeBuff(GameObject obj)
    {
        return new TimedLightBuff(duration, this, obj);
    }
}
