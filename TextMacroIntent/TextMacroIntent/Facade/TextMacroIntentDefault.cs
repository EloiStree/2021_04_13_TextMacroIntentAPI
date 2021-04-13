using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TextMacroIntent.Core;

namespace TextMacroIntent
{
    public class TextMacroIntentDefault
    {
        I_CommandAuctionDistributor m_auctionHouse;
        I_CommandLineDirectExecutor m_directExecutor;
        I_CommandLineWaiterExecutor m_waitingExecutor;

        public TextMacroIntentDefault()
        {
            m_auctionHouse = new CommandAuctionDistributor();
            m_directExecutor = new CommandLineExecutorDefault(m_auctionHouse);
            m_waitingExecutor = new CommandLineWaitingExecutorDefault(m_auctionHouse);
            
        }

        public I_CommandAuctionDistributor GetAuction() { return m_auctionHouse; }
        public I_CommandLineDirectExecutor GetDirectExecutor() { return m_directExecutor; }
        public I_CommandLineWaiterExecutor GetWaitingExecutor() { return m_waitingExecutor; }


        public void AddInterpreter(I_Interpreter rubix)
        {
            if (m_auctionHouse != null)
                m_auctionHouse.AddInterpreter(rubix);
        }
        public void RemoveInterpreter(I_Interpreter rubix)
        {
            if (m_auctionHouse != null)
                m_auctionHouse.RemoveInterpreter(rubix);
        }

        public void FindInterpreter(I_CommandLine command,out bool found, out I_Interpreter interpreter) {

            m_auctionHouse.SeekForFirstTaker(command, out found, out interpreter);
        }

        public I_InterpretorCompiledAction GetCompiledAccessTo(I_CommandLine commandLine)
        {
            bool found;
            I_Interpreter interpreter;
            m_auctionHouse.SeekForFirstTaker(commandLine, out found, out interpreter);
            if (found && interpreter != null) {
                return interpreter.TryToGetCompiledAction(commandLine);
            }
            return null;
        }




        //public void TriggerRefreshThreads() {

        //    ThreadPool.QueueUserWorkItem(TimeSensitiveRefresh, (ushort)1);
        //}

        //private void TimeSensitiveRefresh(object timeToWaitObject)
        //{
        //    int timeToWait= (int) timeToWaitObject;
        //    while (true) {

        //        Thread.Sleep(timeToWait);
        //    }
        //}
    }
}
