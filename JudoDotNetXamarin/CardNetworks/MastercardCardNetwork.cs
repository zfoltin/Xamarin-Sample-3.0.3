using System.Collections.Generic;
using Xamarin.Forms;

namespace JudoDotNetXamarin
{

	public class MastercardCardNetwork : CardNetworkBase, ICardNetwork
	{
		private string _regex = "^5[1-5][0-9]{2}.*?";
		private List<string> _prefixes = new List<string> { /*"50",*/ "51", "52", "53", "54", "55" };

		public CardNetwork GetCardNetworkType()
		{
			return CardNetwork.MASTERCARD;
		}

		public string GetSecurityCodeLabel()
		{
			return "CVC2";
		}

		public ImageSource GetCardImageSource()
		{
			return "ic_card_mastercard.png";
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
