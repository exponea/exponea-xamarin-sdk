﻿using Foundation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ExponeaSdkIos = ExponeaSdk;

namespace Exponea
{
    public class ExponeaSdk : IExponeaSdk
    {
        private readonly ExponeaSdkIos.Exponea _exponea = ExponeaSdkIos.Exponea.Instance;

        public string CustomerCookie => _exponea.CustomerCookie;

        public bool IsConfigured
        {
            get => _exponea.IsConfigured;
        }

        public bool AutomaticSessionTracking
        {
            get => _exponea.IsAutoSessionTrackingEnabled;
            set => throw new NotSupportedException();
        }

        public FlushMode FlushMode
        {
            get => (FlushMode)(Enum.TryParse<FlushModeInternal>(_exponea.FlushMode, true, out var value) ? value : default);
            set => _exponea.SetFlushMode(((FlushModeInternal)value).ToString());
        }

        public TimeSpan FlushPeriod
        {
            get => TimeSpan.FromMilliseconds(_exponea.FlushPeriod);
            set => _exponea.SetFlushPeriod(value.TotalMilliseconds);
        }

        public LogLevel LogLevel
        {
            get => (LogLevel)(Enum.TryParse<LogLevelInternal>(_exponea.LogLevel, true, out var value) ? value : default);
            set => _exponea.SetLogLevel(((LogLevelInternal)value).ToString());
        }

        public void Anonymize()
        {
            _exponea.Anonymize();
        }

        public void Configure(Configuration config)
        {
            _exponea.Configure(config.ToNSDictionary());
        }

        public void SwitchProject(Project project, IDictionary<EventType, IList<Project>> projectMapping = null)
        {
            _exponea.Anonymize(project.ToNsDictionary(), projectMapping.ToNsDictionary());
        }

        public Task<string> FetchConsentsAsync()
        {
            var tcs = new TaskCompletionSource<string>();
            _exponea.FetchConsents(
                success => tcs.SetResult(success),
                failure => tcs.SetException(new FetchException(failure, failure)));
            return tcs.Task;
        }

        public Task<string> FetchRecommendationsAsync(RecommendationsRequest request)
        {
            var tcs = new TaskCompletionSource<string>();

            var options = new NSDictionary(
                "id", request.Id,
                "fillWithRandom", request.FillWithRandom,
                "size", request.Size,
                "items", request.Items.ToNsDictionary<string>(),
                "noTrack", request.NoTrack,
                "catalogAttributesWhitelist", request.CatalogAttributesWhitelist);

            _exponea.FetchRecommendations(
                options,
                success => tcs.SetResult(success), //TODO: Parse result as list of CustomerRecommendation
                failure => tcs.SetException(new FetchException(failure, failure)));
            return tcs.Task;
        }

        public Task FlushAsync()
        {
            var tcs = new TaskCompletionSource<bool>();
            _exponea.FlushData(done => tcs.SetResult(true));
            return tcs.Task;
        }

        public void Flush()
        { 
            _exponea.FlushData();
        }

        public IDictionary<string, object> GetDefaultProperties()
        {
            if (_exponea.DefaultProperties == null)
            {
                return new Dictionary<string, object>();
            }

            try
            {
                return JsonSerializer.Deserialize<IDictionary<string, object>>(_exponea.DefaultProperties);
            }
            catch
            {
                // TODO: log? should we even catch?
                return new Dictionary<string, object>();
            }
        }

        public void SetDefaultProperties(IDictionary<string, object> properties)
        {
            _exponea.SetDefaultProperties(properties.ToNsDictionary<Object>());
        }

        public void IdentifyCustomer(Customer customer)
        {
            _exponea.IdentifyCustomer(customer.ExternalIds.ToNsDictionary(), customer.Attributes.ToNsDictionary());
        }

        private static double GetTimestamp()
            => (DateTime.UtcNow - DateTime.UnixEpoch).TotalSeconds;

        public void Track(Event evt, double? timestamp = null)
            => _exponea.TrackEvent(
                evt.Name,
                evt.Attributes.ToNsDictionary(),
                timestamp != null ? (double)timestamp : GetTimestamp());

        public void Track(Delivery delivery)
            => throw new NotSupportedException();

        public void Track(Click click)
        {
            var info = click.Attributes.ToNsDictionary();
            if (click.ActionType != null) { info["action_type"] = click.ActionType.ToNsString(); }
            if (click.ActionName != null) { info["action_name"] = click.ActionName.ToNsString(); }
            if (click.Url != null) { info["url"] = click.Url.ToNsString(); }
            _exponea.TrackPushOpened(info);
        }

        public void Track(Payment payment, double? timestamp = null)
        {
           

            _exponea.TrackPayment(payment.ToNsDictionary(), timestamp != null ? (double)timestamp : GetTimestamp());
        }

        public void TrackSessionStart()
        {
            _exponea.TrackSessionStart();
        }

        public void TrackSessionEnd()
        {
            _exponea.TrackSessionEnd();
        }

        public void TrackPushToken(string token)
        //TODO: Add trackPushToken method to iOS native SDK wrapper
            => throw new NotSupportedException();
    }
}
