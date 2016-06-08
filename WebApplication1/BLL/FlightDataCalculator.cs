using FlightChecker.Contracts;
using FlightChecker.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightChecker.BLL
{
    public class FlightDataCalculator : IPriceRangeCalculator<Flight>
    {
        private IDataSanitizer<Flight> _dataSanitizer;
        //let's not implemet Japanese or Croatian currency specifics
        private const int _decimalDelimeter = 2;

        public FlightDataCalculator(IDataSanitizer<Flight> dataSanitizer)
        {
            _dataSanitizer = dataSanitizer;
        }

        public IPriceRange CalculatePriceRange(IEnumerable<Flight> collection)
        {
            var listOfOneWayFlights = collection.Where(x => !x.Inbound.HasValue);
            var listOfTwoWayFlights = collection.Where(x => x.Inbound.HasValue);
            List<decimal> listOfMinimumAndMaximumPrices = new List<decimal>();

 
            if (listOfOneWayFlights.Any())
            {
                listOfOneWayFlights = _dataSanitizer.SanitizeAndSortCollection(listOfOneWayFlights);
                decimal price1 = listOfOneWayFlights.First().Price;
                decimal price2 = listOfOneWayFlights.Last().Price;
                listOfMinimumAndMaximumPrices.Add(price1);
                listOfMinimumAndMaximumPrices.Add(price2);
            }

            if (listOfTwoWayFlights.Any())
            {
                listOfTwoWayFlights = _dataSanitizer.SanitizeAndSortCollection(listOfTwoWayFlights);
                decimal price3 = listOfTwoWayFlights.First().Price;
                decimal price4 = listOfTwoWayFlights.Last().Price;
                listOfMinimumAndMaximumPrices.Add(price3);
                listOfMinimumAndMaximumPrices.Add(price4);
            }

            var minPrice = Math.Round(listOfMinimumAndMaximumPrices.Min(), _decimalDelimeter);
            var maxPrice = Math.Round(listOfMinimumAndMaximumPrices.Max(), _decimalDelimeter);
            var result = new FlightPriceRangeContract(minPrice,maxPrice);            
            return result;
        }

        public IPriceRange ConvertPriceRange(IPriceRange range,  decimal rate)
        {
            var minPriceRecalculated = Math.Round(range.MinimumPrice * rate, _decimalDelimeter);
            var maxPriceRecalculated = Math.Round(range.MaximumPrice * rate, _decimalDelimeter);
            range.MaximumPrice = maxPriceRecalculated;
            range.MinimumPrice = minPriceRecalculated;
            return range;
        }
    }
}