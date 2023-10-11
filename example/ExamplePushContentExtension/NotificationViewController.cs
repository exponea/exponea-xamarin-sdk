using System;
using Foundation;
using UIKit;
using UserNotifications;
using UserNotificationsUI;
using ExponeaSdkNotifications;


namespace ExamplePushContentExtension
{

    public partial class NotificationViewController : UIViewController, IUNNotificationContentExtension
    {

        ExponeaNotificationHandler notificationHandler = new ExponeaNotificationHandler("group.com.exponea.ExponeaSDK-Example2");

        #region Constructors
        protected NotificationViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
        #endregion

        #region Override Methods
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Do any required interface initialization here.
        }
        #endregion

        #region Public Methods
        [Export("didReceiveNotification:")]
        public void DidReceiveNotification(UNNotification notification)
        {
            notificationHandler.HandleNotificationReceived(notification, ExtensionContext, this);
        }
        #endregion
    }
}
