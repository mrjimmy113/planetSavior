using UnityEngine;
namespace BuffSystem
{
    public abstract class ABuff : ScriptableObject
    {
        public float duration;

        public abstract TimedBuff InitializeBuff(GameObject obj);
    }
}