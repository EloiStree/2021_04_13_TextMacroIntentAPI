using System;
using System.Collections.Generic;
using System.Text;

namespace TextMacroIntent
{
    public class LoopDateTimeBean<T> where T : class
    {
        long m_millisecondsLoopTime;
        bool m_loopIsActive;
        T m_holdInformation;
        DateTime p, n;
        DateTime startPoint;

        public void SetLoopAsActive(bool value)
        {
            m_loopIsActive = true;
            startPoint= p= n = DateTime.Now;
        }
        public LoopDateTimeBean()
        {
            m_holdInformation = null;
        }
        public LoopDateTimeBean(long millisecondsLoopDuration, T holdInformation)
        {
            m_millisecondsLoopTime = millisecondsLoopDuration;
            m_holdInformation = holdInformation;
        }

        public void ResetWith(long milliseconds, T command)
        {
            m_millisecondsLoopTime  = milliseconds;
            m_holdInformation = command;
        }

        public void RefreshAndCountPing(out int pingCount) {

            n = DateTime.Now;
            if (n == p) {
                pingCount = 0;
                return;
            }
            long ts = (long)(p-startPoint).TotalMilliseconds, te= (long)(n-startPoint).TotalMilliseconds;
            int cs = (int)(ts / m_millisecondsLoopTime) , ce = (int)(te / m_millisecondsLoopTime);
            pingCount = ce - cs;

            //Console.WriteLine(string.Format("{0} - {1} - {2} - {3} : {4}",ts,te,cs,ce, pingCount));

            p = n;
        }
       
        public bool IsActive()
        {
            return m_loopIsActive;
        }

        public T GetHoldedInformation() { return m_holdInformation; }
    }

}
