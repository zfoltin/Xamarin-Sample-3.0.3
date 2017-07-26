using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace JudoDotNetXamarin
{
	public class AmexCardNetwork : CardNetworkBase, ICardNetwork
	{
		private string _regex = "^3[47][0-9]{2}.*?";
		private List<string> _prefixes = new List<string> { "34", "37" };

		private const string _cardNumberFormat = "0000 000000 00000";

		public CardNetwork GetCardNetworkType()
		{
			return CardNetwork.AMEX;
		}

		public override int GetSecurityCodeLength()
		{
			return 4;
		}

		public override int GetCardNumberLength()
		{
			return 15;
		}

		public override string CardNumberFormat()
		{
			return _cardNumberFormat;
		}

		public override int GetFormattedCardNumberLength()
		{
			return _cardNumberFormat.Length;
		}

		public string GetSecurityCodeLabel()
		{
			return "CID";
		}

		public ImageSource GetCardImageSource()
		{
			return "ic_card_amex.png";
		}

		public ImageSource GetSecurityCodeImageSource()
		{
			return "ic_card_cidv.png";
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
