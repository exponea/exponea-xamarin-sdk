using System;
using System.IO;
using Android.Content;
using Android.Util;
using Huawei.Agconnect.Config;

namespace XamarinExample.Droid
{
    public class HmsLazyInputStream : LazyInputStream
    {
        public HmsLazyInputStream(Context context)
            : base(context)
        {
        }

        public override Stream Get(Context context)
        {
            try
            {
                return context.Assets.Open("agconnect-services.json");
            }
            catch (Exception e)
            {
                Log.Error(e.ToString(), $"Can't open agconnect file:" + e.Message);
                return null;
            }
        }
    }
}
