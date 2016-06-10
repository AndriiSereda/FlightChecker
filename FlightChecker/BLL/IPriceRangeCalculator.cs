using FlightChecker.Contracts;
using System.Collections.Generic;

namespace FlightChecker.BLL
{
    public interface IPriceRangeCalculator<T>
    {
        IPriceRange CalculatePriceRange(IEnumerable<T> collection);
        IPriceRange ConvertPriceRange(IPriceRange range, decimal rate);
    }
}
