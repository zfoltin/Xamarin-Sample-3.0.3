using Xamarin.Forms;

namespace JudoDotNetXamarin
{

	public class UnknownCardNetwork : CardNetworkBase, ICardNetwork
	{
		public CardNetwork GetCardNetworkType()
		{
			return CardNetwork.UNKNOWN;
		}

		public string GetSecurityCodeLabel()
		{
			return "CVV";
		}

		public ImageSource GetCardImageSource()
		{
			return "ic_card_unknown.png";
		}

		public ImageSource GetSecurityCodeImageSource()
		{
			return "ic_card_cv2.png";
		}

		public bool IsCardNumberValid(string unvalidatedCardNumber)
		{
			return CardNumberIsCorrectLength(unvalidatedCardNumber, GetCardNumberLength()) && Luhn.IsValid(unvalidatedCardNumber);
		}

		public bool CardNumberIsOfNetworkType(string cardNumber)
		{
			return true;
		}
	}
	
}
