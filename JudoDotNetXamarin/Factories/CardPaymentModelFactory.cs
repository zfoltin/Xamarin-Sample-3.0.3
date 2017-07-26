using System;
using JudoPayDotNet.Models;
using Newtonsoft.Json.Linq;

namespace JudoDotNetXamarin
{
	public class CardPaymentModelFactory : BasePaymentModelFactory
	{
		internal CardPaymentModel Create(PaymentViewModel paymentViewModel, Judo _judo, JObject clientDetails)
		{
			var paymentModel = new CardPaymentModel();

			paymentModel.JudoId = (string.IsNullOrWhiteSpace(paymentViewModel.JudoID) ? _judo.JudoId : paymentViewModel.JudoID);
			paymentModel.YourConsumerReference = ConsumerReference(paymentViewModel, _judo);
			paymentModel.Amount = paymentViewModel.Amount;
			paymentModel.CardNumber = paymentViewModel.Card.CardNumber;
			paymentModel.CV2 = paymentViewModel.Card.SecurityCode;
			paymentModel.ExpiryDate = paymentViewModel.Card.ExpiryDate;
			paymentModel.CardAddress = new CardAddressModel
			{
				PostCode = paymentViewModel.Card.Postcode,
				CountryCode = paymentViewModel.Card.Country.GetISOCode()
			};
			paymentModel.StartDate = paymentViewModel.Card.StartDate;
			paymentModel.IssueNumber = paymentViewModel.Card.IssueNumber;
			paymentModel.YourPaymentMetaData = paymentViewModel.YourPaymentMetaData;
			paymentModel.ClientDetails = clientDetails;
			paymentModel.Currency = paymentViewModel.Currency;

			return paymentModel;
		}
	}
}