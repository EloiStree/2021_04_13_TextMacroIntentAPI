using System;
using System.Collections.Generic;
using System.Text;

namespace TextMacroIntent
{

   
    /// <summary>
    /// If you know the command line you will send can change a bit but won't change of interpretor suddently.
    /// Then better just ask the access to the interpretor instead of auction the command all time.
    /// </summary>
    public class InterpretorDirectAccess : I_InterpretorDirectAccess
    {
        private I_Interpreter interpreter;

        public InterpretorDirectAccess(I_Interpreter interpreter)
        {
            this.interpreter = interpreter;
        }

        public I_Interpreter GetInterpreter()
        {
            return interpreter;
        }

        public void TryToTranslateAndExecute(I_CommandLine command)
        {
            I_ExecutionStatus status=null;
            TryToTranslateAndExecute(command, ref status);
        }

        public void TryToTranslateAndExecute(I_CommandLine command, ref I_ExecutionStatus result)
        {
            interpreter.TranslateToActionsWithStatus(ref command, ref result);
        }
    }
}
