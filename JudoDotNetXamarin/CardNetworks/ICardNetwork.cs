using System.Collections.Generic;
using Xamarin.Forms;

namespace JudoDotNetXamarin
{
	public interface ICardNetwork
	{
		bool IsCardNumberValid(string unvalidatedCardNumber);
		string GetSecurityCodeLabel();
		int GetSecurityCodeLength();
		int GetCardNumberLength();
		int GetFormattedCardNumberLength();
		CardNetwork GetCardNetworkType();
		bool ShouldDisplayStartDate();
		bool ShouldDisplayIssueNumber();
		bool CardNumberIsOfNetworkType(string cardNumber);
		ImageSource GetCardImageSource();
		ImageSource GetSecurityCodeImageSource();
		string CardNumberFormat();
		int GetIssueNumberLength();
		List<CardPart> GetPartsNeededToBeValid(bool isTokenPayment);
		void SetAvsEnabled(bool enabled);
		bool CardNetworkAcceptedForTransaction(CardNetwork discoveredCardNetwork, List<CardNetwork> acceptedCardNetworksForTransaction);
	}
}
