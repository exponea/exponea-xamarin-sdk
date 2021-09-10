using System;
using System.Collections.Generic;
using System.Text;

namespace Exponea
{
    public class Event : AttributedObject
    {
        public Event()
        {
        }

        public Event(string eventName)
        {
            Name = eventName;
        }

        public string Name { get; set; }
    }
}
