using System;
using System.Collections.Generic;
using System.Text;

namespace TextMacroIntent
{
    public class LoopCountBean<T> where T : class
    {
        int m_millisecondsLoopTime;
        int m_millisecondsLeft;
        bool m_loopIsActive;
        T m_holdInformation;

        public void SetLoopAsActive(bool value) {
            m_loopIsActive = true;
        }
        public LoopCountBean()
        {
            m_millisecondsLeft = 0;
            m_holdInformation = null;
        }
        public LoopCountBean(uint millisecondsLoopDuration, T holdInformation)
        {
            m_millisecondsLeft = m_millisecondsLoopTime=(int)millisecondsLoopDuration;
            m_holdInformation = holdInformation;
        }

        public void ResetWith(uint milliseconds, T command)
        {
            m_millisecondsLoopTime= m_millisecondsLeft = (int)milliseconds;
            m_holdInformation = command;
        }

        public void RemoveMillisecondPast(ref uint time)
        {
            m_millisecondsLeft -= (int)time;
        }
        public bool HasNoTimeLeft() { return m_millisecondsLeft <= 0; }

        public bool HasTimeLeft()
        {
            return m_millisecondsLeft > 0;
        }

        public bool IsActive()
        {
            return m_loopIsActive;
        }

        public void ResetTimerConsideringTimePast()
        {
            if (m_millisecondsLeft < 0)
                m_millisecondsLeft = m_millisecondsLoopTime - m_millisecondsLeft;
            else
                m_millisecondsLeft = m_millisecondsLoopTime;
        }


        public void BrutalResetTimer()
        {
            m_millisecondsLeft = m_millisecondsLoopTime;
        }

        public T GetHoldedInformation() { return m_holdInformation; }
        public int GetTimeleftInMilliseconds() { return m_millisecondsLeft; }
    }
}
