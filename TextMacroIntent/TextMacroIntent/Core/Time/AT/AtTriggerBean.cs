using System;
using System.Collections.Generic;
using System.Text;

namespace TextMacroIntent
{
    public class AtTriggerBean<T> where T:class
    {
         DateTime m_triggerAt;
         T m_holdInformation; 
        public AtTriggerBean()
        {
            m_triggerAt = DateTime.Now;
            m_holdInformation = null;
        }
        public AtTriggerBean(DateTime triggerAt, T holdInformation)
        {
            m_triggerAt = triggerAt;
            m_holdInformation = holdInformation;
        }

        public bool IsOutdated(DateTime current)
        {
            return m_triggerAt <= current;
        }

        public void ResetWith(DateTime time, T holded)
        {
            m_triggerAt = time;
            m_holdInformation = holded;
        }
        public T GetHoldedInformation() { return m_holdInformation; }
        public DateTime GetTimeToTrigger() { return m_triggerAt; }

    }
}
