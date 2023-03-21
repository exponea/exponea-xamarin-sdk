using System;
using System.Collections.Generic;
using System.Text;

namespace Exponea
{
    public class AppInboxMessage
    {
        public AppInboxMessage()
        { 
        }
        public string Id { get; set; }
        public AppInboxMessageType type { get; set; }
        public bool IsRead { get; set; } = false;
        public double ReceivedTime { get; set; }
        public IDictionary<string, object> content { get; set; }
    }
}
