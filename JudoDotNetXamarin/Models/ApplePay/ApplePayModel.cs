using System.Collections.Generic;
using System.Linq;

namespace JudoDotNetXamarin
{
	public class ApplePayModel
	{ 
		public List<ApplePayItem> Items { get; set; }
		public string CurrencyCode { get; set; }
		public string CountryCode { get; set; }
		public string ConsumerRef { get; set; }
		public string MerchantIdentifier { get; set; }
		public string ItemsSummaryLabel { get; set; }
		public List<ApplePayCardNetwork> SupportedCardNetworks { get; set; }

		public ApplePayModel()
		{
			Items = new List<ApplePayItem>();
			SupportedCardNetworks = new List<ApplePayCardNetwork>();
		}

		public decimal ItemsTotalAmount()
		{
			return Items.Sum(x => x.Amount);
		}
	}
}
