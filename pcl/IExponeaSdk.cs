using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exponea
{
    public interface IExponeaSdk
    {
        void Configure(Configuration config);
        bool IsConfigured { get; }
        void SwitchProject(Project project, IDictionary<EventType, IList<Project>> projectMapping = null);

        void Track(Event evt, double? timestamp = null);
        void Track(Delivery delivery);
        void Track(Click click);
        void Track(Payment payment, double? timestamp = null);
        void TrackPushToken(string token);
        void TrackSessionStart();
        void TrackSessionEnd();
        void Anonymize();
        void IdentifyCustomer(Customer customer);

        string CustomerCookie { get; }

        Task FlushAsync();
        void Flush();
        Task<string> FetchConsentsAsync();
        Task<string> FetchRecommendationsAsync(RecommendationsRequest request);

        bool AutomaticSessionTracking { get; set; }
        FlushMode FlushMode { get; set; }
        TimeSpan FlushPeriod { get; set; }
        LogLevel LogLevel { get; set; }
        IDictionary<string, object> GetDefaultProperties();
        void SetDefaultProperties(IDictionary<string, object> properties);
        void CheckPushSetup();
        void SetInAppMessageDelegate(bool overrideDefaultBehavior, bool trackActions, Action<InAppMessage, string, string, bool> action);
        void TrackInAppMessageClick(InAppMessage message, string buttonText, string buttonLink);
        void TrackInAppMessageClose(InAppMessage message);
    }
}
