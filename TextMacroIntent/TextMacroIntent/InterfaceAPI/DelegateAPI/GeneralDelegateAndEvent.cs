using System;
using System.Collections.Generic;
using System.Text;

namespace TextMacroIntent
{
    public  delegate void CommandLineCall(I_CommandLine commandLine);
    public  delegate void CommandLineInterpreterCall(I_Interpreter interpreter, I_CommandLine commandLine);
}
