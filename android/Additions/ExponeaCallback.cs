using System;
using Kotlin.Jvm.Functions;

namespace ExponeaSdk
   
{
    public class ExponeaCallback<T> : Java.Lang.Object, IFunction1 where T : Java.Lang.Object
    {
        private readonly Action<T> OnInvoked;

        public ExponeaCallback(Action<T> onInvoked)
        {
            this.OnInvoked = onInvoked;
        }

        public Java.Lang.Object Invoke(Java.Lang.Object objParameter)
        {
            try
            {
                T parameter = (T)objParameter;
                OnInvoked?.Invoke(parameter);
                return null;
            }
            catch (Exception ex)
            {
                if (ex.Message != null)
                {
                    Console.WriteLine(ex.Message);
                }
                if (ex.StackTrace != null)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                return null;
            }
        }
    }
}
