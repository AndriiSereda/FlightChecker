using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlightChecker.Models;

namespace FlightChecker.BLL
{
    public class FlightDataSanitizer : IDataSanitizer<Flight>
    {
        public IEnumerable<Flight> Sanitize(IEnumerable<Flight> data)
        {
            throw new NotImplementedException();
        }
    }
}