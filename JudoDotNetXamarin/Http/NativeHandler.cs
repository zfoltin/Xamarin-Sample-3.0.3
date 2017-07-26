using System;
using System.Net.Http;
using JudoPayDotNet.Authentication;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JudoDotNetXamarin
{
	public class NativeHandler : DelegatingHandler
	{
		readonly ILog _log;
		readonly Credentials _credentials;
		readonly string _apiVersionHeader;
		readonly string _apiVersionValue;
		readonly IClientService _clientService;

		public NativeHandler(HttpMessageHandler innerHandler, Credentials credentials, ILog log, string apiVersionHeader, string apiVersionValue, IClientService clientService) : base(innerHandler)
		{
			_log = log;
			_credentials = credentials;
			_clientService = clientService;
			_apiVersionHeader = apiVersionHeader;
			_apiVersionValue = apiVersionValue;
		}

		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			request.Headers.Add(_apiVersionHeader, _apiVersionValue);
			request.Headers.Add("Sdk-Version", _clientService.GetSdkVersion());
			//request.Headers.Add("User-Agent", _clientService.GetUserAgent());

			var full = string.Format("{0}:{1}", _credentials.Token, _credentials.Secret);

			var schema = AuthType.Basic.ToString();
			var authDetails = Encoding.GetEncoding("iso-8859-1").GetBytes(full);
			var parameter = Convert.ToBase64String(authDetails);

			if (!string.IsNullOrWhiteSpace(schema) && !string.IsNullOrWhiteSpace(parameter))
			{
				request.Headers.Authorization = new AuthenticationHeaderValue(schema, parameter);
			}

			return base.SendAsync(request, cancellationToken);
		}
	}
}