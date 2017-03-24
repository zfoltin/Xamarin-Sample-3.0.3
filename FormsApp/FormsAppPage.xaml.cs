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
				JudoId = "<JUDO_ID>",
				ApiToken = "<TOKEN>",
				ApiSecret = "<SECRET>",
				Environment = JudoEnvironment.Sandbox,
				Amount = 1.50m,
				Currency = "GBP",
				ConsumerReference = "<CONSUMER_REFERENCE>"
			};

			var paymentPage = new PaymentPage(judo);
			Navigation.PushAsync(paymentPage);

			paymentPage.resultHandler += async (sender, result) =>
			{
				if ("Success".Equals(result.Response.Result))
				{
					await Navigation.PopAsync();
				}
			};
		}
	}
}
