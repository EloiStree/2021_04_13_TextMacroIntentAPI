using System;
using System.Collections.Generic;
using System.Text;

namespace TextMacroIntent
{
    public class CommandLineAtTimeCollection<T> where T : class
    {

        EmiteHolderThatFinished m_dealWithReady;
        List<AtTriggerBean<T>> m_observedTimeTrigger = new List<AtTriggerBean<T>>();
        Queue<AtTriggerBean<T>> m_poolToRecycle = new Queue<AtTriggerBean<T>>();
        DateTime m_currenTime;

        public void AddAtReleasedListener(EmiteHolderThatFinished pushInRelayWhenReady)
        {
            m_dealWithReady += pushInRelayWhenReady;
        }
        public void RemoveAtReleasedListener(EmiteHolderThatFinished pushInRelayWhenReady)
        {
            m_dealWithReady -= pushInRelayWhenReady;
        }

        public void UnsubscribeAllHolderWithTimeout()
        {
            m_currenTime = DateTime.Now;

            for (int i = m_observedTimeTrigger.Count - 1; i >= 0; i--)
            {
                if (m_observedTimeTrigger[i].IsOutdated(m_currenTime))
                {
                    if (m_dealWithReady != null)
                        m_dealWithReady(m_observedTimeTrigger[i].GetHoldedInformation());
                    m_poolToRecycle.Enqueue(m_observedTimeTrigger[i]);
                    m_observedTimeTrigger.RemoveAt(i);
                }
            }
        }

        public void CheckHolderThatAreFinishSinceLastTime()
        {
            UnsubscribeAllHolderWithTimeout();
        }

        private AtTriggerBean<T> GetNotUsed()
        {
            if (m_poolToRecycle.Count > 0)
                return m_poolToRecycle.Dequeue();
            else return new AtTriggerBean<T>();
        }
        public void Add(DateTime timeToRelease, T holder)
        {
            AtTriggerBean<T> c = GetNotUsed();
            c.ResetWith(timeToRelease, holder);
            m_observedTimeTrigger.Add(c);
        }

        public int GetWaitingCount()
        {
            return m_observedTimeTrigger.Count;
        }
        public int GetPoolCount()
        {
            return m_poolToRecycle.Count;
        }

        public delegate void EmiteHolderThatFinished(T toEmit);
    }

}
