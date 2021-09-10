using System;
using System.Collections.Generic;
using System.Text;

namespace Exponea
{
    public class FetchException : Exception
    {
        public FetchException(string message, string jsonBody)
            : base(message)
        {
            Data[nameof(JsonBody)] = jsonBody;
        }

        public string JsonBody => Data[nameof(JsonBody)] as string;
    }
}
