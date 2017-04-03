using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Gms.Security;

namespace FormsApp.Droid
{
	[Activity(Label = "FormsApp.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, ProviderInstaller.IProviderInstallListener
	{
		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);

			if (Android.OS.Build.VERSION.SdkInt <= BuildVersionCodes.Kitkat)
			{
				ProviderInstaller.InstallIfNeededAsync(this, this);
			}

			LoadApplication(new App());
		}

		public void OnProviderInstalled()
		{
		}

		public void OnProviderInstallFailed(int errorCode, Intent recoveryIntent)
		{
		}
	}
}
