using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlightChecker.Repository;
using FlightChecker.BLL;

namespace FlightChecker.Tests.BLL
{
    [TestClass]
    public class UseCaseTest
    {
        [TestMethod]
        [TestCategory("Integration test")]
        public void IntegrationTestForLONtoBERFlights()
        {
            var _flightPricesRepository = new FlightCsvRepository("prices", TestSetup.GiveMeALocalDummyPathMapper("prices"));
            var _currencyRateRepository = new CurrencyRateCsvRepository("currencies", "EUR", TestSetup.GiveMeALocalDummyPathMapper("currencies"));
            var _dataSanitizer = new FlightDataSanitizer();
            var _priceRangeCalculator = new FlightDataCalculator(_dataSanitizer);

            var flightsAndPrices = _flightPricesRepository.GetFlightsFromOriginToDestination("LON", "BER");
            var priceRange = _priceRangeCalculator.CalculatePriceRange(flightsAndPrices);
            Assert.IsFalse(priceRange.MaximumPrice > 200);
            Assert.IsFalse(priceRange.MinimumPrice < 20);

        }        
    }
}
