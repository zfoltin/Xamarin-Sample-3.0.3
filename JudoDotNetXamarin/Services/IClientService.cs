using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace JudoDotNetXamarin
{
	public interface IClientService
	{
		string GetSdkVersion();

    	IEnumerable<ProductInfoHeaderValue> GetUserAgent();

		Task<JObject> IdentifyDevice(Judo judo, Dictionary<string, object> signals);
	}
}