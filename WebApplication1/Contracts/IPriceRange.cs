namespace FlightChecker.Contracts
{
    public interface IPriceRange
    {
        decimal MinimumPrice { get; set; }

        decimal MaximumPrice { get; set; }
    }
}
