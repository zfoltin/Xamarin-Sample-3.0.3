
using System.Collections.Generic;

namespace JudoDotNetXamarin
{
    public class BasePaymentViewModel
    {
        public Dictionary<string, object> ClientDetails { get; set; }

		/// <summary>
		/// your consumer reference 
		/// </summary>
		public string ConsumerReference { get; set; }

        /// <summary>
        /// your JudoID, can be used to ovverride the value set within JudoConfiguration on a transactional bases
        /// JudoConfiguration MUST still be set as failover
        /// </summary>
        public string  JudoID { get; set; }
    }
}