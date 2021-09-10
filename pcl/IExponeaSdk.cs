using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exponea
{
    public interface IExponeaSdk
    {
        void Configure(Project project);
        void SwitchProject(Project project);

        void Track(Event evt);
        void Track(Delivery delivery);
        void Track(Click click);
        void Track(Payment payment);
        void Anonymize();
        void IdentifyCustomer(Customer customer);

        string CustomerCookie { get; }

        Task FlushAsync();
        Task<string> FetchConsentsAsync();
        Task<string> FetchRecommendationsAsync(RecommendationsRequest request);

        bool AutomaticSessionTracking { get; set; }
        FlushMode FlushMode { get; set; }
        TimeSpan FlushPeriod { get; set; }
        LogLevel LogLevel { get; set; }
        IDictionary<string, object> GetDefaultProperties();
        void SetDefaultProperties(IDictionary<string, object> properties);
    }
}
