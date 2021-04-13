using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextMacroIntent
{
    public class CommandRelayByTimeSubstraction
    {
         I_CommandLineRelay m_relayListening;
         InCountCollection<I_CommandLine> m_commandInHold = new InCountCollection<I_CommandLine>();

        public CommandRelayByTimeSubstraction(I_CommandLineRelay relay)
        { 
            m_relayListening = relay;
            m_commandInHold.AddAtReleasedListener( RelayFollowingCommand);
        }

        public void SetCommandRelayer(I_CommandLineRelay relay) { m_relayListening = relay; }
        private void RelayFollowingCommand(I_CommandLine cmd)
        {
            if (m_relayListening != null)
                m_relayListening.Push(cmd);
        }

        public override string ToString()
        {
            return "InWaiting... " + m_commandInHold.GetWaitingCount() + "/" + m_commandInHold.GetPoolCount();
        }
        public  string InWaitingDescription()
        {
            return ToString() +" -> "+ m_commandInHold;
        }



        /// <summary>
        /// Check will 
        /// </summary>
        public void CheckHolderThatAreFinishSinceLastTime()
        {
            m_commandInHold.CheckHolderThatAreFinishSinceLastTime();
        }
        public void AddCmdCountdownIn(uint millisecondsToStart, I_CommandLine command, bool includeFirstTime, params uint[] ricochetInMilliseconds)
        {
            if (includeFirstTime)
                m_commandInHold.Add(millisecondsToStart, command);

            foreach (uint t in ricochetInMilliseconds)
            {
                m_commandInHold.Add(millisecondsToStart+t, command);
            }
        }

        public void AddCmdCountdownIn(uint milliseconds, string command)
        {
            AddCmdCountdownIn(milliseconds, new CommandLine(command));
        }
        public void AddCmdCountdownIn(uint milliseconds, I_CommandLine command)
        {
            m_commandInHold.Add(milliseconds, command);
        }

    }
}
