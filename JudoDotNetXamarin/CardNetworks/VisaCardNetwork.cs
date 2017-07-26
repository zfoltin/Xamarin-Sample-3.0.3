using System.Collections.Generic;
using Xamarin.Forms;

namespace JudoDotNetXamarin
{

	public class VisaCardNetwork : CardNetworkBase, ICardNetwork
	{
		private string _regex = "^4[0-9]{3}.*?";
		private List<string> _prefixes = new List<string> { "4" };

		public CardNetwork GetCardNetworkType()
		{
			return CardNetwork.VISA;
		}

		public string GetSecurityCodeLabel()
		{
			return "CVV2";
		}

		public ImageSource GetCardImageSource()
		{
			return "ic_card_visa.png";
		}

		public ImageSource GetSecurityCodeImageSource()
		{
			return "ic_card_cv2.png";
		}

		public bool IsCardNumberValid(string unvalidatedCardNumber)
		{
			return IsCardNumberValid(unvalidatedCardNumber, _regex, _prefixes, GetCardNumberLength());
		}

		public bool CardNumberIsOfNetworkType(string cardNumber)
		{
			return CardNumberIsOfNetworkType(cardNumber, _prefixes);
		}
	}
	
}
