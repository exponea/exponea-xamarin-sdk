using System;
using System.Collections.Generic;
using System.Text;

namespace Exponea
{
    public enum FlushMode
    {
        Unknown,
        AppClose,
        Immediate,
        Manual,
        Period,
    }

    internal enum FlushModeInternal
    {
        UNKNOWN,
        APP_CLOSE,
        IMMEDIATE,
        MANUAL,
        PERIOD,
    }
}
