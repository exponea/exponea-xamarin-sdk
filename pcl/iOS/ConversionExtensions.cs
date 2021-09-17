using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation;

namespace Exponea
{
    internal static class ConversionExtensions
    {
        public static NSMutableDictionary<NSString, NSObject> ToNsDictionary<TValue>(this IDictionary<string, TValue> dic)
        {
            if (dic == null)
            {
                return null;
            }

            var res = new NSMutableDictionary<NSString, NSObject>();
            foreach (var kvp in dic)
            {
                res[kvp.Key.ToNsString()] = kvp.Value.ToNSObject();
            }

            return res;
        }

        public static NSMutableDictionary<NSString, NSMutableArray> ToNsDictionary(this IDictionary<EventType, IList<Project>> dic)
        {
            if (dic == null)
            {
                return null;
            }

            var res = new NSMutableDictionary<NSString, NSMutableArray>();
            foreach (var kvp in dic)
            {
                var array = new NSMutableArray<NSDictionary>();
                foreach (var project in kvp.Value)
                {
                    array.Add(project.ToNsDictionary());
                }

                res[((EventTypeInternal)kvp.Key).ToString().ToNsString()] = array;
            }

            return res;
        }

        public static NSDictionary ToNsDictionary(this Project project)
        {
            return new NSDictionary(
                "projectToken", project.ProjectToken,
                "authorizationToken", project.Authorization,
                "baseUrl", project.BaseUrl);
        }

        public static NSMutableDictionary ToNsDictionary(this Payment payment)
        {
            var dict = new NSMutableDictionary
            {
                { new NSString("value"), new NSNumber(payment.Value) },
                { new NSString("currency"), new NSString(payment.Currency) },
                { new NSString("paymentSystem"), new NSString(payment.System) },
                 { new NSString("productId"), new NSString(payment.ProductId) },
                 { new NSString("productTitle"), new NSString(payment.ProductTitle) }
            };


            if (payment.Receipt != null)
            {
                dict.Add(new NSString("receipt"), new NSString(payment.Receipt));
            }

            return dict;
        }

        public static NSMutableDictionary ToNSDictionary(this Configuration config)
        {
            var configDictionary = new NSMutableDictionary
            {
                { new NSString("projectToken"), new NSString(config.ProjectToken) },
                { new NSString("authorizationToken"), new NSString(config.Authorization) },
                { new NSString("baseUrl"), new NSString(config.BaseUrl) }
            };

            if (config.AutomaticSessionTracking != null)
            {
                configDictionary.Add(new NSString("automaticSessionTracking"), new NSNumber((bool)config.AutomaticSessionTracking));
            }

            if (config.DefaultProperties != null)
            {
                configDictionary.Add(new NSString("defaultProperties"), config.DefaultProperties.ToNsDictionary<object>());
            }

            if (config.MaxTries != null)
            {
                configDictionary.Add(new NSString("maxTries"), new NSNumber((int)config.MaxTries));
            }

            if (config.SessionTimeout != null)
            {
                configDictionary.Add(new NSString("sessionTimeout"), new NSNumber((double)config.SessionTimeout));
            }

            if (config.ProjectRouteMap != null)
            {
                configDictionary.Add(new NSString("projectMapping"), config.ProjectRouteMap.ToNsDictionary());
            }

            configDictionary.Add(new NSString("pushTokenTrackingFrequency"), new NSString(((TokenTrackFrequencyInternal)config.TokenTrackFrequency).ToString()));

            if (config.iOSConfiguration != null)
            {
                var iosConfig = config.iOSConfiguration;

                if (iosConfig.RequirePushAuthorization != null)
                {
                    configDictionary.Add(new NSString("requirePushAuthorization"), new NSString(iosConfig.RequirePushAuthorization.ToString().ToLower()));
                }
                if (iosConfig.AppGroup != null)
                {
                    configDictionary.Add(new NSString("appGroup"), new NSString(iosConfig.AppGroup));
                }
            }

            
            return configDictionary;
        }

        public static NSString ToNsString(this string s)
            => s == null ? null : new NSString(s);

        public static NSObject ToNSObject(this object value)
        {
            return value switch
            {
                null => null,
                short s => new NSNumber(s),
                int i => new NSNumber(i),
                long l => new NSNumber(l),
                float f => new NSNumber(f),
                double d => new NSNumber(d),
                string s => new NSString(s),
                NSObject o => o,
                _ => throw new NotSupportedException()
            };
        }
    }
}
