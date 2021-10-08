using System;
using Android.Content;

namespace Exponea.Android
{
    public class ExponeaLinkHandler
    {
        private readonly global::ExponeaSdk.Exponea _exponea = global::ExponeaSdk.Exponea.Instance;

        private static readonly ExponeaLinkHandler instance = new ExponeaLinkHandler();

        static ExponeaLinkHandler()
        {
        }

        private ExponeaLinkHandler()
        {
        }

        public static ExponeaLinkHandler Instance
        {
            get
            {
                return instance;
            }
        }

        public void HandleCampaignClick(Intent intent, Context context)
        {
            _exponea.HandleCampaignIntent(intent, context);
        }
    }
}
