using System.Threading.Tasks;

namespace JudoDotNetXamarin
{
	public interface INetworkDetector
	{
		Task<bool> IsConnected();
	}
}