using System;
using Android.Content;
using ExponeaSdkAndroid = ExponeaSdk.Models;

namespace Exponea.Android
{
    internal class InAppMessageCallback : Java.Lang.Object, ExponeaSdkAndroid.IInAppMessageCallback
    {
        private bool Override;
        private bool Track;
        private readonly Action<InAppMessage, string, string, bool> Action;

        internal InAppMessageCallback(
            bool overrideDefaultBehavior,
            bool trackActions,
            Action<InAppMessage, string, string, bool> action)
        {
            Override = overrideDefaultBehavior;
            Track = trackActions;
            Action = action;
        }

        public bool OverrideDefaultBehavior { get => Override; set => Override = value; }

        public bool TrackActions { get => Track; set => Track = value; }

        public void InAppMessageAction(ExponeaSdkAndroid.InAppMessage message, ExponeaSdkAndroid.InAppMessageButton button, bool interaction, Context context)
        {
            Action?.Invoke(message?.ToNetInAppMessage(), button?.Text, button?.Url, interaction);
        }
    }
}
