using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TextMacroIntent
{
    public abstract class AbstractInterpreter : I_Interpreter
    {
        public bool StartWith(ref I_CommandLine command, string startWith, bool trim = true, bool lower = true)
        {
            return StartWith(command.GetLine(), startWith, trim, lower);
        }
        public bool StartWith(string text, string startWith, bool trim = true, bool lower = true)
        {
            if (trim)
                text = text.Trim();
            if (lower)
                text = text.ToLower();
            return text.Trim().IndexOf(startWith) == 0;
        }
        public bool RespectRegex(string text, string regex, bool trim = true, bool lower=false)
        {
            text = TrimAndLow(ref text, trim, lower);
            return Regex.IsMatch(text, regex);
        }

        private static string TrimAndLow(ref string text, bool trim, bool lower)
        {
            if (trim)
                text = text.Trim();
            if (lower)
                text = text.ToLower();
            return text;
        }

        public abstract bool CanInterpreterUnderstand(ref I_CommandLine command);
        public abstract void TranslateToActionsWithStatus(ref I_CommandLine command, ref I_ExecutionStatus succedToExecute);

        public abstract I_InterpretorCompiledAction TryToGetCompiledAction(I_CommandLine command)  ;
    }
}
