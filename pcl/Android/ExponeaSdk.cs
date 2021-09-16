using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Com.Exponea.Sdk.Util;
using ExponeaSdk.Models;
using Xamarin.Essentials;


namespace Exponea
{
    public class ExponeaSdk : IExponeaSdk
    {
        private static readonly global::ExponeaSdk.Exponea _exponea = global::ExponeaSdk.Exponea.Instance;

        public ExponeaSdk()
        {
          
        }

        public void Configure(Configuration config)
        {
            var configuration = new ExponeaConfiguration
            {
                ProjectToken = config.ProjectToken,
                Authorization = "Token " + config.Authorization,
                BaseURL = config.BaseUrl,

            };

            if (config.AutomaticSessionTracking != null) {
                configuration.AutomaticSessionTracking = (bool)config.AutomaticSessionTracking;
            }
            if (config.DefaultProperties != null)
            {
                configuration.DefaultProperties = config.DefaultProperties.ToJavaDictionary();
            }
            if (config.MaxTries != null)
            {
                configuration.MaxTries = (int)config.MaxTries;
            }
            if (config.SessionTimeout != null)
            {
                configuration.SessionTimeout = (double)config.SessionTimeout;
            }
            if (config.ProjectRouteMap != null)
            {
                configuration.ProjectRouteMap = config.ProjectRouteMap.ToJavaDictionary();
            }

            _exponea.Init(Platform.CurrentActivity, configuration);
        }

        public bool IsConfigured
        {
            get => _exponea.IsInitialized;
        }

        public void SwitchProject(Project project, IDictionary<EventType, IList<Project>> projectMapping = null)
        {
            _exponea.Anonymize(new ExponeaProject(project.BaseUrl, project.ProjectToken, "Token" + project.Authorization), projectMapping.ToJavaDictionary());
        }

        public string CustomerCookie
            => _exponea.CustomerCookie;

        public bool AutomaticSessionTracking
        {
            get => _exponea.AutomaticSessionTracking;
            set => _exponea.AutomaticSessionTracking = value;
        }

        public FlushMode FlushMode
        {
            get => _exponea.FlushMode.ToNetEnum<FlushMode, FlushModeInternal>();
            set => _exponea.FlushMode = global::ExponeaSdk.Models.FlushMode.ValueOf(value.ToJavaEnumName<FlushModeInternal, FlushMode>());
        }

        public TimeSpan FlushPeriod
        {
            get => _exponea.FlushPeriod.ToTimeSpan();
            set => _exponea.FlushPeriod = value.ToFlushPeriod();
        }

        public LogLevel LogLevel
        {
            get => _exponea.LoggerLevel.ToNetEnum<LogLevel, LogLevelInternal>();
            set => _exponea.LoggerLevel = Logger.Level.ValueOf(value.ToJavaEnumName<LogLevelInternal, LogLevel>());
        }
        public IDictionary<string, object> GetDefaultProperties()
            => _exponea.DefaultProperties.ToNetDictionary();

        public void SetDefaultProperties(IDictionary<string, object> properties)
            => _exponea.DefaultProperties = properties.ToJavaDictionary();

        public void Anonymize() => _exponea.Anonymize();

        public void IdentifyCustomer(Customer customer)
            => _exponea.IdentifyCustomer(
                new CustomerIds(customer.ExternalIds),
                new PropertiesList(customer.Attributes.ToJavaDictionary()));

        public void Track(Click click)
            => _exponea.TrackClickedPush(
                new NotificationData(click.Attributes.ToJavaDictionary(), new CampaignData() /* ? */),
                new NotificationAction(click.ActionType, click.ActionName, click.Url),
                GetTimestamp());

        public void Track(Delivery delivery)
            => _exponea.TrackDeliveredPush(
                new NotificationData(delivery.Attributes.ToJavaDictionary(), new CampaignData() /* ? */),
                GetTimestamp());

        public void Track(Event evt, double? timestamp = null)
            => _exponea.TrackEvent(
                new PropertiesList(evt.Attributes.ToJavaDictionary()),
                timestamp != null ? (double)timestamp : GetTimestamp(),
                evt.Name);

        public void Track(Payment payment, double? timestamp = null)
            => _exponea.TrackPaymentEvent(
                timestamp != null ? (double)timestamp : GetTimestamp(),
                new PurchasedItem((double)payment.Value, payment.Currency, payment.System, payment.ProductId, payment.ProductTitle, payment.Receipt));

        public Task FlushAsync()
        {
            var tcs = new TaskCompletionSource<Kotlin.Result>();
            _exponea.FlushData(new KotlinCallback<Kotlin.Result>(tcs.SetResult));
            return tcs.Task;
        }

        public void Flush()
        {
            _exponea.FlushData();
        }

        public Task<string> FetchConsentsAsync()
        {
            var tcs = new TaskCompletionSource<string>();
            _exponea.GetConsents(
                new KotlinCallback<Result>(r =>
                {
                    tcs.SetResult(r.Results.ToString());
                }),
                new KotlinCallback<Result>(r =>
                {
                    var err = (FetchError)r.Results;
                    tcs.SetException(new FetchException(err.Message, err.JsonBody));
                }));
            return tcs.Task;
        }

        public Task<string> FetchRecommendationsAsync(RecommendationsRequest request)
        {
            var tcs = new TaskCompletionSource<string>();
            var recommendationOptions = new CustomerRecommendationOptions(
                request.Id,
                request.FillWithRandom,
                request.Size,
                request.Items,
                Java.Lang.Boolean.ValueOf(request.NoTrack),
                request.CatalogAttributesWhitelist);
            _exponea.FetchRecommendation(recommendationOptions,
                new KotlinCallback<Result>(r =>
                {
                    //TODO: Return list of CustomerRecommendation instead of string
                    tcs.SetResult(r.Results.ToString());
                }),
                new KotlinCallback<Result>(r =>
                {
                    var err = (FetchError)r.Results;
                    tcs.SetException(new FetchException(err.Message, err.JsonBody));
                }));
            return tcs.Task;
        }

        private static double GetTimestamp()
            => (DateTime.UtcNow - DateTime.UnixEpoch).TotalSeconds;

        public void TrackSessionStart()
        {
            _exponea.TrackSessionStart(GetTimestamp());
        }

        public void TrackSessionEnd()
        {
            _exponea.TrackSessionEnd(GetTimestamp());
        }

        public void TrackPushToken(string token)
        {
            _exponea.TrackPushToken(token);
        }
    }
}
