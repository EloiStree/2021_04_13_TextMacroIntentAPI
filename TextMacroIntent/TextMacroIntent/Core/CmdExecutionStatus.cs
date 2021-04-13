using System;
using System.Collections.Generic;
using System.Text;

namespace TextMacroIntent
{
    public class CmdExecutionStatus : I_ExecutionStatus
    {
        public bool m_finishExecuting;
        public bool m_succedToExecute ;
        public string m_errorInformation ;

      

        public CmdExecutionStatus()
        {
            m_finishExecuting = false;
            m_succedToExecute = false;
            m_errorInformation = null;
        }

        public void Reset()
        {
            m_succedToExecute = false;
            m_errorInformation = null;
            m_finishExecuting = false;
        }
        public string GetErrorInformation()
        {
            return m_errorInformation;
        }

        public bool HadError()
        {
            return m_errorInformation != null;
        }

        public bool HasFinish()
        {
            return m_finishExecuting;
        }

        public bool HasSucced()
        {
            return m_succedToExecute;
        }

        public void SetAsSucced()
        {
            m_finishExecuting = true; m_succedToExecute = true;
        }

        public void SetAsFail(string errorDescription = "")
        {
            m_errorInformation = errorDescription;
            SetAsFinish(false);
        }

        public void SetAsFinish(bool succed)
        {
            m_finishExecuting = true; m_succedToExecute = succed;
        }
    }
}
