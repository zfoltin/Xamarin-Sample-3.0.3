using System;
using System.Collections.Generic;

namespace JudoDotNetXamarin
{
	public class CardNumberValidator : IValidator
	{
		public ValidationResponse Validate(string entry, ICardNetwork cardNetwork, List<CardNetwork> acceptableCardNetworksForTransaction)
		{
			var strippedEntry = (entry ?? string.Empty).Replace(" ", "");
			var errorMessage = string.Empty;

			var networkAccepted = cardNetwork.CardNetworkAcceptedForTransaction(cardNetwork.GetCardNetworkType(), acceptableCardNetworksForTransaction);
			var cardNumberIsValid = cardNetwork.IsCardNumberValid(strippedEntry) && strippedEntry.Length >= cardNetwork.GetCardNumberLength();

			if (!networkAccepted)
			{
				errorMessage = string.Format("We do not accept {0}, please use other cards", cardNetwork.GetCardNetworkType().GetAttribute<DisplayName>().Name);
			}
			else if (!cardNumberIsValid && strippedEntry.Length >= cardNetwork.GetCardNumberLength())
			{
				errorMessage = "Check card number";
			}

			return new ValidationResponse(networkAccepted && cardNumberIsValid, errorMessage , !string.IsNullOrWhiteSpace(errorMessage));
		}
	}
}
