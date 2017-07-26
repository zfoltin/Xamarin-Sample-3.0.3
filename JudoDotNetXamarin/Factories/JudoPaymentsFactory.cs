using System.Collections.Generic;
using JudoPayDotNet;
using JudoPayDotNet.Authentication;
using JudoPayDotNet.Http;
using JudoPayDotNet.Enums;
using Xamarin.Forms;
using System.Net.Http.Headers;

namespace JudoDotNetXamarin
{
	public static class JudoPaymentsFactory
	{
		const string LiveUrl = "https://GW1.judopay.com/";
		const string SandboxUrl = "https://GW1.judopay-sandbox.com/";

		const string ApiVersion = "5.2.0.0";
		const string ApiVersionHeader = "api-version";

		static JudoPayApi Create(Credentials credentials, IEnumerable<ProductInfoHeaderValue> userAgent, JudoEnvironment environment)
		{
			var httpClientHelper = DependencyService.Get<IHttpClientHelper>();
			var clientService = DependencyService.Get<IClientService>();
			var logger = XamarinLoggerFactory.Create(typeof(AuthorizationHandler));

			var handler = new NativeHandler(httpClientHelper.MessageHandler, credentials,
									   logger, ApiVersionHeader, ApiVersion, clientService);

			HttpClientWrapper httpClient = new HttpClientWrapper(userAgent, handler);
			Client client = GetClient(environment, httpClient);

			return new JudoPayApi(XamarinLoggerFactory.Create, client);
		}

		static Client GetClient(JudoEnvironment environment, HttpClientWrapper httpClient)
		{
			var connection = new Connection(httpClient,
								 XamarinLoggerFactory.Create,
								 GetBaseUrl(environment));
			return new Client(connection);
		}

		static string GetBaseUrl(JudoEnvironment environment)
		{
			switch (environment)
			{
				case JudoEnvironment.Live:
					return LiveUrl;
				case JudoEnvironment.Sandbox:
					return SandboxUrl;
				default:
					return "";
			}
		}

		public static JudoPayApi Create(JudoEnvironment environment, string token, string secret, IEnumerable<ProductInfoHeaderValue> userAgent)
		{
			return Create(new Credentials(token, secret), userAgent, environment);
		}
	}
}