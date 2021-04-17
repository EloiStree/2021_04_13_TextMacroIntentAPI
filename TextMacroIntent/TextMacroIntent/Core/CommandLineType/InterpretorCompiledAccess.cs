using System;
using System.Collections.Generic;
using System.Text;

namespace TextMacroIntent
{

    

    /// <summary>
    /// Compiled access is used when you have a command line that you know won't change and will be often called. So better just keep knowing who to call and store the code associated to the command line.
    /// If the command line can change, then better call @interpretordirectaccess
    /// </summary>
    public class InterpretorCompiledAccess :InterpretorDirectAccess,  I_InterpretorCompiledAccess
    {
        I_CommandLine m_commandToExecute;
        I_InterpretorCompiledAction m_compiledAction;
        
        public InterpretorCompiledAccess(I_Interpreter interpreter, I_CommandLine commandToExecute): base (interpreter)
        {
            m_commandToExecute = commandToExecute;
            m_compiledAction = interpreter.TryToGetCompiledAction(m_commandToExecute);
        }

        public void Execute()
        {
            I_ParsingStatus s = null;
            Execute(ref s);
        }

        public void Execute(ref I_ParsingStatus status)
        {
            if (m_compiledAction != null)
                m_compiledAction.Execute(ref status);
            else GetInterpreter().TranslateToActionsWithStatus(ref m_commandToExecute, ref status);
        }


        public I_Interpreter GetInterpretor()
        {
            return base.GetInterpreter();
        }

        public I_CommandLine GetLinkedCommandLine()
        {
            return m_commandToExecute;
        }
    }

    public class InterpretorEnumGroupCompiledAccess : I_InterpretorEnumCompiledAction
    {
        I_CommandLine[] m_commandsToExecute;
        I_InterpretorCompiledAction[] m_commandsCompiledToExecute;
        I_InterpretorCompiledAction m_compiledAction;

       
        public InterpretorEnumGroupCompiledAccess(I_Interpreter interpreter, params I_CommandLine[] commandsToExecute)
        {
            SetWith(interpreter, commandsToExecute);
        }

        private void SetWith(I_Interpreter interpreter, I_CommandLine[] commandsToExecute)
        {
            m_commandsToExecute = commandsToExecute;
            m_commandsCompiledToExecute = new I_InterpretorCompiledAction[commandsToExecute.Length];
            for (int i = 0; i < commandsToExecute.Length; i++)
            {
                m_compiledAction = interpreter.TryToGetCompiledAction(commandsToExecute[i]);
            }
        }

        public void Execute()
        {
            I_ParsingStatus s = null;
            for (int i = 0; i < m_commandsCompiledToExecute.Length; i++)
            {
                    m_commandsCompiledToExecute[i]. Execute(ref s);
            }
        }

        public void Execute(ref I_ParsingStatus status)
        {
            if (status == null)
                Execute();
            I_ParsingStatus statusItem = new ParsingExecutionStatus();
            for (int i = 0; i < m_commandsCompiledToExecute.Length; i++)
            {
                m_commandsCompiledToExecute[i].Execute(ref statusItem );
                if (statusItem.HadError()) {
                    status.SetAsFail("Execution of compiled command failed.\n" + statusItem.GetErrorInformation());
                    return;
                }
            }

            status.SetAsFinish(true);

        }



        public IEnumerable<I_CommandLine> GetLinkedCommandLines()
        {
            return m_commandsToExecute;
        }

        public IEnumerable<I_InterpretorCompiledAction> GetLinkedCompiledCommandLines()
        {
            return m_commandsCompiledToExecute;
        }
    }
}
