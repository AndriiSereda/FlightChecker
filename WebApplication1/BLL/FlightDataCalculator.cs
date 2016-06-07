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
        //let's not implemet Japanese or Croatian currency specifics
        private const int _decimalDelimeter = 2;

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

            var minPrice = Math.Round(listOfMinimumAndMaximumPrices.Min(), _decimalDelimeter);
            var maxPrice = Math.Round(listOfMinimumAndMaximumPrices.Max(), _decimalDelimeter);
            var result = new FlightPriceRangeContract(minPrice,maxPrice);            
            return result;
        }

        public IPriceRange ConvertFlightPriceRange(IPriceRange range,  decimal rate)
        {
            var minPriceRecalculated = Math.Round(range.MinimumPrice * rate, _decimalDelimeter);
            var maxPriceRecalculated = Math.Round(range.MaximumPrice * rate, _decimalDelimeter);
            range.MaximumPrice = maxPriceRecalculated;
            range.MinimumPrice = minPriceRecalculated;
            return range;
        }
    }
}