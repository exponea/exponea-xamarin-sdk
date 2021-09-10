using System;
using System.Collections.Generic;
using System.Text;

namespace Exponea
{
    public enum LogLevel
    {
        Unknown,
        Off,
        Error,
        Warning,
        Information,
        Debug,
        Verbose,
    }

    internal enum LogLevelInternal
    {
        UNKNOWN,
        OFF,
        ERROR,
        WARN,
        INFO,
        DEBUG,
        VERBOSE,
    }
}
