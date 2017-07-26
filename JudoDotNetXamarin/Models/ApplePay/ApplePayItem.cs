namespace JudoDotNetXamarin
{
	public class ApplePayItem
	{
		public string Label { get; private set; }
		public decimal Amount { get; private set; }

		public ApplePayItem(string label, decimal amount)
		{
			Label = label;
			Amount = amount;
		}
	}

}
