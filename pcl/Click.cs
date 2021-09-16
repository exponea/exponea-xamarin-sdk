using System;
using System.Collections.Generic;
using System.Text;

namespace Exponea
{
    public class Click : AttributedObject
    {
        public Click(string actionType, string actionName, string url = null)
        {
            ActionType = actionType;
            ActionName = actionName;
            Url = url;
        }

        public string ActionType { get; set; }
        public string ActionName { get; set; }
        public string Url { get; set; }
    }
}
