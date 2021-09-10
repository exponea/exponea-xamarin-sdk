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

        public void Anonymize()
        {
            throw new NotImplementedException();
        }

        public void Configure(Project project)
        {
            throw new NotImplementedException();
        }

        public void SwitchProject(Project project)
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

        public void Track(Event evt)
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

        public void Track(Payment payment)
        {
            throw new NotImplementedException();
        }
    }
}
