using System;
using System.Collections.Generic;
using System.Text;

namespace TextMacroIntent
{
    public class ParsingExecutionStatus : I_ParsingStatus
    {
        public bool m_finishParsing;
        public bool m_succedToParse ;
        public string m_errorInformation ;



        public ParsingExecutionStatus()
        {
            m_finishParsing = false;
            m_succedToParse = false;
            m_errorInformation = null;
        }
        public ParsingExecutionStatus(string failMessage)
        {
            m_finishParsing = false;
            m_succedToParse = false;
            m_errorInformation = null;
            SetAsFail(failMessage);
        }

        public void Reset()
        {
            m_succedToParse = false;
            m_errorInformation = null;
            m_finishParsing = false;
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
            return m_finishParsing;
        }

        public bool HasSucced()
        {
            return m_succedToParse;
        }

        public void SetAsSucced()
        {
            m_finishParsing = true; m_succedToParse = true;
        }

        public void SetAsFail(string errorDescription = "")
        {
            m_errorInformation = errorDescription;
            SetAsFinish(false);
        }

        public void SetAsFinish(bool succed)
        {
            m_finishParsing = true; m_succedToParse = succed;
        }
    }
}
