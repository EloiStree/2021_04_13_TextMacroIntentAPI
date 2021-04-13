using System;
using System.Collections.Generic;
using System.Text;

namespace TextMacroIntent.Core.CommandLineType
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
            I_ExecutionStatus s = null;
            Execute(ref s);
        }

        public void Execute(ref I_ExecutionStatus status)
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
}
