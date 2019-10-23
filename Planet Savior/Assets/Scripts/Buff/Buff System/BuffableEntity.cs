using UnityEngine;
using System.Collections.Generic;

namespace BuffSystem
{
    public class BuffableEntity : MonoBehaviour
    {

        public List<TimedBuff> CurrentBuffs = new List<TimedBuff>();

        void Update()
        {
            foreach (TimedBuff buff in CurrentBuffs.ToArray())
            {
                buff.Tick(Time.deltaTime);
                if (buff.IsFinished)
                {
                    CurrentBuffs.Remove(buff);
                }
            }
        }

        public void AddBuff(TimedBuff buff)
        {
            CurrentBuffs.Add(buff);
            buff.Activate();
        }
    }
}