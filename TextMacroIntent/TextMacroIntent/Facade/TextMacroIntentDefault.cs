using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TextMacroIntent;

namespace TextMacroIntent
{           
    ///BIG REMINDER FOR ME:
             ///I AM CREATING A PARSER NOT A EXECUTOR
             ///MEANING I CAN't WAIT THE PARSING EXECUTION BECAUSE
             ///IT IS NOT REFLECTION THE TIME TO EXECUTE BUT TO PARSE
             ///ALL THE RESPONABILITY OF TIME EXECUTING IS OUTSIDE THE PARSER AND SHOULD NOT BE DEAL BY THIS CLASSES.
             ///
    public class TextMacroIntentDefault : I_CommandLineAuctionToExecutor, I_CommandLineDirectExecutorWithReturn
    {

        I_CommandAuctionDistributor m_auctionHouse;
        I_CommandLineDirectExecutor m_directExecutor;
        I_CommandLineDirectExecutorWithReturn m_directExecutorWithReturn;
        public TextMacroIntentDefault()
        {
            m_auctionHouse = new CommandAuctionDistributor();
            m_directExecutor = new CommandLineExecutorDefault(m_auctionHouse);
            m_directExecutorWithReturn  = new CommandLineExecutorDefault(m_auctionHouse);



        }

        public I_CommandAuctionDistributor GetAuction() { return m_auctionHouse; }
        public I_CommandLineDirectExecutor GetDirectExecutor() { return m_directExecutor; }


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


        public void Execute(string commandLine)
        {
            if(ThrowExceptionIfEmpty(m_directExecutor))
            m_directExecutor.Execute(commandLine);
        }

        public void Execute(string[] commandLines)
        {
            if (ThrowExceptionIfEmpty(m_directExecutor))
                m_directExecutor.Execute(commandLines);
        }

        public void Execute(I_CommandLine commandLine)
        {
            if (ThrowExceptionIfEmpty(m_directExecutor))
                m_directExecutor.Execute(commandLine);
        }

        public void Execute(IEnumerable<I_CommandLine> commandLines)
        {
            if (ThrowExceptionIfEmpty(m_directExecutor))
                m_directExecutor.Execute(commandLines);
        }

        public void Execute(I_CommandLineEnumList commandLines)
        {
            if (ThrowExceptionIfEmpty(m_directExecutor))
                m_directExecutor.Execute(commandLines);
        }

        public bool SeekForFirstTaker(I_CommandLine command, out bool foundInterpreter, out I_Interpreter interpreter)
        {
            if (m_auctionHouse == null)
            {
                foundInterpreter = false;
                 interpreter  = null; 
                return false ;
            }
            return m_auctionHouse.SeekForFirstTaker(command, out foundInterpreter, out interpreter);
        }


        private bool ThrowExceptionIfEmpty(I_CommandLineDirectExecutor directExecutor)
        {
            if (directExecutor == null)
                throw new Exception("You forget to provide an executor");
            return true;
        }
        private bool ThrowExceptionIfEmpty(I_CommandLineDirectExecutorWithReturn directExecutor)
        {
            if (directExecutor == null)
                throw new Exception("You forget to provide an executor");
            return true;
        }

        public void Execute(string commandLine, out I_ParsingStatus exeStatus)
        {
            if (ThrowExceptionIfEmpty(m_directExecutorWithReturn))
                m_directExecutorWithReturn.Execute(commandLine,out exeStatus);
            else exeStatus = failBecauseEmpty;
        }

        public void Execute(string[] commandLine, out I_ParsingStatus exeStatus)
        {
            if (ThrowExceptionIfEmpty(m_directExecutorWithReturn))
                m_directExecutorWithReturn.Execute(commandLine, out exeStatus);
            else exeStatus = failBecauseEmpty;
        }

        public void Execute(I_CommandLine commandLine, out I_ParsingStatus exeStatus)
        {
            if (ThrowExceptionIfEmpty(m_directExecutorWithReturn))
                m_directExecutorWithReturn.Execute(commandLine, out exeStatus);
            else exeStatus = failBecauseEmpty;
        }

        public void Execute(IEnumerable<I_CommandLine> commandLines, out I_ParsingStatus exeStatus)
        {
            if (ThrowExceptionIfEmpty(m_directExecutorWithReturn))
                m_directExecutorWithReturn.Execute(commandLines, out exeStatus);
            else exeStatus = failBecauseEmpty;

        }

        public void Execute(I_CommandLineEnumList commandLines, out I_ParsingStatus exeStatus)
        {
            if (ThrowExceptionIfEmpty(m_directExecutorWithReturn))
                m_directExecutorWithReturn.Execute(commandLines, out exeStatus);
            else exeStatus = failBecauseEmpty;
        }

        private ParsingExecutionStatus failBecauseEmpty = new ParsingExecutionStatus("Failed to execute because of missing executor");
    }
}
