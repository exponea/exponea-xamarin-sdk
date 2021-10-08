using System;
using ExponeaSdkIos = ExponeaSdk;
using Foundation;

namespace Exponea.iOS
{
    public class ExponeaLinkHandler
    {
        private readonly ExponeaSdkIos.Exponea _exponea = ExponeaSdkIos.Exponea.Instance;

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

        public void HandleCampaignClick(NSUrl url, double? timestamp = null)
        {
            _exponea.HandleCampaignClick(url, timestamp ?? Utils.GetTimestamp());
        }
    }
}
