
using System;

namespace Exponea
{
    internal class KotlinCallback<TArg> : Java.Lang.Object, Kotlin.Jvm.Functions.IFunction1
        where TArg: Java.Lang.Object
    {
        private readonly Action<TArg> _callback;

        public KotlinCallback(Action<TArg> callback)
        {
            _callback = callback;
        }

        public Java.Lang.Object Invoke(Java.Lang.Object p0)
        {
            _callback.Invoke(p0 as TArg);
            return null;
        }
    }
}
