using System;

namespace Exponea.iOS
{
    internal class Utils
    {
        public static double GetTimestamp()
           => (DateTime.UtcNow - DateTime.UnixEpoch).TotalSeconds;
    }
}
