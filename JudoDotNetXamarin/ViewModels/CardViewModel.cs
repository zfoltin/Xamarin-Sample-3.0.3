using JudoPayDotNet.Models;

namespace JudoDotNetXamarin
{
    public class CardViewModel
    {
        /// <summary>
        /// Card Number 
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// Card expiry date 
        /// </summary>
        public string ExpiryDate { get; set; }

        /// <summary>
        /// Card CV2 number
        /// </summary>
        public string SecurityCode { get; set; }

        /// <summary>
        /// Card Type
        /// </summary>
        internal CardType CardType { get; set; }

        /// <summary>
        /// Postcode
        /// </summary>
        public string Postcode { get; set; }

        /// <summary>
        /// ISO standard CountryCode
        /// </summary>
		public ICountry Country { get; set; }

        /// <summary>
        /// card start date 
        /// </summary>
        public string StartDate { get; set; }

        /// <summary>
        /// Issue Number for Mestro card
        /// </summary>
        public string IssueNumber { get; set; }

        public CardViewModel Clone ()
        {
            return new CardViewModel {
                CardNumber = this.CardNumber,
                ExpiryDate = this.ExpiryDate,
                SecurityCode = this.SecurityCode,
                CardType = this.CardType,
                Postcode = this.Postcode,
                Country = this.Country,
                StartDate = this.StartDate,
                IssueNumber = this.IssueNumber,
            }; 
        }
    }
}

