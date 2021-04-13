using System;
using System.Collections.Generic;
using System.Text;

namespace TextMacroIntent
{

    public class InCountBean<T> where T : class
    {
         int m_millisecondsLeft;
         T m_holdInformation;

        public InCountBean()
        {
            m_millisecondsLeft = 0;
            m_holdInformation = null;
        }
        public InCountBean(uint millisecondsLeft, T holdInformation)
        {
            m_millisecondsLeft = (int)millisecondsLeft;
            m_holdInformation = holdInformation;
        }

        public void ResetWith(uint milliseconds, T command)
        {
            m_millisecondsLeft = (int)milliseconds;
            m_holdInformation = command;
        }

        public void RemoveMillisecondPast(ref uint time)
        {
            m_millisecondsLeft -= (int)time;
        }
        public bool HasNoTimeLeft() { return m_millisecondsLeft <= 0; }


        public T GetHoldedInformation() { return m_holdInformation; }
        public int GetTimeleftInMilliseconds() { return m_millisecondsLeft; }
    }
}
