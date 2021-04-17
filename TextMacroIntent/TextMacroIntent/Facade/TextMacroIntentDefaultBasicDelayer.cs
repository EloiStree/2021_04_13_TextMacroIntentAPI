using System;
using System.Collections.Generic;
using System.Text;
using TextMacroIntent.Facade;
using TextMacroIntent.Unstore;

namespace TextMacroIntent
{
    public class TextMacroIntentDefaultBasicDelayer : TextMacroIntentToAuctionExecutorRef ,  I_CommandLineDelayExecutor
    {
        I_CommandLineAuctionToExecutor m_executor;
        public  CommandRelayAtTime m_relayerAt;

        public TextMacroIntentDefaultBasicDelayer(I_CommandLineAuctionToExecutor executionManager , out I_ThreadDependant needToBeTakeInChargeByThread): base (executionManager) {


            CommandLineRelayDefault relay = new CommandLineRelayDefault();
            relay.AddListener(ExecuteFoundCommand);
            m_relayerAt = new CommandRelayAtTime(relay);

            m_executor = executionManager;

            //ThreadDependantGroup tg = new ThreadDependantGroup();
            //needToBeTakeInChargeByThread = tg;
            //tg.Add(relayAt);
            needToBeTakeInChargeByThread = m_relayerAt;
        }

        private void ExecuteFoundCommand(I_CommandLine cmd)
        {
            if (cmd != null)
                m_executor.Execute(cmd);
        }

      
        public void ExecuteAt(I_CommandLine commandLine, I_BlackBoxTime when)
        {   if (commandLine == null || when == null)
                return;
            if (ThrowExceptionIfEmpty(m_executor))
                ExecuteAt(commandLine, when.GetTime());
        }

        public void ExecuteAt(I_CommandLine commandLine, DateTime when)
        {
            if (commandLine == null || when == null)
                return;
            if (ThrowExceptionIfEmpty(m_executor))
                m_relayerAt.PushIn(when,commandLine );
        }

        public void ExecuteIn(I_CommandLine commandLine, float milliseconds)
        {
            if (commandLine == null )
                return;
            if (ThrowExceptionIfEmpty(m_executor))
                m_relayerAt.PushIn(DateTime.Now.AddMilliseconds(milliseconds), commandLine);
        }

       


        private bool ThrowExceptionIfEmpty(I_CommandLineAuctionToExecutor directExecutor)
        {
            if (directExecutor == null)
                throw new Exception("You forget to provide an delay executor");
            return true;
        }


    }
}
