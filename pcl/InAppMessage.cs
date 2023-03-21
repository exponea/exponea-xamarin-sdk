using System;
namespace Exponea
{
    public class InAppMessage
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string RawMessageType { get; set; }
        public string RawFrequency { get; set; }
        public int VariantId { get; set; }
        public string VariantName { get; set; }
        public string EventType { get; set; }
        public int Priority { get; set; }
        public int DelayMS { get; set; }
        public int TimeoutMS { get; set; }
        public string PayloadHtml { get; set; }
        public bool IsHtml { get; set; }
        public bool RawHasTrackingConsent { get; set; }
        public string ConsentCategoryTracking { get; set; }

        //payload: InAppMessagePayload?,
        //trigger: EventFilter?,
        //dateFilter: DateFilter,

        public InAppMessage(
            string id,
            string name,
            string rawMessageType,
            string rawFrequency,
            int variantId,
            string variantName,
            string eventType,
            int priority,
            int delayMs,
            int timeoutMs,
            string payloadHtml,
            bool isHtml,
            bool rawHasTrackingConsent,
            string consentCategoryTracking
            )
        {
            Id = id;
            Name = name;
            RawMessageType = rawMessageType;
            RawFrequency = rawFrequency;
            VariantId = variantId;
            VariantName = variantName;
            EventType = eventType;
            Priority = priority;
            DelayMS = delayMs;
            TimeoutMS = timeoutMs;
            PayloadHtml = payloadHtml;
            IsHtml = isHtml;
            RawHasTrackingConsent = rawHasTrackingConsent;
            ConsentCategoryTracking = consentCategoryTracking;
        }
    }
}
