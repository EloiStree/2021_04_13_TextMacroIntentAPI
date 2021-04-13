using System;
using System.Collections.Generic;
using System.Text;

namespace TextMacroIntent.Core
{
    public class CommandLineExecutorDefault : I_CommandLineExecutorFrequently
    {
        I_CommandAuctionDistributor m_auction;

        public CommandLineExecutorDefault(I_CommandAuctionDistributor auction)
        {
            m_auction = auction;
        }
        public void Execute(I_CommandLine commandLine )
        {
            bool found;
            I_Interpreter inter;
            m_auction.SeekForFirstTaker(commandLine, out found, out inter);
            if (found && inter!=null ) {
                I_ExecutionStatus status=null;
                inter.TranslateToActionsWithStatus(ref commandLine,ref status);
            }
        }




        public void Execute(string commandLine)
        {
            Execute(new CommandLine(commandLine));
        }

        public void Execute(string[] commandLine)
        {
            I_CommandLine[] list = new I_CommandLine[commandLine.Length];
            for (int i = 0; i < commandLine.Length; i++)
            {
                list[i]=new CommandLine(commandLine[i]);
            }
            Execute(list);
        }


        public void Execute(IEnumerable<I_CommandLine> commandLines)
        {
            foreach (I_CommandLine c in commandLines)
                Execute(c);
        }

        public void Execute(I_CommandLineEnumList commandLines)
        {
            Execute(commandLines.GetLines());
        }

        #region COMPILE PART
        public void Compile(string command)
        {
            throw new NotImplementedException();
        }

        public void Compile(I_CommandLine command)
        {
            throw new NotImplementedException();
        }

        public void Compile(I_CommandLineEnumList commandLines)
        {
            throw new NotImplementedException();
        }

        public void Compile(string command, out I_InterpretorCompiledAction compilationRegistered)
        {
            throw new NotImplementedException();
        }

        public void Compile(I_CommandLine command, out I_InterpretorCompiledAction compilationRegistered)
        {
            throw new NotImplementedException();
        }

        public void Compile(I_CommandLineEnumList commandLines, out I_InterpretorCompiledAction compilationRegistered)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
