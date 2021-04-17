using System;
using System.Collections.Generic;
using System.Text;

namespace TextMacroIntent
{
    public class CommandLineWaitingExecutorDefault : I_CommandLineDelayExecutor, I_ThreadDependant
    {
        private I_CommandAuctionDistributor m_auctionHouse;

        public CommandLineWaitingExecutorDefault(I_CommandAuctionDistributor auctionHouse)
        {
            this.m_auctionHouse = auctionHouse;
        }

        public void ExecuteAt(I_CommandLine commandLine, I_BlackBoxTime when)
        {
            throw new NotImplementedException();
        }

        public void ExecuteAt(I_CommandLine commandLine, DateTime when)
        {
            throw new NotImplementedException();
        }

        public void ExecuteAt(I_CommandLine commandLine, out I_ParsingStatus exeStatus, I_BlackBoxTime when)
        {
            throw new NotImplementedException();
        }

        public void ExecuteAt(I_CommandLine commandLine, out I_ParsingStatus exeStatus, DateTime when)
        {
            throw new NotImplementedException();
        }

        public void ExecuteIn(I_CommandLine commandLine, float milliseconds)
        {
            throw new NotImplementedException();
        }

        public void ExecuteIn(I_CommandLine commandLine, out I_ParsingStatus exeStatus, float milliseconds)
        {
            throw new NotImplementedException();
        }

        public void ToIncludeInLoopThreadToWork()
        {
            throw new NotImplementedException();
        }
    }


    /// PREVIOUS UNITY VERSION OF THIS
    /*
     * 
      public class RunningCmd
{
    private ICommandLine cmd;
    private ExecutionStatus status;
    private Coroutine coroutine;

    public RunningCmd(ICommandLine cmd, ExecutionStatus status, Coroutine coroutine)
    {
        this.cmd = cmd;
        this.status = status;
        this.coroutine = coroutine;
    }

    public bool IsFinish()
    {
        return status.HasFinish();
    }

    public Coroutine GetCoroutineToStop() { return coroutine; }
}
     * 
     * 
     * 
     * 
     * 
     * 
    public class CommandAuctionCoroutineExecuter : MonoBehaviour
    {

        public CommandAuctionDistributor m_auction;
        #region Previously
        public List<RunningCmd> m_running = new List<RunningCmd>();
        public int m_runningNumber;
        public long m_executed;



        //public void TryToExecutreWithoutStatus(string cmdLine)
        //{
        //    CommandLine cmd = new CommandLine(cmdLine);
        //    ExecutionStatus status = new ExecutionStatus();
        //    m_running.Add(
        //        new RunningCmd(cmd, status, StartCoroutine(ExecuteCMD(cmd, status))));
        //}

        //public void TryToExecutreWithoutSatus(ICommandLine cmd )
        //{
        //    ExecutionStatus status = new ExecutionStatus();
        //    m_running.Add(
        //        new RunningCmd(cmd, status, StartCoroutine(ExecuteCMD(cmd, status))));
        //}
        public void TryToExecutre(ICommandLine cmd, out ExecutionStatus status)
        {
            status = new ExecutionStatus();
            m_running.Add(
                new RunningCmd(cmd, status, StartCoroutine(ExecuteCMD(cmd, status))));
        }

        private IEnumerator ExecuteCMD(ICommandLine cmd, ExecutionStatus status)
        {

            IInterpreter taker;
            if (m_auction.SeekForFirstTaker(cmd.GetLine(), out taker))
            {
                taker.TranslateToActionsWithStatus(ref cmd, ref status);
                yield return new WaitWhile(() => !status.HasFinish());
            }
            else status.StopWithError("Did not find a take for the commmand.");
            yield break;
        }

        private void Update()
        {
            m_runningNumber = m_running.Count;
            for (int i = m_running.Count - 1; i >= 0; i--)
            {
                if (m_running[i].IsFinish())
                {
                    Coroutine c = m_running[i].GetCoroutineToStop();
                    if (c != null)
                        StopCoroutine(c);
                    m_running.RemoveAt(i);
                    m_executed++;
                }
            }
        }
        #endregion

    }
    */

}
