using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightChecker.Models
{
    public class Flight
    {
        public DateTime Outbound { get; set; }
        public DateTime? Inbound { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public decimal Price { get; set; }
    }
}