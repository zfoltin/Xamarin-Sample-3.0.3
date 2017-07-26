using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace JudoDotNetXamarin
{
	public class PaymentPage : CardEntryPage
	{
		private INetworkDetector _networkDetector = DependencyService.Get<INetworkDetector>();

		public PaymentPage(Judo judo, PaymentDefaultsViewModel defaults) : base(judo, defaults)
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

		public PaymentPage(Judo judo) : this(judo, new PaymentDefaultsViewModel()) { }

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

				Presenter.HandleResult(await PaymentService.Payment(payment));
			}
			else
			{
				await OnDisplayConnectionError();
			}
		}
	}
}