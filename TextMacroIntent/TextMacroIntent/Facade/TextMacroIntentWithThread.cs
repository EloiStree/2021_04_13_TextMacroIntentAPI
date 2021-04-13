using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TextMacroIntent
{
    public class TextMacroIntentWithThread : TextMacroIntentDefaultWithHolders { 

        public Int16 m_timeBetweenFrame;
        public Thread m_timer;
        public bool m_keepThreadAlive;
        public TextMacroIntentWithThread( ThreadPriority priority= ThreadPriority.AboveNormal)
        {
            m_keepThreadAlive = true;
            Console.WriteLine("Text Macro Input  Start");
            m_timer = new Thread(TimeCheckCoroutine);
            m_timer.Priority = priority;
            m_timer.Start();
        }
        ~TextMacroIntentWithThread()
        {

            m_keepThreadAlive = false;
            Console.WriteLine("Text Macro Input Stop");
        }

        public void TimeCheckCoroutine() {

            while (m_keepThreadAlive) {

                base.ChecktimeEveryMilliseconds();
                Thread.Sleep(m_timeBetweenFrame);
            }

        }
    }
}
