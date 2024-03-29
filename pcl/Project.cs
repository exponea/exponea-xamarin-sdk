﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Exponea
{
    public class Project
    {
        public Project(string projectToken, string authorization, string baseUrl)
        {
            ProjectToken = projectToken;
            Authorization = authorization;
            BaseUrl = baseUrl;
        }

        public string ProjectToken { get; set; }
        public string Authorization { get; set; }
        public string BaseUrl { get; set; }
    }
}
