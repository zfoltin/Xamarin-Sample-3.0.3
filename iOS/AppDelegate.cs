using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using JudoDotNetXamariniOSSDK;
using UIKit;
using Xamarin.Forms;

namespace FormsApp.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();

			LoadApplication(new App());

			DependencyService.Register<ClientService>();
			DependencyService.Register<HttpClientHelper>();

			// Required if using Apple Pay
			DependencyService.Register<ApplePayService>();

			return base.FinishedLaunching(app, options);
		}
	}
}
