using System.Collections.Generic;

namespace JudoDotNetXamarin
{
	public class PaymentViewModel : BasePaymentViewModel
	{
		public CardViewModel Card { get; set; }

		public decimal Amount { get; set; }

		public string Currency { get; set; }

		public IDictionary<string, string> YourPaymentMetaData { get; set; }

		public PaymentViewModel Clone()
		{
			return new PaymentViewModel
			{
				Card = this.Card.Clone(),
				Amount = this.Amount,
				Currency = this.Currency,
				ConsumerReference = this.ConsumerReference,
				YourPaymentMetaData = this.YourPaymentMetaData,
				JudoID = this.JudoID
			};
		}
	}
}