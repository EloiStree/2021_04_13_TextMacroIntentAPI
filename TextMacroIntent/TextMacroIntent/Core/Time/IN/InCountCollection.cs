using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextMacroIntent
{

    public class InCountCollection<T> where T : class
    {

        public EmitecountDownThatFinished m_finishCountdownListeners;
        
        List<InCountBean<T>> m_observeCountdown = new List<InCountBean<T>>();
        Queue<InCountBean<T>> m_poolToRecycle = new Queue<InCountBean<T>>();
        DateTime previous, current;

        public void AddAtReleasedListener(EmitecountDownThatFinished pushInRelayWhenReady)
        {
            m_finishCountdownListeners += pushInRelayWhenReady;
        }
        public void RemoveAtReleasedListener(EmitecountDownThatFinished pushInRelayWhenReady)
        {
            m_finishCountdownListeners -= pushInRelayWhenReady;
        }



        public InCountCollection()
        {
            previous = current = DateTime.Now;
        }
        public void RemoveTimePastedToAllObserved()
        {
            current = DateTime.Now;
            uint t = (uint)((current - previous).TotalMilliseconds);
            if (t <= 0)
                return;
            for (int i = 0; i < m_observeCountdown.Count; i++)
            {
                m_observeCountdown[i].RemoveMillisecondPast(ref t);
            }
            previous = current;
        }

        public void UnsubscribeAllHolderWithTimeout()
        {
            for (int i = m_observeCountdown.Count - 1; i >= 0; i--)
            {
                if (m_observeCountdown[i].HasNoTimeLeft())
                {
                    if (m_finishCountdownListeners != null)
                        m_finishCountdownListeners(m_observeCountdown[i].GetHoldedInformation());
                    m_poolToRecycle.Enqueue(m_observeCountdown[i]);
                    m_observeCountdown.RemoveAt(i);
                }
            }
        }

        public void CheckHolderThatAreFinishSinceLastTime()
        {
            RemoveTimePastedToAllObserved();
            UnsubscribeAllHolderWithTimeout();
        }

        private InCountBean<T> GetNotUsed()
        {
            if (m_poolToRecycle.Count > 0)
                return m_poolToRecycle.Dequeue();
            else return new InCountBean<T>();
        }
        public void Add(uint milliseconds, T cmd)
        {
            InCountBean<T> c = GetNotUsed();
            c.ResetWith(milliseconds, cmd);
            m_observeCountdown.Add(c);
        }

        public int GetWaitingCount()
        {
            return m_observeCountdown.Count;
        }
        public int GetPoolCount()
        {
            return m_poolToRecycle.Count;
        }

        public override string ToString()
        {
            return string.Join(" ", m_observeCountdown.Select(k => k.GetTimeleftInMilliseconds()).ToArray());
        }
        public delegate void EmitecountDownThatFinished(T cmd);
    }

}
