using System;
using Foundation;
using ObjCRuntime;
using UIKit;
using UserNotifications;

namespace XamarinExample.iOS
{
    [Register("ExponeaAuthProvider")]
    public class ExampleAuthProvider : ExponeaSdk.XamarinAuthorizationProvider
    {
        public ExampleAuthProvider()
        {
        }

        public override string AuthorizationToken => CustomerTokenStorage.INSTANCE.RetrieveJwtToken();
    }

}
