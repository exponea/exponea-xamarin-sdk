

## 🚀 Fetching Data

Exponea Xamarin SDK has some methods to retrieve your data from the Exponea web application.

#### Get customer recommendation

Get items recommended for a customer.

#### 💻 Usage

``` csharp
_exponea.FetchRecommendationsAsync(
         new RecommendationsRequest(
                 id: recommendationId.Text,
                 fillWithRandom: true
         )
);
```

#### Consent Categories

Fetch the list of your existing consent categories.

#### 💻 Usage=

``` csharp
_exponea.FetchConsentsAsync();
```