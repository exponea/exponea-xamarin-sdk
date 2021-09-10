using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation;

namespace Exponea
{
    internal static class ConversionExtensions
    {
        public static NSDictionary ToNsDictionary<TValue>(this IDictionary<string, TValue> dic)
        {
            if (dic == null)
            {
                return null;
            }

            var res = new NSDictionary();
            foreach (var kvp in dic)
            {
                res[kvp.Key] = NSObject.FromObject(kvp.Value);
            }

            return res;
        }

        public static NSString ToNsString(this string s)
            => s == null ? null : new NSString(s);
    }
}
