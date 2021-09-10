using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Com.Exponea.Sdk.Util;
using ExponeaSdk.Models;

namespace Exponea
{
    public class ExponeaSdk : IExponeaSdk
    {
        private static readonly global::ExponeaSdk.Exponea _exponea = global::ExponeaSdk.Exponea.Instance;

        private readonly Android.Content.Context _context;

        public ExponeaSdk()
        {
            _context = Android.App.Application.Context;
        }

        public void Configure(Project project)
        {
            _exponea.Init(_context, new ExponeaConfiguration
            {
                ProjectToken = project.ProjectToken,
                Authorization = project.Authorization,
                BaseURL = project.BaseUrl,
            });
        }

        public void SwitchProject(Project project)
        {
            _exponea.Anonymize(new ExponeaProject(project.BaseUrl, project.ProjectToken, project.Authorization));
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

        public void Track(Event evt)
            => _exponea.TrackEvent(
                new PropertiesList(evt.Attributes.ToJavaDictionary()),
                GetTimestamp(),
                evt.Name);

        public void Track(Payment payment)
            => _exponea.TrackPaymentEvent(
                GetTimestamp(),
                new PurchasedItem((double)payment.Value, payment.Currency, payment.System, payment.ProductId, payment.ProductTitle, payment.Receipt));

        public Task FlushAsync()
        {
            var tcs = new TaskCompletionSource<Kotlin.Result>();
            _exponea.FlushData(new KotlinCallback<Kotlin.Result>(tcs.SetResult));
            return tcs.Task;
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
    }
}
