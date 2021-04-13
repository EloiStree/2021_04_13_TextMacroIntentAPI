using System;
using System.Collections.Generic;
using System.Text;

namespace TextMacroIntent
{
    public class Interpreter_ConsoleDebugger : AbstractInterpreter
    {
        public string m_debugStartId= "debuglog ";
         MessageToDebug m_messageToDebug;

        public void AddConsoleMessageDebugger(MessageToDebug listener) { m_messageToDebug += listener; }
        public void RemoveConsoleMessageDebugger(MessageToDebug listener) { m_messageToDebug -= listener; }

        public override bool CanInterpreterUnderstand(ref I_CommandLine command)
        {
            return StartWithDebug(command);
        }

        private bool StartWithDebug(I_CommandLine command)
        {
            return StartWith(command.GetLine(), m_debugStartId, true, true);
        }

        public override void TranslateToActionsWithStatus(ref I_CommandLine command, ref I_ExecutionStatus succedToExecute)
        {


            bool parsed;
            string message;
            GetMessageFrom(command, out parsed, out message);

            if (parsed && !string.IsNullOrEmpty(message)) { 
                if(succedToExecute!=null)
                    succedToExecute.SetAsFinish(true);
                if (m_messageToDebug != null) { 
                    m_messageToDebug(message);
                }
            }

            if (succedToExecute != null)
                succedToExecute.SetAsFail("Did not succed to parse|" + command.GetLine());


        }

        private void GetMessageFrom(I_CommandLine command, out bool parsed, out string message)
        {
            message = "";
            parsed = false;
            if (StartWithDebug(command))
            {
                string t = command.GetLine().Trim();
                if (t.Length > m_debugStartId.Length)
                {
                    message = t.Substring(m_debugStartId.Length);
                    parsed = true;
                }
            }
        }

        public override I_InterpretorCompiledAction TryToGetCompiledAction(I_CommandLine command)
        {
            bool parsed;
            string message;
            GetMessageFrom(command, out parsed, out message);
            if (parsed && !string.IsNullOrEmpty(message))
            {

                return new CompiledMessage(this, message);
            }
            return null;

        }
        public class CompiledMessage : I_InterpretorCompiledAction
        {
            public Interpreter_ConsoleDebugger m_source;
            public string m_message;

            public CompiledMessage(Interpreter_ConsoleDebugger source, string message)
            {
                m_source = source;
                m_message = message;
            }

            public void Execute()
            {
                I_ExecutionStatus s=null;
                Execute(ref s);
            }

            public void Execute(ref I_ExecutionStatus status)
            {
                try
                {
                    m_source.Push(m_message);
                }
                catch (System.Exception e) {
                    if (status != null)
                        status.SetAsFail(e.StackTrace);
                }
            }
        }

        private void Push(string message)
        {
            if (m_messageToDebug != null)
                m_messageToDebug(message);
        }

        public delegate void MessageToDebug(string message);
    }
}
