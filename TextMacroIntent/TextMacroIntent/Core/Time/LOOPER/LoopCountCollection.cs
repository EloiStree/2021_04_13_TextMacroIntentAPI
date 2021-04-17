using System;
using System.Collections.Generic;
using System.Text;

namespace TextMacroIntent
{
    public class LoopCountCollection<T> where T : class
    {

        public delegate void LoopEmition(T cmd);
        public LoopEmition m_finishCountdownListeners;


        List<LoopCountBean<T>> m_observeCountdown = new List<LoopCountBean<T>>();
        DateTime previous, current;

        public void AddLoopersListener(LoopEmition pushInRelayWhenReady)
        {
            m_finishCountdownListeners += pushInRelayWhenReady;
        }
        public void RemoveLoopersListener(LoopEmition pushInRelayWhenReady)
        {
            m_finishCountdownListeners -= pushInRelayWhenReady;
        }

        public void AddLoop(ref LoopCountBean<T> loop)
        {
            if(! m_observeCountdown.Contains(loop))
                m_observeCountdown.Add(loop);
        }
        public void RemoveLoop(ref LoopCountBean<T> loop)
        {
            m_observeCountdown.Remove(loop);

        }
        public void CreateLoop(uint millisecondDuration, T holdedValue, out LoopCountBean<T> createdLoop)
        {
            createdLoop = new LoopCountBean<T>(millisecondDuration, holdedValue);
            AddLoop(ref createdLoop);
        }

        public LoopCountCollection()
        {
            previous = current = DateTime.Now;
        }
        public void RemoveTimePastedToAllObservedAndEmit()
        {
            current = DateTime.Now;
            uint timePassed = (uint)((current - previous).TotalMilliseconds);
            if (timePassed <= 0)
                return;
            for (int i = 0; i < m_observeCountdown.Count; i++)
            {
                if (m_observeCountdown[i].IsActive())
                {
                     
                    m_observeCountdown[i].RemoveMillisecondPast(ref timePassed);
                    if (m_observeCountdown[i].HasNoTimeLeft())
                    {
                        m_observeCountdown[i].ResetTimerConsideringTimePast();
                        if (m_finishCountdownListeners != null)
                            m_finishCountdownListeners(m_observeCountdown[i].GetHoldedInformation());
                    }
                }
            }
            previous = current;
        }
      


        public void CheckHolderThatAreFinishSinceLastTime()
        {
            RemoveTimePastedToAllObservedAndEmit();
        }




    }
}
