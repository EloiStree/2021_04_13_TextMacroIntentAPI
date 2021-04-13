using System;
using System.Collections.Generic;
using System.Text;

namespace TextMacroIntent
{
    public class CommandRelayAtTime
    {
         I_CommandLineRelay m_relay;
         CommandLineAtTimeCollection<I_CommandLine> m_waitingCollection = new CommandLineAtTimeCollection<I_CommandLine>();

        public CommandRelayAtTime(I_CommandLineRelay relay)
        {
            m_relay = relay;
            m_waitingCollection.AddAtReleasedListener( PushInRelayWhenReady);

        }
        public void SetRelay(I_CommandLineRelay relay) { m_relay = relay; }
        private void PushInRelayWhenReady(I_CommandLine cmd)
        {
            if (m_relay != null)
                m_relay.Push(cmd);
        }  
       
        public void CheckHolderThatAreFinishSinceLastTime()
        {
            m_waitingCollection.CheckHolderThatAreFinishSinceLastTime();
        }

        public void PushInWithRicochet(DateTime dateTime, I_CommandLine cmd, bool includeFirstTime, params uint[] ricochetInMilliseconds)
        {
            if(includeFirstTime)
                m_waitingCollection.Add(dateTime, cmd);
            
            foreach (uint t in ricochetInMilliseconds)
            {
                m_waitingCollection.Add(dateTime.AddMilliseconds(t), cmd);
            }
        }
        public void PushIn(DateTime dateTime, I_CommandLine cmd)
        {
            m_waitingCollection.Add(dateTime, cmd);
        }
       
        public void PushIn(I_BlackBoxTime dateTime, I_CommandLine cmd)
        {
            PushIn(dateTime.GetTime(), cmd);
        }
        public void PushIn(DateTime dateTime, string cmd)
        {
            PushIn(dateTime, new CommandLine(cmd));
        }

        public void PushIn(I_BlackBoxTime lune, string cmd)
        {
            PushIn(lune.GetTime(), new CommandLine(cmd));
        }
    }

    
    

    
}
