

## 🔍 Tracking
Exponea SDK allows you to track events that occur while using the app and add properties of your customer. When SDK is first initialized, we generate a cookie for the customer that will be used for all the tracking. You can retrieve that cookie using `_exponea.CustomerCookie`.

> If you need to reset the tracking and start fresh with a new user, you can use [Anonymize](./ANONYMIZE.md) functionality.

## 🔍 Tracking Events
> Some events are tracked automatically. We track installation event once for every customer, and when `AutomaticSessionTracking` is enabled in [ExponeaConfiguration](./CONFIG.md) we automatically track session events.

You can define any event types for each of your projects based on your business model or current goals. If you have a product e-commerce website, your essential customer journey will probably/most likely be:

* Visiting your App
* Searching for a specific product
* Product page
* Adding product to the cart
* Going through the ordering process
* Payment

So the possible events for tracking will be: `search`, `product view`, `add a product to cart`, `checkout`, `purchase`. Remember that you can define any event names you wish. However, our recommendation is to make them self-descriptive and human-understandable.


#### 💻 Usage

``` csharp
 _exponea.Track(new Event("page_view") { ["thisIsAStringProperty"] = "thisIsAStringValue" }); 
```

## 🔍 Default Properties

It’s possible to set values in the [ExponeaConfiguration](../Documentation/CONFIG.md) to be sent in every tracking event. Once Exponea is configured, you can also change default properties calling `SetDefaultProperties`. Notice that those values will be overwritten if the tracking event has properties with the same key name. 

#### 💻 Usage 

``` csharp
 _exponea.SetDefaultProperties(new Dictionary<string, object>()
            {
                { "thisIsADefaultStringProperty", "This is a default string value" },
                { "thisIsADefaultIntProperty", 1},
                { "thisIsADefaultDoubleProperty", 12.53623}

            });
```

## 🔍 Tracking Customer Properties

#### Identify Customer

Save or update your customer data in the Exponea APP through this method.


#### 💻 Usage 

``` csharp
 _exponea.IdentifyCustomer(new Customer("donald@exponea.com") { ["name"] = "John" });
```

