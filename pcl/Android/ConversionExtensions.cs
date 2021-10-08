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
                Java.Lang.Object o => o,
                _ => throw new NotSupportedException()
            };
    }
}
