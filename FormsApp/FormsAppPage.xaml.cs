using JudoDotNetXamarin;
using JudoPayDotNet.Enums;
using Xamarin.Forms;

namespace FormsApp
{
	public partial class FormsAppPage : ContentPage
	{
		public FormsAppPage()
		{
			InitializeComponent();

			var judo = new Judo()
			{
				ApiToken = "<TOKEN>",
				ApiSecret = "<SECRET>",
				JudoId = "<JUDO_ID>",
				Environment = JudoEnvironment.Sandbox,
				Amount = 1.50m,
				Currency = "GBP",
				ConsumerReference = "2345678654324506"
			};

			var registerCardPage = new RegisterCardPage(judo);
			Navigation.PushAsync(registerCardPage);

			registerCardPage.resultHandler += async (sender, result) =>
			{
				if (result != null && result.Response != null && "Success".Equals(result.Response.Result))
				{
					await Navigation.PopAsync();
				}
			};
		}
	}
}
