using System;
using System.Collections.Generic;
using System.Text;

namespace TextMacroIntent.Unstore
{
    public class DelayDateTime : I_BlackBoxTime
    {
        private long m_delayInMilliseconds;
        public DateTime m_resultingDateTime;

        public DelayDateTime(long delayInMilliseconds)
        {
            m_delayInMilliseconds = delayInMilliseconds;
        }

        public DelayDateTime(long delayInMilliseconds, DateTime origineTime) : this(delayInMilliseconds)
        {
            m_delayInMilliseconds = delayInMilliseconds;
            SetWith(origineTime);
        }

        public void SetWith(DateTime origineTime) {
            m_resultingDateTime= origineTime.AddMilliseconds(m_delayInMilliseconds);
        }

        public DateTime GetTime()
        {
            return m_resultingDateTime;
        }
    }
}
