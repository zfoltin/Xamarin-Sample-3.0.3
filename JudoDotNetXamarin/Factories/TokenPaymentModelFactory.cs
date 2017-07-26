using System;
using JudoPayDotNet.Models;
using Newtonsoft.Json.Linq;

namespace JudoDotNetXamarin
{
	public class TokenPaymentModelFactory : BasePaymentModelFactory
	{
		internal TokenPaymentModel Create(TokenPaymentViewModel paymentViewModel, Judo judo, JObject jObject)
		{
			var tokenPaymentModel = new TokenPaymentModel();

			tokenPaymentModel.JudoId = (string.IsNullOrWhiteSpace(paymentViewModel.JudoID) ? judo.JudoId : paymentViewModel.JudoID);
			tokenPaymentModel.YourConsumerReference = ConsumerReference(paymentViewModel, judo);
			tokenPaymentModel.Amount = paymentViewModel.Amount;
			tokenPaymentModel.CardToken = paymentViewModel.Token;
			tokenPaymentModel.CV2 = paymentViewModel.CV2;
	
			tokenPaymentModel.ConsumerToken = paymentViewModel.ConsumerToken;
			tokenPaymentModel.YourPaymentMetaData = paymentViewModel.YourPaymentMetaData;
			tokenPaymentModel.ClientDetails = jObject;

			return tokenPaymentModel;
		}
	}
}
