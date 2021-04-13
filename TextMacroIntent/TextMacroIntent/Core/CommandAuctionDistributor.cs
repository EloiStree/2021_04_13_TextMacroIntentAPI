using System;
using System.Collections.Generic;
using System.Text;

namespace TextMacroIntent
{
    public class CommandAuctionDistributor : I_CommandAuctionDistributor
    {

        public List<I_Interpreter> m_interpreters = new List<I_Interpreter>();

        public CommandLineInterpreterCall m_interpreterFound;
        public CommandLineCall m_commandLineNotTakeInCharge;

        public void AddInterpreter(I_Interpreter interpreter)
        {
            m_interpreters.Add(interpreter);
        }
        public void RemoveInterpreter(I_Interpreter interpreter)
        {
            m_interpreters.Remove(interpreter);
        }
        public bool SeekForFirstTaker(string command , out bool foundInterpreter, out I_Interpreter taker)
        {
            return SeekForFirstTaker(new CommandLine(command), out foundInterpreter, out taker);
        }

        public bool SeekForFirstTaker(I_CommandLine command, out bool foundInterpreter, out I_Interpreter interpreter)
        {
            foundInterpreter = false;
            interpreter = null;
            for (int i = 0; i < m_interpreters.Count; i++)
            {
                if (m_interpreters[i].CanInterpreterUnderstand(ref command))
                {

                    foundInterpreter = true;
                    interpreter = m_interpreters[i];
                    if (m_interpreterFound != null)
                        m_interpreterFound(interpreter, command);
                    break;
                }
            }


            if (!foundInterpreter) { 
                if(m_commandLineNotTakeInCharge!=null)
                m_commandLineNotTakeInCharge(command);
            }
            return foundInterpreter;
        }

    }

  
}
