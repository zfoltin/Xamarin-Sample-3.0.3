using System.Linq;

namespace JudoDotNetXamarin
{
	public class TokenPaymentDefaultsViewModel : DefaultsViewModelBase
	{ 
		public string LastFour { get; private set; }
		public string ExpiryDate { get; private set; }
		public string CardToken { get; private set; }
		public string ConsumerToken { get; private set; }
		public string MaskedCardNumber { get; private set; }
		public ICardNetwork CardNetork { get; private set; }

		public TokenPaymentDefaultsViewModel(string lastFour, string expiryDate, string cardToken, string consumerToken, CardNetwork cardNetwork)
		{
			var unformattedLastFour = lastFour ?? string.Empty;
			var unformattedExpiryDate = expiryDate ?? string.Empty;
			CardToken = cardToken;
			ConsumerToken = consumerToken;

			CardNetork = CardNetorkDiscoverer.DiscoverCardNetwork(cardNetwork);

			var masklength = CardNetork.GetCardNumberLength() - unformattedLastFour.Length;
			var mask = string.Concat(Enumerable.Repeat("*", masklength));

			MaskedCardNumber = string.Concat(mask, lastFour).FormatToJudoString(CardNetork.CardNumberFormat());
			ExpiryDate = unformattedExpiryDate.FormatToJudoString(DateFormat);
		}
	}
}
