using System;

namespace TextMacroIntent
{
    public class TextMacroIntentDefaultComplexDelayer : I_CommandLineComplexDelayExecutor
    {
        private I_CommandLineAuctionToExecutor m_executioner;
        I_CommandLineDelayExecutor m_delayer;

        public TextMacroIntentDefaultComplexDelayer(I_CommandLineAuctionToExecutor executioner,I_CommandLineDelayExecutor delayerSimple, out I_ThreadDependant threadComplex)
        {
            this.m_executioner = executioner;
            this.m_delayer = delayerSimple;
            threadComplex = null;
        }

        public void SendRicochet(I_CommandLine cmd, DateTime timeStart, params uint[] timeFromStartInMillisecond)
        {
            for (int i = 0; i < timeFromStartInMillisecond.Length; i++)
            {
                m_delayer.ExecuteAt(cmd,timeStart.AddMilliseconds( timeFromStartInMillisecond[i]));

            }
        }

        public void SendRicochetNow(I_CommandLine cmd, params uint[] timeFromStartInMilliseconds)
        {
            for (int i = 0; i < timeFromStartInMilliseconds.Length; i++)
            {
                m_delayer.ExecuteIn(cmd, timeFromStartInMilliseconds[i]);

            }
        }
    }
}