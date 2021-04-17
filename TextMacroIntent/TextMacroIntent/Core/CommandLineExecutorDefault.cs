using System;
using System.Collections.Generic;
using System.Text;

namespace TextMacroIntent
{
    public class CommandLineExecutorDefault : I_CommandLineDirectExecutor, I_CommandLineDirectExecutorWithReturn
    {
        I_CommandAuctionDistributor m_auction;

        public CommandLineExecutorDefault(I_CommandAuctionDistributor auction)
        {
            m_auction = auction;
        }
        public void Execute(I_CommandLine commandLine)
        {
            bool found;
            I_Interpreter inter;
            m_auction.SeekForFirstTaker(commandLine, out found, out inter);
            if (found && inter != null)
            {
                I_ParsingStatus status = null;
                inter.TranslateToActionsWithStatus(ref commandLine, ref status);
            }
        }

        public void Execute(I_CommandLine commandLine, out I_ParsingStatus parseStatus)
        {
            parseStatus = new ParsingExecutionStatus();
            bool found;
            I_Interpreter inter;
            m_auction.SeekForFirstTaker(commandLine, out found, out inter);
            if (found && inter != null)
            {
                parseStatus = new ParsingExecutionStatus();
                inter.TranslateToActionsWithStatus(ref commandLine, ref parseStatus);
            }
            else parseStatus.SetAsFail("Did not found the interpreter");
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

        public void Execute(string commandLine, out I_ParsingStatus exeStatus)
        {
            Execute(new CommandLine(commandLine), out exeStatus);
        }

        public void Execute(string[] commandLine, out I_ParsingStatus exeStatus)
        {
            List<I_CommandLine> cmds = new List<I_CommandLine>();
            for (int i = 0; i < commandLine.Length; i++)
            {
                cmds.Add(new CommandLine(commandLine[i]));
            }
            Execute(cmds, out exeStatus);
        }

       

        public void Execute(IEnumerable<I_CommandLine> commandLines, out I_ParsingStatus exeStatus)
        {
            I_ParsingStatus groupStatus = new ParsingExecutionStatus();
            exeStatus = groupStatus;
            I_ParsingStatus itemStatus = new ParsingExecutionStatus();
            foreach (I_CommandLine cmd in commandLines)
            {
                Execute(cmd, out itemStatus);
                if (itemStatus.HadError()) {
                    groupStatus.SetAsFail(itemStatus.GetErrorInformation());
                    return;
                }
            }

            groupStatus.SetAsFinish(true);
        }

        public void Execute(I_CommandLineEnumList commandLines, out I_ParsingStatus exeStatus)
        {
            Execute(commandLines.GetLines(), out exeStatus);
        }

      
    }
}
