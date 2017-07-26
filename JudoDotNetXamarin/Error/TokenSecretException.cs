using System;
namespace JudoDotNetXamarin
{
	public class TokenSecretException : Exception
	{
		public TokenSecretException() : base("Judo Token or Secret is not configured correctly, please configure when creating the Judo instance") { }
	}
}