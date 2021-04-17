using System;
using System.Collections.Generic;
using System.Text;

namespace TextMacroIntent
{
    public class CommandLineRelayDefault : I_CommandLineRelay
    {
        public void Push(I_CommandLine commandLine)
        {
            if (commandLine == null)
                return;
            if (m_listeners != null)
                m_listeners(commandLine);
        }
        public void AddListener(OnPush listener) { m_listeners += listener;  }
        public void RemoveListener(OnPush listener) { m_listeners -= listener; }

        public void Push(I_CommandLineEnumList commandLines)
        {
            foreach (I_CommandLine item in commandLines.GetLines())
            {
                Push(item);
            }
        }

        OnPush m_listeners;
        public delegate void OnPush(I_CommandLine cmd);
    }
}
