using System;
using System.Collections.Generic;
using System.Text;

namespace TextMacroIntent
{
    public interface I_TextMacroInputAll :
        I_CommandLineDirectExecutor,
        I_CommandLineDirectExecutorWithReturn,
        I_CommandLineDelayExecutor,
        I_CommandLineComplexDelayExecutor, 
        I_CommandAuctionDistributor
    {    }
}
