using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JudoDotNetXamarin
{
	public class TokenPreAuthPage : CardEntryPage
	{
		TokenPaymentViewModel tokenViewModel = new TokenPaymentViewModel();
		INetworkDetector _networkDetector = DependencyService.Get<INetworkDetector>();

		public TokenPreAuthPage(Judo judo, TokenPaymentDefaultsViewModel model) : base(judo, model)
		{
			tokenViewModel.Amount = Judo.Amount;
			tokenViewModel.JudoID = Judo.JudoId;
			tokenViewModel.Currency = Judo.Currency;
			tokenViewModel.ConsumerReference = Judo.ConsumerReference;
			tokenViewModel.Token = model.CardToken;
			tokenViewModel.ConsumerToken = model.ConsumerToken;
		}

		protected override async Task OnSubmit(CardViewModel card, Dictionary<string, object> clientDetails)
		{
			if (await _networkDetector.IsConnected())
			{
				ShowLoading();

				tokenViewModel.CV2 = card.SecurityCode;
				tokenViewModel.PostCode = card.Postcode;
				tokenViewModel.Country = card.Country;
				tokenViewModel.ClientDetails = clientDetails;

				Presenter.HandleResult(await PaymentService.TokenPreAuth(tokenViewModel));
			}
			else
			{
				await OnDisplayConnectionError();
			}
		}
	}
}
