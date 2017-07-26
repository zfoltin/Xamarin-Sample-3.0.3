namespace JudoDotNetXamarin
{
	public class EuroCurrency : ICurrency
	{
		public string GetAbbreviation()
		{
			return "EUR";
		}

		public string GetSymbol()
		{
			return "€";
		}
	}
}
