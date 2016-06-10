using System.Collections.Generic;
using FlightChecker.Models;

namespace FlightChecker.Repository
{
    public interface IFlightPricesRepository
    {
        IEnumerable<Flight> GetFlightsFromOriginToDestination(string origin, string destination);
    }
}
