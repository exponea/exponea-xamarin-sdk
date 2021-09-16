using System;
using System.Collections.Generic;

namespace Exponea
{
    public class Configuration
    {

        public Configuration(string projectToken, string authorization, string baseUrl)
        {
            ProjectToken = projectToken;
            Authorization = authorization;
            BaseUrl = baseUrl;
        }

        public string ProjectToken { get; set; }
        public string Authorization { get; set; }
        public string BaseUrl { get; set; }
        public IDictionary<EventType, IList<Project>> ProjectRouteMap { get; set; }
        public int? MaxTries { get; set; }
        public double? SessionTimeout { get; set; }
        public bool? AutomaticSessionTracking { get; set; }
        public Dictionary<string, Object> DefaultProperties { get; set; }
    }
}
