using System;
using System.Collections.Generic;
using System.Text;

namespace Exponea
{
    public class Customer : AttributedObject
    {
        private const string RegisteredKey = "registered";

        public Customer(string registered)
        {
            ExternalIds[RegisteredKey] = registered;
        }

        public IDictionary<string, string> ExternalIds { get; set; } = new Dictionary<string, string>();

        public string RegisteredId
        {
            get => ExternalIds.TryGetValue(RegisteredKey, out var id) ? id : null;
            set => ExternalIds[RegisteredKey] = value;
        }
    }
}
