using JudoPayDotNet.Models;
using Newtonsoft.Json.Linq;

namespace JudoDotNetXamarin
{
	public class PKPaymentModelFactory : BasePaymentModelFactory
	{
		internal PKPaymentModel Create(PKPaymentViewModel paymentViewModel, Judo judo, JObject jObject)
		{
			var paymentModel = new PKPaymentModel();

			paymentModel.JudoId = (string.IsNullOrWhiteSpace(paymentViewModel.JudoID) ? judo.JudoId : paymentViewModel.JudoID);
			paymentModel.YourConsumerReference = ConsumerReference(paymentViewModel, judo);
			paymentModel.Amount = paymentViewModel.Amount;
			paymentModel.ClientDetails = jObject;
			paymentModel.PkPayment = new PKPaymentInnerModel()
			{
				Token = new PKPaymentTokenModel()
				{
					PaymentData = paymentViewModel.PaymentData,
					PaymentInstrumentName = paymentViewModel.PaymentInstrumentName,
					PaymentNetwork = paymentViewModel.PaymentNetwork
				}
			};

			return paymentModel;
		}
	}
}
