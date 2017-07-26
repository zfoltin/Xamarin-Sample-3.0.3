using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace JudoDotNetXamarin
{
	public abstract class CardNetworkBase
	{
		protected bool _avsEnabled = false;
		private const string _cardNumberFormat = "0000 0000 0000 0000";

		public virtual int GetSecurityCodeLength()
		{
			return 3;
		}

		public virtual int GetIssueNumberLength()
		{
			return 3;
		}

		public virtual int GetCardNumberLength()
		{
			return 16;
		}

		public virtual int GetFormattedCardNumberLength()
		{
			return _cardNumberFormat.Length;
		}

		public virtual string CardNumberFormat()
		{
			return _cardNumberFormat;
		}

		public virtual bool ShouldDisplayIssueNumber()
		{
			return false;
		}

		public virtual bool ShouldDisplayStartDate()
		{
			return false;
		}

		public virtual List<CardPart> GetPartsNeededToBeValid(bool isTokenPayment)
		{
			var parts = new List<CardPart> { CardPart.SecurityCode };

			if (!isTokenPayment)
			{
				parts.Add(CardPart.CardNumber);
				parts.Add(CardPart.ExpiryDate);
			}

			if (_avsEnabled)
			{
				parts.Add(CardPart.Postcode);
			}

			return parts;
		}

		public void SetAvsEnabled(bool enabled)
		{
			_avsEnabled = enabled;
		}

		public bool CardNetworkAcceptedForTransaction(CardNetwork discoveredCardNetwork, List<CardNetwork> acceptedCardNetworksForTransaction)
		{
			return (acceptedCardNetworksForTransaction ?? new List<CardNetwork>()).Any(x => x == discoveredCardNetwork);
		}

		protected bool CardNumberIsOfNetworkType(string cardNumber, List<string> prefixes)
		{
			return CardNumberIsPrefixMatch(cardNumber, prefixes);
		}

		protected bool IsCardNumberValid(string unvalidatedCardNumber, string regex, List<string> prefixes, int expectedlength)
		{
			return 	
				CardNumberIsCorrectLength(unvalidatedCardNumber, expectedlength)
				&& Luhn.IsValid(unvalidatedCardNumber) 
				&& (prefixes.Any(unvalidatedCardNumber.StartsWith) || CardNumberIsRegexMatch(unvalidatedCardNumber, regex));
		}

		protected bool CardNumberIsCorrectLength(string cardNumber, int expectedLength)
		{
			return cardNumber.Length == expectedLength;
		}

		protected bool CardNumberIsPrefixMatch(string cardNumber, List<string> prefixes) 
		{
			return prefixes.Any(cardNumber.StartsWith);
		}

		protected bool CardNumberIsRegexMatch(string unvalidatedCardNumber, string regex)
		{
			Match match = Regex.Match(regex, unvalidatedCardNumber, RegexOptions.IgnoreCase);
			return match.Success;
		}
	}
}