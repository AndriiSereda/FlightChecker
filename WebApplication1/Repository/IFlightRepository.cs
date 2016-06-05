using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    interface IFlightRepository
    {
        IEnumerable<Flight> GetFlightsFromOriginToDestination(string origin, string destination);
    }
}
