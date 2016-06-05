using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.BLL
{
    public class FlightDataSanitizer : IDataSanitizer<Flight>
    {
        public IEnumerable<Flight> Sanitize(IEnumerable<Flight> data)
        {
            throw new NotImplementedException();
        }
    }
}