using FlightChecker.BLL;
using FlightChecker.Contracts;
using FlightChecker.Models;
using FlightChecker.Repository;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FlightChecker.Controllers
{
    public class FlightPriceController : ApiController
    {
        private IFlightPricesRepository _flightPricesRepository;
        private ICurrencyRateRepository _currencyRateRepository;
        private IDataSanitizer<Flight> _dataSanitizer;
        private IPriceRangeCalculator<Flight> _priceRangeCalculator;

        public FlightPriceController()
        {
            _flightPricesRepository = new FlightCsvRepository("prices");
            _currencyRateRepository = new CurrencyRateCsvRepository("currencies", "EUR");
            _dataSanitizer = new FlightDataSanitizer();
            _priceRangeCalculator = new FlightDataCalculator(_dataSanitizer);
        }

        public FlightPriceController(IFlightPricesRepository flightPricesRepository, ICurrencyRateRepository currencyRateRepository,
                                     IDataSanitizer<Flight> dataSanitizer, IPriceRangeCalculator<Flight> priceRangeCalculator)
        {
            _flightPricesRepository = flightPricesRepository;
            _currencyRateRepository = currencyRateRepository;
            _dataSanitizer = dataSanitizer;
            _priceRangeCalculator = priceRangeCalculator;
        }

        public HttpResponseMessage Get(string origin, string destination)
        {
            try
            {
                var flightsAndPrices = _flightPricesRepository.GetFlightsFromOriginToDestination(origin, destination);
                if (!flightsAndPrices.Any())
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There were no matching flights found");
                }

                var priceRange = _priceRangeCalculator.CalculatePriceRange(flightsAndPrices);
                return Request.CreateResponse<IPriceRange>(HttpStatusCode.OK, priceRange);

            }
            catch (Exception)
            {

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error at processing your request");
            }  
        }


       

    }
}
