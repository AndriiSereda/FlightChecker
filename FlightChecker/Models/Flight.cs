using System;


namespace FlightChecker.Models
{
    public class Flight : IComparable
    {
        public DateTime Outbound { get; set; }
        public DateTime? Inbound { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public decimal Price { get; set; }

        public int CompareTo(object obj)
        {
            Flight flight = (Flight)obj;
            return Decimal.Compare(this.Price, flight.Price);
        }
    }
}