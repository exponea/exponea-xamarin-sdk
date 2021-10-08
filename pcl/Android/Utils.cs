using System;
using Android.Content;

namespace Exponea.Android

{
    internal class Utils
    {
        public static double GetTimestamp()
           => (DateTime.UtcNow - DateTime.UnixEpoch).TotalSeconds;

        public static Java.Lang.Integer GetResourceId(Context context, string name) 
           => (Java.Lang.Integer)context.Resources.GetIdentifier(name, "drawable", context.PackageName);
        
    } 
}
