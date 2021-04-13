using System;
using System.Collections.Generic;
using System.Text;

namespace TextMacroIntent
{
    public class CommandLine: I_CommandLine 
    {
        string m_command;

        public CommandLine(string command)
        {
            m_command = command;
        }

        public string GetLine()
        {
            return m_command;
        }
    }
}
