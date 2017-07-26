using System.Collections.Generic;
using Xamarin.Forms;

namespace JudoDotNetXamarin
{

	public class MaestroCardNetwork : CardNetworkBase, ICardNetwork
	{
		private string _regex = "^(5018|5020|5038|6304|6759|6761|6763|6334|6767|4903|4905|4911|4936|564182|633110|6333|6759|5600|5602|5603|5610|5611|5656|6700|6706|6773|6775|6709|6771|6773|6775).*?";
		private List<string> _prefixes = new List<string> { "56", "57", "58", "59", "50", "63", "67", "06" };

		public CardNetwork GetCardNetworkType()
		{
			return CardNetwork.MAESTRO;
		}

		public string GetSecurityCodeLabel()
		{
			return "CVV";
		}

		public ImageSource GetCardImageSource()
		{
			return "ic_card_maestro.png";
		}

		public ImageSource GetSecurityCodeImageSource()
		{
			return "ic_card_cv2.png";
		}

		public override bool ShouldDisplayIssueNumber()
		{
			return true;
		}

		public override bool ShouldDisplayStartDate()
		{
			return true;
		}

		public bool IsCardNumberValid(string unvalidatedCardNumber)
		{
			return IsCardNumberValid(unvalidatedCardNumber, _regex, _prefixes, GetCardNumberLength());
		}

		public bool CardNumberIsOfNetworkType(string cardNumber)
		{
			return CardNumberIsOfNetworkType(cardNumber, _prefixes);
		}

		public override List<CardPart> GetPartsNeededToBeValid(bool isTokenPayment)
		{
			var parts = base.GetPartsNeededToBeValid(isTokenPayment);
			parts.Add(CardPart.IssueNumber);
			parts.Add(CardPart.StartDate);
			return parts;
		}
	}
	
}
