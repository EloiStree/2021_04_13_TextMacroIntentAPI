using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TextMacroIntent
{
    public class TextMacroIntentWithCSThread 
    {

        public Int16 m_timeBetweenFrame;
        public Thread m_timer;
        public bool m_keepThreadAlive;

        public TextMacroIntentDefault m_auctionExecutorDefault;

        I_ThreadDependant m_threadBasic;
        public TextMacroIntentDefaultBasicDelayer m_basicDelayer;

        I_ThreadDependant m_threadComplex;
        public TextMacroIntentDefaultComplexDelayer m_complexDelayer;

        public TextMacroIntentWithCSThread( out I_TextMacroInputAll access, Int16 timeBetween = 1)
        {
            m_timeBetweenFrame = timeBetween;
            m_auctionExecutorDefault = new TextMacroIntentDefault();
            m_basicDelayer = new TextMacroIntentDefaultBasicDelayer(m_auctionExecutorDefault, out m_threadBasic);
            m_complexDelayer = new TextMacroIntentDefaultComplexDelayer(m_auctionExecutorDefault, m_basicDelayer, out m_threadComplex);
            access = new TextMacroInputAll(m_auctionExecutorDefault, m_basicDelayer, m_complexDelayer);


            m_keepThreadAlive = true;
            m_timer = new Thread(TimeCheckCoroutine);
            m_timer.Start();
        }
        ~TextMacroIntentWithCSThread()
        {
            m_keepThreadAlive = false;
            if (m_timer!=null && m_timer.IsAlive)
                m_timer.Abort();
        }
        private void TimeCheckCoroutine()
        {
            while (m_keepThreadAlive) {

                if(m_threadBasic != null)
                    m_threadBasic.ToIncludeInLoopThreadToWork();

                if (m_threadComplex != null)
                    m_threadComplex.ToIncludeInLoopThreadToWork();

                Thread.Sleep(m_timeBetweenFrame);
            }
        }


       
    }
}
