using JudoPayDotNet.Enums;

namespace JudoDotNetXamarin
{
	public class Judo
	{
		public string JudoId { get; set; }

		public string Token { get; set; }

		public string Secret { get; set; }

		public string DeviceId { get; set; }

		public string Currency { get; set; }

		public string ConsumerReference { get; set; }

		public decimal Amount { get; set; }

		public bool AvsEnabled { get; set; }

		public bool AmexAccepted { get; set; } = true;

		public bool MaestroAccepted { get; set; } = true;

		public bool RootedDevicesAllowed { get; set; } = true;

		public bool SslPinningEnabled { get; set; } = true;

		public JudoEnvironment Environment { get; set; }

		public Theme Theme { get; set; } = DefaultTheme.Theme;

		public void Validate()
		{
			if (string.IsNullOrWhiteSpace(JudoId) || !Luhn.IsValid(JudoId) || string.IsNullOrWhiteSpace(Token) || string.IsNullOrWhiteSpace(Secret))
			{
				throw new TokenSecretException();
			}
		}
	}
}