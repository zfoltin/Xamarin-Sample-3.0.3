using System;
using System.Collections.Generic;
using System.Linq;

namespace JudoDotNetXamarin
{
	public class CardNetworkDiscoverer
	{
		private readonly List<ICardNetwork> _cardNetworks = new List<ICardNetwork>
		{
			new VisaCardNetwork(),
			new MastercardCardNetwork(),
			new MaestroCardNetwork(),
			new AmexCardNetwork(),
			new UnknownCardNetwork()
		};

		private ICardNetwork _defaultCardNetworkIfNotDiscovered = new UnknownCardNetwork();

		public ICardNetwork DiscoverCardNetwork(string cardNumber)
		{
			var discoverableCardNetworks = _cardNetworks.Where(x => x.GetType() != typeof(UnknownCardNetwork));
			var matchingNetwork = discoverableCardNetworks.FirstOrDefault(x => x.CardNumberIsOfNetworkType(cardNumber));
			return matchingNetwork ?? _defaultCardNetworkIfNotDiscovered;
		}

		public ICardNetwork DiscoverCardNetwork(CardNetwork cardNetwork)
		{
			return _cardNetworks.SingleOrDefault(x => x.GetCardNetworkType() == cardNetwork) ?? _defaultCardNetworkIfNotDiscovered;
		}

		public List<CardNetwork> GetAvailableCardNetworks()
		{
			return _cardNetworks.Select(x => x.GetCardNetworkType()).ToList();
		}
	}
}

