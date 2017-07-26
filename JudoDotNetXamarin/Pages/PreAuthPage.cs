using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JudoDotNetXamarin
{
	public class PreAuthPage : CardEntryPage
	{
		private INetworkDetector _networkDetector = DependencyService.Get<INetworkDetector>();

		public PreAuthPage(Judo judo, PaymentDefaultsViewModel defaults) : base(judo, defaults)
		{
			if (!Judo.AmexAccepted)
			{
				SetAmexAsUnaccepted();
			}

			if (!Judo.MaestroAccepted)
			{
				SetMaestroAsUnaccepted();
			}
		}

		public PreAuthPage(Judo judo) : this(judo, new PaymentDefaultsViewModel()) { }

		protected override async Task OnSubmit(CardViewModel card, Dictionary<string, object> clientDetails)
		{
			if (await _networkDetector.IsConnected())
			{
				ShowLoading();

				var payment = new PaymentViewModel();
				payment.Card = card;
				payment.Amount = Judo.Amount;
				payment.JudoID = Judo.JudoId;
				payment.ConsumerReference = Judo.ConsumerReference;
				payment.Currency = Judo.Currency;
				payment.ClientDetails = clientDetails;

				Presenter.HandleResult(await PaymentService.PreAuth(payment));
			}
			else
			{
				await DisplayAlert("Can't connect", "Please check your internet connection", "OK");
			}
		}
	}
}
