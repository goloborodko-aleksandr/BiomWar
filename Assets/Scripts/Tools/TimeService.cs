namespace Tools
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Zenject;

    public class TimerService : ITickable
    {

        private readonly List<TimerData> timers = new();

        public void StartTimer(Action callback, float seconds)
        {
            timers.Add(new TimerData
            {
                EndTime = Time.time + seconds,
                Callback = callback
            });
        }

        private void Do()
        {
            float now = Time.time;
            for (int i = timers.Count - 1; i >= 0; i--)
            {
                var timer = timers[i];
                if (now >= timer.EndTime)
                {
                    timer.Callback?.Invoke();
                    timers.RemoveAt(i);
                }
            }
        }

        public void Tick()
        {
            if(timers.Count == 0) return;
            Do();
        }
    }
}