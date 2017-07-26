namespace JudoDotNetXamarin
{
	public abstract class DefaultsViewModelBase
	{
		protected CardNetworkDiscoverer CardNetorkDiscoverer = new CardNetworkDiscoverer();
		protected const string DateFormat = "00/00";
	}

	public class PaymentDefaultsViewModel : DefaultsViewModelBase
	{
		public string _cardNumber;
		public string _expiryDate;
		public string _startDate;
		public string _issueNumber;

		public string CardNumber
		{
			get { return _cardNumber; }
			set
			{
				var network = CardNetorkDiscoverer.DiscoverCardNetwork(value);
				_cardNumber = value.FormatToJudoString(network.CardNumberFormat());
			}
		}

		public string ExpiryDate
		{
			get { return _expiryDate; }
			set
			{
				_expiryDate = value.FormatToJudoString(DateFormat);
			}
		}

		public string StartDate
		{
			get { return _startDate; }
			set
			{
				_startDate = value.FormatToJudoString(DateFormat);
			}
		}

		public string IssueNumber
		{
			get { return _issueNumber; }
			set
			{
				_issueNumber = value ?? string.Empty;
			}
		}
	}
}
