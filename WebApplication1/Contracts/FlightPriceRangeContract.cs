using System.Runtime.Serialization;

namespace FlightChecker.Contracts
{
    
    public class FlightPriceRangeContract : IPriceRange
    {
        
        public decimal MinimumPrice { get; set; }
        
        public decimal MaximumPrice { get; set; }

        //public FlightPriceRangeContract(decimal minPrice, decimal maxPrice)
        //{
        //    MinimumPrice = minPrice;
        //    MaximumPrice = maxPrice;
        //}
    }
}