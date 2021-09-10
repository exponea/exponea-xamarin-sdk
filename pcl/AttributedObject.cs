using System;
using System.Collections.Generic;
using System.Text;

namespace Exponea
{
    public class AttributedObject 
    {
        public Dictionary<string, object> Attributes { get; set; } = new Dictionary<string, object>();

        public object this[string key]
        {
            get => Attributes.TryGetValue(key, out var value) ? value : null;
            set => Attributes[key] = value;
        }
    }
}
