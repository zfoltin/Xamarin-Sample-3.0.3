The Judopay library lets you integrate card payments into your Xamarin.Forms project. It is built to be mobile first with ease of integration in mind. Judopay's SDK enables a faster, simpler and more secure payment experience within your app. Build trust and user loyalty in your app with our secure and intuitive UX.

##### **\*\*\*Due to industry-wide security updates, versions below 2.3.0 of this SDK will no longer be supported after 1st Oct 2016. For more information regarding these updates, please read our blog [here](http://hub.judopay.com/pci31-security-updates/ "Security Blog").*****

## Requirements
- Xamarin Studio 6.1 / Visual Studio 2015
- Xamarin Forms 2.3.2.127
- Xcode 8
- Android 7.0 (API 24) SDK and build tools 24.0.3 installed

The SDK is compatible with Android Jelly Bean (4.1) and above and iOS 8 and above.

## Getting started

#### 1. Integration

In the Xamarin component store, search for 'Judopay' and add the component to your app, Android and iOS projects.

#### 2. Setup

In your Xamarin Forms page, create a new Judo instance:

```csharp
var judo = new Judo()
{
    JudoId = "<JUDO_ID>",
    ApiToken = "<API_TOKEN>",
    ApiSecret = "<API_SECRET>",
    Environment = JudoEnvironment.Sandbox,
    Amount = 1.50m,
    Currency = "GBP",
    ConsumerReference = "YourUniqueReference"
};
```

__Note:__ Please make sure that you are using a unique Consumer Reference for each different consumer.

#### 3. Configure iOS and Android projects

Android and iOS require additional steps to get set up with the Xamarin SDK. See the [guide to initializing the SDK](https://github.com/JudoPay/Judo-Xamarin/wiki/Initializing-the-SDK) for more information.

#### 4. Make a payment

Create a PaymentPage to show the card payment screen:

```chsarp
var paymentPage = new PaymentPage(judo);
Navigation.PushAsync(paymentPage);
```

#### 4. Check the payment result

Receive the result of the payment:

```csharp
paymentPage.resultHandler += async (sender, result) =>
{
	if ("Success".Equals(result.Response.Result))
	{
		// handle successful payment
		// close payment page
		await Navigation.PopAsync();
	}
};
```

## Next steps

The Judopay Xamarin library supports a range of customization options. For more information on using Judopay for Xamarin see our [wiki documentation](https://github.com/JudoPay/Judo-Xamarin/wiki).
