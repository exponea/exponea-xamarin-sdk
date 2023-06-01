using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using ExponeaSdkIos = ExponeaSdk;

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

        public static Dictionary<string, object> ToNetDictionary(this NSDictionary nsDictionary) =>
            nsDictionary.ToDictionary<KeyValuePair<NSObject, NSObject>, string, object>
                (item => item.Key as NSString, item => item.Value);

        public static NSArray ToNSArray<TValue>(this List<TValue> list)
        {

            if (list == null)
            {
                return null;
            }

            var array = new NSObject[list.Count];

            for (int i = 0; i < list.Count; i++)
            {
                array[i] = list[i].ToNSObject();
            }

            return NSArray.FromNSObjects<NSObject>(array);
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

        public static NSMutableDictionary ToNSDictionary(this Configuration source)
        {
            var target = new NSMutableDictionary
            {
                { new NSString("projectToken"), new NSString(source.ProjectToken) },
                { new NSString("authorizationToken"), new NSString(source.Authorization) },
                { new NSString("baseUrl"), new NSString(source.BaseUrl) }
            };

            if (source.AutomaticSessionTracking != null)
            {
                target.Add(new NSString("automaticSessionTracking"), new NSNumber((bool)source.AutomaticSessionTracking));
            }

            if (source.DefaultProperties != null)
            {
                target.Add(new NSString("defaultProperties"), source.DefaultProperties.ToNsDictionary<object>());
            }

            if (source.MaxTries != null)
            {
                target.Add(new NSString("maxTries"), new NSNumber((int)source.MaxTries));
            }

            if (source.SessionTimeout != null)
            {
                target.Add(new NSString("sessionTimeout"), new NSNumber((double)source.SessionTimeout));
            }

            if (source.ProjectRouteMap != null)
            {
                target.Add(new NSString("projectMapping"), source.ProjectRouteMap.ToNsDictionary());
            }

            target.Add(new NSString("pushTokenTrackingFrequency"), new NSString(((TokenTrackFrequencyInternal)source.TokenTrackFrequency).ToString()));

            if (source.iOSConfiguration != null)
            {
                var iosConfig = source.iOSConfiguration;

                if (iosConfig.RequirePushAuthorization != null)
                {
                    target.Add(new NSString("requirePushAuthorization"), new NSNumber((bool)iosConfig.RequirePushAuthorization));
                }
                if (iosConfig.AppGroup != null)
                {
                    target.Add(new NSString("appGroup"), new NSString(iosConfig.AppGroup));
                }
            }

            target.Add(new NSString("allowDefaultCustomerProperties"), new NSNumber((bool)source.AllowDefaultCustomerProperties));
            target.Add(new NSString("advancedAuthEnabled"), new NSNumber((bool)source.AdvancedAuthEnabled));

            return target;
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
                bool b => new NSNumber(b),
                IDictionary<string, object> dic => dic.ToNsDictionary(),
                NSObject o => o,
                _ => throw new ArgumentException()
            };
        }

        public static InAppMessage ToNetInAppMessage(this ExponeaSdkIos.SimpleInAppMessage message)
        {
            return new InAppMessage(
                    message.Id,
                    message.Name,
                    message.RawMessageType,
                    message.RawFrequency,
                    message.VariantId,
                    message.VariantName,
                    message.EventType,
                    message.Priority,
                    message.DelayMS,
                    message.TimeoutMS,
                    message.PayloadHtml,
                    message.IsHtml,
                    message.RawHasTrackingConsent,
                    message.ConsentCategoryTracking
                    );
        }

        public static ExponeaSdkIos.SimpleInAppMessage ToNsSimleInAppMessage(this InAppMessage message)
        {
            return new ExponeaSdkIos.SimpleInAppMessage(
                    message.Id,
                    message.Name,
                    message.RawMessageType,
                    message.VariantId,
                    message.VariantName,
                    message.RawFrequency,
                    message.EventType,
                    message.Priority,
                    message.DelayMS,
                    message.TimeoutMS,
                    message.PayloadHtml,
                    message.IsHtml,
                    message.RawHasTrackingConsent,
                    message.ConsentCategoryTracking
                    );
        }

        public static ExponeaSdkIos.AppInboxMessage ToNative(this Exponea.AppInboxMessage source)
        {
            return new ExponeaSdkIos.AppInboxMessage(
                source.Id,
                source.type.ToString().ToLower(),
                source.IsRead,
                (int)source.ReceivedTime,
                source.content.ToNsDictionary()
            );
        }

        public static Exponea.AppInboxMessage ToNet(this ExponeaSdkIos.AppInboxMessage source)
        {
            var target = new Exponea.AppInboxMessage();
            target.Id = source.Id;
            AppInboxMessageType messageType;
            if (Enum.TryParse<AppInboxMessageType>(source.Type, true, out messageType) == false)
            {
                messageType = AppInboxMessageType.UNKNOWN;
            }
            target.type = messageType;
            target.IsRead = source.Read;
            target.ReceivedTime = source.ReceivedTime;
            target.content = source.Content.ToNetDictionary();
            return target;
        }

        public static ExponeaSdkIos.AppInboxAction ToNative(this Exponea.AppInboxAction source)
        {
            return new ExponeaSdkIos.AppInboxAction(
                source.Type.ToString().ToLower(),
                source.Title,
                source.Url
            );
        }
    }
}
