using System.Runtime.Serialization;

namespace FlightChecker.Contracts
{
    [DataContract]
    public class FlightPriceRangeContract : IPriceRange
    {
        [DataMember]
        public decimal MinimumPrice { get; set; }
        [DataMember]
        public decimal MaximumPrice { get; set; }

        public FlightPriceRangeContract(decimal minPrice, decimal maxPrice)
        {
            MinimumPrice = minPrice;
            MaximumPrice = maxPrice;
        }
    }
}