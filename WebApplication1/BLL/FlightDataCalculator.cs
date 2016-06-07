using FlightChecker.BLL;
using FlightChecker.Contracts;
using FlightChecker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightChecker.BLL
{
    public class FlightDataCalculator
    {
        private IDataSanitizer<Flight> _dataSanitizer;

        public FlightDataCalculator(IDataSanitizer<Flight> dataSanitizer)
        {
            _dataSanitizer = dataSanitizer;

        }

        public IPriceRange CalculateFlightPriceRange(IEnumerable<Flight> collection)
        {
            var listOfOneWayFlights = collection.Where(x => !x.Inbound.HasValue);
            var listOfTwoWayFlights = collection.Where(x => x.Inbound.HasValue);
            List<decimal> listOfMinimumAndMaximumPrices = new List<decimal>();

 
            if (listOfOneWayFlights.Any())
            {
                listOfOneWayFlights = _dataSanitizer.SanitizeAndSortCollection(listOfOneWayFlights);
                listOfMinimumAndMaximumPrices.Add(listOfOneWayFlights.First().Price);
                listOfMinimumAndMaximumPrices.Add(listOfOneWayFlights.Last().Price);
            }

            if (listOfTwoWayFlights.Any())
            {
                listOfTwoWayFlights = _dataSanitizer.SanitizeAndSortCollection(listOfTwoWayFlights);
                listOfMinimumAndMaximumPrices.Add(listOfTwoWayFlights.First().Price);
                listOfMinimumAndMaximumPrices.Add(listOfTwoWayFlights.Last().Price);
            }

            var result = new FlightPriceRangeContract(listOfMinimumAndMaximumPrices.Min(), listOfMinimumAndMaximumPrices.Max());

            return result;
        }
    }
}