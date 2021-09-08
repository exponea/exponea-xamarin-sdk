using System;

namespace ExponeaSdk.Models
{
    public partial class CustomerRecommendationOptions
    {
        public CustomerRecommendationOptions(string id, bool fillWithRandom, int size) : this(id, fillWithRandom, size, null, null, null) { }
    }
}
