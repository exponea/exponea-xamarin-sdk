using System;
using System.Threading.Tasks;

namespace XamarinExample
{
    public class Util
    {
        public Util()
        {
        }

        // This is demo method that simulates some API call
        public Task<bool> VerifyUrl(string url, bool expectedResult)
        {
            var tcs = new TaskCompletionSource<bool>();
            Task.Delay(2000).Wait();
            tcs.SetResult(expectedResult);
            return tcs.Task;
        }
    }
}
