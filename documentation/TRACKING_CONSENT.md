## Tracking consent

Based on the recent judgment (May 2022) made by the Federal Court of Justice in Germany (Bundesgerichtshof – BGH)
regarding the EU Datenschutz Grundverordnung (EU-GDPR), all access to data on the affected person’s device would
require explicit consent. For more info see [Configuration of the tracking consent categories](https://documentation.bloomreach.com/engagement/docs/configuration-of-tracking-consent).

The SDK is adapted to the rules and is controlled according to the data received from the Push Notifications or InApp Messages.
If the tracking consent feature is disabled, the Push Notifications and InApp Messages data do not contain 'hasTrackingConsent' and their tracking behaviour has not been changed, so if the attribute 'hasTrackingConsent' is not present in data, SDK considers it as 'true'.
If the tracking consent feature is enabled, Push Notifications and InApp Messages data contain 'hasTrackingConsent' and the SDK tracks events according to the boolean value of this field.

Disallowed tracking consent ('hasTrackingConsent' provided with 'false' value) can be overridden with URL query param 'xnpe_force_track' with 'true' value.

### Event for push notification delivery

Event is normally tracked by calling `IExponeaSdk.TrackDeliveredPush` or `IExponeaSdk.HandleRemoteMessage`. This methods are tracking
a delivered event only if:

* Tracking consent feature is disabled
* Tracking consent feature is enabled and 'hasTrackingConsent' has 'true' value

If you are using `IExponeaSdk.TrackDeliveredPush` method manually and you want to avoid to consider tracking, you may use `IExponeaSdk.TrackDeliveredPushWithoutTrackingConsent` instead. This method will do track event ignoring tracking consent state.

### Event for clicked push notification

Event is normally tracked by calling `IExponeaSdk.TrackClickedPush`. This method is tracking
a clicked event only if:

* Tracking consent feature is disabled
* Tracking consent feature is enabled and 'hasTrackingConsent' has 'true' value
* Action URL contains 'xnpe_force_track' with 'true' value independently from 'hasTrackingConsent' value

> Event that is tracked because of `xnpe_force_track` (forced tracking) will contains an additional property `tracking_forced` with value `true`

If you are using `IExponeaSdk.TrackClickedPush` method manually and you want to avoid to consider tracking, you may use `IExponeaSdk.TrackClickedPushWithoutTrackingConsent` instead. This method will do track event ignoring tracking consent state.

### Event for clicked InApp Message

Event is normally tracked by calling `IExponeaSdk.TrackInAppMessageClick`. This method is tracking
a clicked event only if:

* Tracking consent feature is disabled
* Tracking consent feature is enabled and 'hasTrackingConsent' has 'true' value
* Action URL contains 'xnpe_force_track' with 'true' value independently from 'hasTrackingConsent' value

> Event that is tracked because of `xnpe_force_track` (forced tracking) will contains an additional property `tracking_forced` with value `true`

If you are using `IExponeaSdk.TrackInAppMessageClick` method manually and you want to avoid to consider tracking, you may use `IExponeaSdk.TrackInAppMessageClickWithoutTrackingConsent` instead. This method will do track event ignoring tracking consent state.

### Event for closed InApp Message

Event is normally tracked by calling `IExponeaSdk.TrackInAppMessageClose`. This method is tracking a delivered event only if:

* Tracking consent feature is disabled
* Tracking consent feature is enabled and 'hasTrackingConsent' has 'true' value

If you are using `IExponeaSdk.TrackInAppMessageClose` method manually and you want to avoid to consider tracking, you may use `IExponeaSdk.TrackInAppMessageCloseWithoutTrackingConsent` instead. This method will do track event ignoring tracking consent state.

### Event for opened AppInbox Message

Event is normally tracked by calling `IExponeaSdk.TrackAppInboxOpened`. This method is tracking a delivered event only if:

* Tracking consent feature is disabled
* Tracking consent feature is enabled and 'hasTrackingConsent' has 'true' value
* AppInbox has been loaded and given MessageItem is listed in AppInbox

If you are using `IExponeaSdk.TrackAppInboxOpened` method manually and you want to avoid to consider tracking, you may use `IExponeaSdk.TrackAppInboxOpenedWithoutTrackingConsent` instead. This method will do track event ignoring tracking consent state.

### Event for clicked AppInbox Message action

Event is normally tracked by calling `IExponeaSdk.TrackAppInboxClick`. This method is tracking a clicked event only if:

* Tracking consent feature is disabled
* Tracking consent feature is enabled and 'hasTrackingConsent' has 'true' value
* Action URL contains 'xnpe_force_track' with 'true' value independently from 'hasTrackingConsent' value

> Event that is tracked because of `xnpe_force_track` (forced tracking) will contains an additional property `tracking_forced` with value `true`

If you are using `IExponeaSdk.TrackAppInboxClick` method manually and you want to avoid to consider tracking, you may use `IExponeaSdk.TrackAppInboxClickWithoutTrackingConsent` instead. This method will do track event ignoring tracking consent state.
