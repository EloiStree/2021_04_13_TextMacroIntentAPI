using System;
using System.Collections.Generic;
using System.Text;

namespace TextMacroIntent.Facade
{
    public  abstract class TextMacroIntentToAuctionExecutorRef : I_CommandLineAuctionToExecutor
    {
        I_CommandLineAuctionToExecutor m_executor;

        public TextMacroIntentToAuctionExecutorRef(I_CommandLineAuctionToExecutor executionManager )
        {

            m_executor = executionManager;
        }

        public void AddInterpreter(I_Interpreter interpretor)
        {
            if (m_executor != null)
                m_executor.AddInterpreter(interpretor);
        }

        public void Execute(string commandLine)
        {
            if (m_executor != null)
                m_executor.Execute(commandLine);
        }

        public void Execute(string[] commandLine)
        {
            if (m_executor != null)
                m_executor.Execute(commandLine);
        }
        public void Execute(I_CommandLine commandLine)
        {
            if (m_executor != null)
                m_executor.Execute(commandLine);
        }


        public void Execute(IEnumerable<I_CommandLine> commandLines)
        {
            if (m_executor != null)
                m_executor.Execute(commandLines);
        }

        public void Execute(I_CommandLineEnumList commandLines)
        {
            if (m_executor != null)
                m_executor.Execute(commandLines);
        }

      

        public void FindInterpreter(I_CommandLine command, out bool found, out I_Interpreter interpreter)
        {
            if (m_executor != null)
            {
                m_executor.FindInterpreter(command, out found, out interpreter);
                return;
            }
            found = false;
            interpreter = null;
        }

        public I_InterpretorCompiledAction GetCompiledAccessTo(I_CommandLine commandLine)
        {
            if (m_executor != null)
                return m_executor.GetCompiledAccessTo(commandLine);
            return null;
        }

        public void RemoveInterpreter(I_Interpreter interpretor)
        {
            if (m_executor != null)
                m_executor.RemoveInterpreter(interpretor);
            throw new NullReferenceException("Not executor found");
        }
    }
}
