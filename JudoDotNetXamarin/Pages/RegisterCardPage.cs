using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JudoDotNetXamarin
{
	public class RegisterCardPage : CardEntryPage
	{
		private INetworkDetector _networkDetector = DependencyService.Get<INetworkDetector>();

		public RegisterCardPage(Judo judo, PaymentDefaultsViewModel defaults) : base(judo, defaults)
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

		public RegisterCardPage(Judo judo) : this(judo, new PaymentDefaultsViewModel()) { }

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

				Presenter.HandleResult(await PaymentService.RegisterCard(payment));
			}
			else
			{
				await OnDisplayConnectionError();
			}
		}

		protected override string GetLoadingOverlayTitleLabel() => "Adding card";

		protected override string GetDefaultButtonLabel() => "Add card";

		protected override string GetDefaultTitle() => "Add card";
	}
}
