using System;
using System.Collections.Generic;
using System.Text;

namespace TextMacroIntent
{
    public interface ICommandLineAtTimeOfDay
    {
        ushort GetMillisecond();
        ushort GetSecond();
        ushort GetMinute();
        ushort GetHourOn24H();
    }
    public interface ICommandLineAtDate : ICommandLineAtTimeOfDay
    {
        ushort GetDay();
        ushort GetMonth();
        ushort GetYear();
    }
}
