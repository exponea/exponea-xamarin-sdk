using System;
using ExponeaSdk.Models;
using System.Collections.Generic;

namespace ExponeaSdk
{
    public partial class Exponea
    {

        public void FlushData() {
            this.FlushData(null);
        
        }

        public void Anonymize()
        {
            this.Anonymize(null, null);

        }

        public void Anonymize(ExponeaProject project)
        {
            this.Anonymize(project, null);

        }

        public void Anonymize(IDictionary<EventType, IList<ExponeaProject>> projectRouteMap)
        {
            this.Anonymize(null, projectRouteMap);

        }

    }
}
