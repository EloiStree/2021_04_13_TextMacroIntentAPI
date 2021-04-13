using System;
using System.Collections.Generic;
using System.Text;

namespace TextMacroIntent
{
    public class LoopDateTimeCollection <T> where T : class
    {

        public delegate void LoopEmition(T holdValue);
        public LoopEmition m_finishCountdownListeners;


        List<LoopDateTimeBean<T>> m_observeCountdown = new List<LoopDateTimeBean<T>>();
        DateTime previous, current;

        public void AddLoopersListener(LoopEmition pushInRelayWhenReady)
        {
            m_finishCountdownListeners += pushInRelayWhenReady;
        }
        public void RemoveLoopersListener(LoopEmition pushInRelayWhenReady)
        {
            m_finishCountdownListeners -= pushInRelayWhenReady;
        }

        public void AddLoop(ref LoopDateTimeBean<T> loop)
        {
            if (!m_observeCountdown.Contains(loop))
                m_observeCountdown.Add(loop);
        }
        public void RemoveLoop(ref LoopDateTimeBean<T> loop)
        {
            m_observeCountdown.Remove(loop);
        }
        public void CreateLoop(uint millisecondDuration, T holdedValue, out LoopDateTimeBean<T> createdLoop)
        {
            createdLoop = new LoopDateTimeBean<T>(millisecondDuration, holdedValue);
            AddLoop(ref createdLoop);
        }

        public LoopDateTimeCollection()
        {
            previous = current = DateTime.Now;
        }
        public void CheckHolderThatAreFinishSinceLastTime()
        {
            current = DateTime.Now;
            double timePassed = (current - previous).TotalMilliseconds;
            if (timePassed < 1)
                return;

            for (int i = 0; i < m_observeCountdown.Count; i++)
            {
                if (m_observeCountdown[i].IsActive())
                {
                    int count ;
                    m_observeCountdown[i].RefreshAndCountPing(out count);
                    if (count > 0)
                    {
                        if (m_finishCountdownListeners != null)
                        {
                            while (count > 0) { 
                                m_finishCountdownListeners(m_observeCountdown[i].GetHoldedInformation());
                                count--;
                            }
                        }
                      
                    }
                }
            }
            previous = current;
        }




    }
}
