using Android.Runtime;
using ExponeaSdk.Services;

namespace XamarinExample.Droid
{
    [Register("XamarinExample.ExponeaAuthProvider")]
    public class ExampleAuthProvider : Java.Lang.Object, IAuthorizationProvider
    {
        public ExampleAuthProvider()
        {
        }

        public string AuthorizationToken => CustomerTokenStorage.INSTANCE.RetrieveJwtToken();

    }

}
