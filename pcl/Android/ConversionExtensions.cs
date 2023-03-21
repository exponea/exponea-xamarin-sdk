using ExponeaSdkAndroid = ExponeaSdk.Models;
using Java.Util.Concurrent;
using System;
using System.Collections.Generic;
using Android.Runtime;

namespace Exponea
{
    internal static class ConversionExtensions
    {
        private const long NanosecondsPerTick = 100;    // .NET ticks are 100ns quantities

        public static TimeSpan ToTimeSpan(this ExponeaSdkAndroid.FlushPeriod flushPeriod)
            => TimeSpan.FromTicks(flushPeriod.TimeUnit.ToNanos(flushPeriod.Amount) / NanosecondsPerTick);
        public static ExponeaSdkAndroid.FlushPeriod ToFlushPeriod(this TimeSpan timeSpan)
            => new ExponeaSdkAndroid.FlushPeriod(timeSpan.Ticks * NanosecondsPerTick, TimeUnit.Nanoseconds);

        public static TPublicEnum ToNetEnum<TPublicEnum, TPrivateEnum>(this Java.Lang.Enum value)
            where TPublicEnum : struct
            where TPrivateEnum : struct
            => (TPublicEnum)(object)(Enum.TryParse<TPrivateEnum>(value.Name(), true, out var result) ? result : default);
        public static string ToJavaEnumName<TPrivateEnum, TPublicEnum>(this TPublicEnum value)
            where TPrivateEnum : struct
            where TPublicEnum : struct
            => ((TPrivateEnum)(object)value).ToString();

        public static Dictionary<string, object> ToNetDictionary(this IDictionary<string, Java.Lang.Object> dic)
        {
            if (dic == null)
            {
                return null;
            }

            var res = new Dictionary<string, object>();
            foreach (var kvp in dic)
            {
                res[kvp.Key] = kvp.Value.ToNet();
            }

            return res;
        }

        public static Dictionary<string, Java.Lang.Object> ToJavaDictionary<TValue>(this IDictionary<string, TValue> dic)
        {
            if (dic == null)
            {
                return null;
            }

            var res = new Dictionary<string, Java.Lang.Object>();
            foreach (var kvp in dic)
            {
                res[kvp.Key] = kvp.Value.ToJava();
            }

            return res;
        }

        public static IDictionary<ExponeaSdkAndroid.EventType, IList<ExponeaSdkAndroid.ExponeaProject>> ToJavaDictionary(this IDictionary<EventType, IList<Project>> dic)
        {
            if (dic == null)
            {
                return null;
            }

            var res = new Dictionary<ExponeaSdkAndroid.EventType, IList<ExponeaSdkAndroid.ExponeaProject>>();
            foreach (var kvp in dic)
            {
                var projectList = new JavaList<ExponeaSdkAndroid.ExponeaProject>();
                foreach (var project in kvp.Value)
                {
                    projectList.Add(new ExponeaSdkAndroid.ExponeaProject(project.BaseUrl, project.ProjectToken, "Token " + project.Authorization));
                }
                res[ExponeaSdkAndroid.EventType.ValueOf(kvp.Key.ToJavaEnumName<EventTypeInternal, EventType>())] = projectList;
            }

            return res;
        }

        public static object ToNet(this Java.Lang.Object java)
            => java switch
            {
                Java.Lang.Short s => (short)s,
                Java.Lang.Integer i => (int)i,
                Java.Lang.Long l => (long)l,
                Java.Lang.Float f => (float)f,
                Java.Lang.Double d => (double)d,
                Java.Lang.String s => (string)s,
                Java.Lang.Boolean b => (bool)b,
                IDictionary<string, Java.Lang.Object> dic => dic.ToNetDictionary(),
                _ => java
            };

        public static Java.Lang.Object ToJava(this object value)
            => value switch
            {
                null => null,
                short s => new Java.Lang.Short(s),
                int i => new Java.Lang.Integer(i),
                long l => new Java.Lang.Long(l),
                float f => new Java.Lang.Float(f),
                double d => new Java.Lang.Double(d),
                string s => new Java.Lang.String(s),
                bool b => new Java.Lang.Boolean(b),
                Java.Lang.Object o => o,
                _ => throw new NotSupportedException()
            };

        public static Java.Lang.Boolean ToJava(this bool? value)
            => value switch
            {
                null => null,
                bool b => new Java.Lang.Boolean(b)
            };

        public static Java.Lang.Boolean ToJava(this bool value)
            => new Java.Lang.Boolean(value);

        public static Java.Lang.Double ToJava(this double? value)
            => value switch
            {
                null => null,
                double d => new Java.Lang.Double(d)
            };

        public static Java.Lang.Double ToJava(this double value)
            => new Java.Lang.Double(value);

        public static ExponeaSdkAndroid.InAppMessage ToAndroidInAppMessage(this InAppMessage message)
        {
            return new ExponeaSdkAndroid.InAppMessage(
                      message.Id,
                      message.Name,
                      message.RawMessageType,
                      message.RawFrequency,
                      null,
                      message.VariantId,
                      message.VariantName,
                      new ExponeaSdkAndroid.EventFilter.EventFilter(
                          message.EventType,
                          new List<ExponeaSdkAndroid.EventFilter.EventPropertyFilter>()),
                      new ExponeaSdkAndroid.DateFilter(),
                      new Java.Lang.Integer(message.Priority),
                      new Java.Lang.Long(message.DelayMS),
                      new Java.Lang.Long(message.TimeoutMS),
                      message.PayloadHtml,
                      new Java.Lang.Boolean(message.IsHtml),
                      new Java.Lang.Boolean(message.RawHasTrackingConsent),
                      message.ConsentCategoryTracking
            );
        }

        public static string ToStringCode(this AppInboxMessageType value)
            => value switch
            {
                AppInboxMessageType.HTML => "html",
                AppInboxMessageType.PUSH => "push",
                AppInboxMessageType.UNKNOWN => "unknown",
                _ => throw new NotImplementedException(),
            };

        public static ExponeaSdkAndroid.MessageItemAction.Type ToAndroidAppInboxMessageType(this AppInboxActionType source)
        => source switch
        {
            AppInboxActionType.APP => ExponeaSdkAndroid.MessageItemAction.Type.App,
            AppInboxActionType.BROWSER => ExponeaSdkAndroid.MessageItemAction.Type.Browser,
            AppInboxActionType.DEEPLINK => ExponeaSdkAndroid.MessageItemAction.Type.Deeplink,
            AppInboxActionType.NO_ACTION => ExponeaSdkAndroid.MessageItemAction.Type.NoAction,
            _ => throw new NotImplementedException(),
        };

        public static ExponeaSdkAndroid.MessageItem ToAndroidAppInboxMessage(this AppInboxMessage message)
        {
            return new ExponeaSdkAndroid.MessageItem(
                message.Id,
                message.type.ToStringCode(),
                message.IsRead.ToJava(),
                message.ReceivedTime.ToJava(),
                message.content.ToJavaDictionary()
            );
        }
        public static ExponeaSdkAndroid.MessageItemAction ToJavaAppInboxAction(this AppInboxAction source)
        {
            ExponeaSdkAndroid.MessageItemAction target = new ExponeaSdkAndroid.MessageItemAction();
            target.SetType(source.Type.ToAndroidAppInboxMessageType());
            target.Title = source.Title;
            target.Url = source.Url;
            return target;
        }

        public static InAppMessage ToNetInAppMessage(this ExponeaSdkAndroid.InAppMessage message)
        {
            return new InAppMessage(
                      message.Id,
                      message.Name,
                      message.RawMessageType,
                      message.RawFrequency,
                      message.VariantId,
                      message.VariantName,
                      message.Trigger?.EventType ?? "session_start",
                      message.Priority?.IntValue() ?? 0,
                      message.Delay?.IntValue() ?? 0,
                      message.Timeout?.IntValue() ?? 0,
                      message.PayloadHtml,
                      message.IsHtml().BooleanValue(),
                      message.RawHasTrackingConsent.BooleanValue(),
                      message.ConsentCategoryTracking
                      );
        }

    }
}
