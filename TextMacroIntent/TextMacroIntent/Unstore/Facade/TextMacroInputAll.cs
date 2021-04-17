using System;
using System.Collections.Generic;
using System.Text;

namespace TextMacroIntent
{
    public class TextMacroInputAll : I_TextMacroInputAll
    {
        public TextMacroIntentDefault m_auctionExecutorDefault;
        public TextMacroIntentDefaultBasicDelayer m_basicDelayer;
        public TextMacroIntentDefaultComplexDelayer m_complexDelayer;

        public TextMacroInputAll(
            TextMacroIntentDefault auctionExecutorDefault,
            TextMacroIntentDefaultBasicDelayer basicDelayer,
            TextMacroIntentDefaultComplexDelayer complexDelayer)
        {
            m_auctionExecutorDefault = auctionExecutorDefault;
            m_basicDelayer = basicDelayer;
            m_complexDelayer = complexDelayer;
        }

      

        public void Execute(string commandLine)
        {
            m_auctionExecutorDefault.Execute(commandLine);
        }

        public void Execute(string[] commandLine)
        {
            m_auctionExecutorDefault.Execute(commandLine);
        }

        public void Execute(I_CommandLine commandLine)
        {
            m_auctionExecutorDefault.Execute(commandLine);
        }

        public void Execute(IEnumerable<I_CommandLine> commandLines)
        {
            m_auctionExecutorDefault.Execute(commandLines);
        }

        public void Execute(I_CommandLineEnumList commandLines)
        {
            m_auctionExecutorDefault.Execute(commandLines);
        }

        public void Execute(string commandLine, out I_ParsingStatus exeStatus)
        {
            m_auctionExecutorDefault.Execute(commandLine, out  exeStatus);
        }

        public void Execute(string[] commandLine, out I_ParsingStatus exeStatus)
        {
            m_auctionExecutorDefault.Execute(commandLine, out  exeStatus);
        }

        public void Execute(I_CommandLine commandLine, out I_ParsingStatus exeStatus)
        {
            m_auctionExecutorDefault.Execute(commandLine, out  exeStatus);
        }

        public void Execute(IEnumerable<I_CommandLine> commandLines, out I_ParsingStatus exeStatus)
        {
            m_auctionExecutorDefault.Execute(commandLines, out  exeStatus);
        }

        public void Execute(I_CommandLineEnumList commandLines, out I_ParsingStatus exeStatus)
        {
            m_auctionExecutorDefault.Execute(commandLines, out  exeStatus);
        }

        public void ExecuteAt(I_CommandLine commandLine, I_BlackBoxTime when)
        {
            m_basicDelayer.ExecuteAt(commandLine,  when);
        }

        public void ExecuteAt(I_CommandLine commandLine, DateTime when)
        {
            m_basicDelayer.ExecuteAt(commandLine,  when);
        }

        public void ExecuteIn(I_CommandLine commandLine, float milliseconds)
        {
            m_basicDelayer.ExecuteIn(commandLine,  milliseconds);
        }
        public void AddInterpreter(I_Interpreter interpreter)
        {
            m_auctionExecutorDefault.AddInterpreter(interpreter);
        }
        public void RemoveInterpreter(I_Interpreter interpreter)
        {
            m_auctionExecutorDefault.RemoveInterpreter(interpreter);
        }

        public bool SeekForFirstTaker(I_CommandLine command, out bool foundInterpreter, out I_Interpreter interpreter)
        {
            return m_auctionExecutorDefault.SeekForFirstTaker(command, out foundInterpreter, out interpreter);
        }

        public void SendRicochet(I_CommandLine cmd, DateTime timeStart, params uint[] timeFromStart)
        {
            m_complexDelayer.SendRicochet(cmd,  timeStart,  timeFromStart);
        }

        public void SendRicochetNow(I_CommandLine cmd, params uint[] timeFromStart)
        {
            m_complexDelayer.SendRicochetNow(cmd, timeFromStart);
        }

        
    }
}
