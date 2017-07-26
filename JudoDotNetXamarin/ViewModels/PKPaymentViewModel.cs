using Newtonsoft.Json.Linq;

namespace JudoDotNetXamarin
{
	public class PKPaymentViewModel : BasePaymentViewModel
	{ 
		/// <summary>
		/// Amount
		/// </summary>
		public decimal Amount { get; set; }

		/// <summary>
		/// Gets or sets the payment data.
		/// </summary>
		/// <value>The payment data.</value>
		public JObject PaymentData { get; set; }

		/// <summary>
		/// Gets or sets the name of the payment instrument.
		/// </summary>
		/// <value>The name of the payment instrument.</value>
		public string PaymentInstrumentName { get; set; }

		/// <summary>
		/// Gets or sets the payment network.
		/// </summary>
		/// <value>The payment network.</value>
		public string PaymentNetwork { get; set; }
	}
}