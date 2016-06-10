namespace FlightChecker.Contracts
{

    public class FlightPriceRangeContract : IPriceRange
    {
        public decimal MinimumPrice { get; set; }

        public decimal MaximumPrice { get; set; }
        
    }
}