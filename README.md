
# Xamarin Exponea SDK
Xamarin Exponea SDK allows your application to interact with the [Exponea](https://exponea.com/) Customer Data & Experience Platform. Exponea empowers B2C marketers to raise conversion rates, improve acquisition ROI, and maximize customer lifetime value.


SDK is created as .net wrapper for binding libraries of [native Android SDK](https://github.com/exponea/exponea-android-sdk) and [native iOS SDK](https://github.com/exponea/exponea-ios-sdk).


## Getting started

 - Add ExponeaSDK NuGet as a dependency
 - Register dependency service in Android MainActivity.OnCreate (iOS AppDelegate.FinishedLaunching) method by calling

 ```csharp
 DependencyService.Register<IExponeaSdk, Exponea.ExponeaSdk>();
 ```
 - Get Exponea SDK instance by calling 

 ```csharp
   public IExponeaSdk _exponea = DependencyService.Get<IExponeaSdk>();
 ```
- Call Exponea SDK methods on the obtained instance.


## Documentation
  * [Configuration](./documentation/CONFIG.md)
  * [Tracking](./documentation/TRACK.md)
  * [Fetching](./documentation/FETCH.md)
  * [Flushing](./documentation/FLUSH.md)
  * [Anonymize customer](./documentation/ANONYMIZE.md)
  * [In-app messages](./documentation/IN_APP_MESSAGES.md)
  * [Project mapping](./documentation/PROJECT_MAPPING.md)
  * [Android push notifications](./documentation/ANDROID_PUSH.md)
  * [iOS push notifications](./documentation/IOS_PUSH.md)
  * [Android univerzal links](./documentation/ANDROID_UNIVERZAL_LINKS.md)
  * [iOS univerzal links](./documentation/IOS_UNIVERSAL_LINKS.md)
  * [SDK version update guide](./documentation/VERSION_UPDATE.md)
  
## Release Notes

[Release notes](./documentation/RELEASE_NOTES.md) for the SDK.

## Support

Are you a Bloomreach customer and dealing with some issues on mobile SDK? You can reach the official Engagement Support [via these recommended ways](https://documentation.bloomreach.com/engagement/docs/engagement-support#contacting-the-support).
Note that Github repository issues and PRs will also be considered but with the lowest priority and without guaranteed output.
