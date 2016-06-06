using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightChecker.Models;

namespace FlightChecker.Repository
{
    interface IFlightRepository
    {
        IEnumerable<Flight> GetFlightsFromOriginToDestination(string origin, string destination);
    }
}
