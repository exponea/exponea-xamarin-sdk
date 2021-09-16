using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Exponea
{
    public class ExponeaSdk : IExponeaSdk
    {
        public string CustomerCookie => throw new NotImplementedException();

        public bool AutomaticSessionTracking { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public FlushMode FlushMode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public TimeSpan FlushPeriod { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public LogLevel LogLevel { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool IsConfigured => throw new NotImplementedException();

        public void Anonymize()
        {
            throw new NotImplementedException();
        }

        public void Configure(Configuration config)
        {
            throw new NotImplementedException();
        }

        public void SwitchProject(Project project, IDictionary<EventType, IList<Project>> projectMapping)
        {
            throw new NotImplementedException();
        }

        public Task<string> FetchConsentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<string> FetchRecommendationsAsync(RecommendationsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task FlushAsync()
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, object> GetDefaultProperties()
        {
            throw new NotImplementedException();
        }

        public void IdentifyCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public void SetDefaultProperties(IDictionary<string, object> properties)
        {
            throw new NotImplementedException();
        }

        public void Track(Event evt, double? timestamp)
        {
            throw new NotImplementedException();
        }

        public void Track(Delivery delivery)
        {
            throw new NotImplementedException();
        }

        public void Track(Click click)
        {
            throw new NotImplementedException();
        }

        public void Track(Payment payment, double? timestamp)
        {
            throw new NotImplementedException();
        }

        public void TrackPushToken(string token)
        {
            throw new NotImplementedException();
        }

        public void TrackSessionStart()
        {
            throw new NotImplementedException();
        }

        public void TrackSessionEnd()
        {
            throw new NotImplementedException();
        }

        public void Flush()
        {
            throw new NotImplementedException();
        }
    }
}
