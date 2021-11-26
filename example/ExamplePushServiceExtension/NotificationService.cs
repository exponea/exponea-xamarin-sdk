using System;
using Foundation;
using UserNotifications;
using ExponeaSdkNotifications;

namespace ExamplePushServiceExtension
{
    [Register("NotificationService")]
    public class NotificationService : UNNotificationServiceExtension
    {
        #region Computed Properties
        public Action<UNNotificationContent> ContentHandler { get; set; }
        public UNMutableNotificationContent BestAttemptContent { get; set; }
        #endregion

        ExponeaNotificationHandler notificationHandler = new ExponeaNotificationHandler("group.com.exponea.xamarin");

        #region Constructors
        protected NotificationService(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
        #endregion

        #region Override Methods
        public override void DidReceiveNotificationRequest(UNNotificationRequest request, Action<UNNotificationContent> contentHandler)
        {
            ContentHandler = contentHandler;
            BestAttemptContent = (UNMutableNotificationContent)request.Content.MutableCopy();

            Console.WriteLine("Notification received! " + BestAttemptContent.UserInfo);
           
            notificationHandler.HandleNotificationRequest(request, contentHandler);

        }

        public override void TimeWillExpire()
        {
            // Called just before the extension will be terminated by the system.
            // Use this as an opportunity to deliver your "best attempt" at modified content, otherwise the original push payload will be used.

           notificationHandler.TimeWillExpire();
        }
        #endregion
    }
}
