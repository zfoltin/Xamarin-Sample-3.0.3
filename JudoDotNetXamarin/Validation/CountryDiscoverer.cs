using System;
using System.Collections.Generic;
using System.Linq;

namespace JudoDotNetXamarin
{
	public class CountryDiscoverer
	{
		private readonly List<ICountry> _countries = new List<ICountry>
		{           
			new UKCountry(),
			new USACountry(),
			new CanadaCountry(),
			new OtherCountry()
		};

		public ICountry DiscoverCountry(string countryName)
		{
			var country = (Country)Enum.Parse(typeof(Country), countryName);
			return _countries.Single(x => x.GetName() == country);
		}

		public List<string> GetCountryNames()
		{
			return _countries.Select(x => x.GetName().GetAttribute<DisplayName>().Name).ToList();
		}
	}
}
