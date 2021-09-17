using System;
using System.Collections.Generic;
using System.Text;

namespace Exponea
{
    public class RecommendationsRequest
    {
        public const int DefaultSize = 10;

        public RecommendationsRequest(string id, bool fillWithRandom = false, int size = DefaultSize)
        {
            Id = id;
            FillWithRandom = fillWithRandom;
            Size = size;
        }

        public string Id { get; set; }
        public bool FillWithRandom { get; set; }
        public int Size { get; set; } = DefaultSize;
        public IDictionary<string, string> Items { get; set; } = new Dictionary<string, string>();
        public bool NoTrack { get; set; }
        public IList<string> CatalogAttributesWhitelist { get; set; } = new List<string>();
    }
}
