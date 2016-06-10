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
        private IPathMapper _pathMapper;
        private const string _defaultCurrency = "EUR";

        public FlightPriceController()
        {
            _pathMapper = new ServerPathMapper();
            _flightPricesRepository = new FlightCsvRepository("prices", _pathMapper);
            _currencyRateRepository = new CurrencyRateCsvRepository("currencies", _defaultCurrency, _pathMapper);
            _dataSanitizer = new FlightDataSanitizer();
            _priceRangeCalculator = new FlightDataCalculator(_dataSanitizer);
        }

        public FlightPriceController(IPathMapper pathmapper, IFlightPricesRepository flightPricesRepository, ICurrencyRateRepository currencyRateRepository,
                                     IDataSanitizer<Flight> dataSanitizer, IPriceRangeCalculator<Flight> priceRangeCalculator)
        {
            _pathMapper = pathmapper;
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

                var priceRange = (FlightPriceRangeContract)_priceRangeCalculator.CalculatePriceRange(flightsAndPrices);
                return Request.CreateResponse<FlightPriceRangeContract>(HttpStatusCode.OK, priceRange);
            }
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error at processing your request");
            }  
        }

        public HttpResponseMessage Get(string origin, string destination, string currency)
        {
            try
            {
                CurrencyRate currencyRate;
                if (currency == _defaultCurrency)
                {
                    currencyRate = new CurrencyRate { Currency = _defaultCurrency, Rate = 1 };
                }
                else
                {
                    currencyRate = _currencyRateRepository.GetRateForCurrency(currency);
                    if (currencyRate == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There was no matching currency found");
                    }
                }
                

                var flightsAndPrices = _flightPricesRepository.GetFlightsFromOriginToDestination(origin, destination);
                if (!flightsAndPrices.Any())
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "There were no matching flights found");
                }

                var priceRange =  _priceRangeCalculator.CalculatePriceRange(flightsAndPrices);
                var calculatedPriceRange = (FlightPriceRangeContract) _priceRangeCalculator.ConvertPriceRange(priceRange, currencyRate.Rate);

                return Request.CreateResponse<FlightPriceRangeContract>(HttpStatusCode.OK, calculatedPriceRange);
            }
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error at processing your request");
            }

        }

        public string Get()

        { 
            return "Parameters: origin and destination, currency(optional)";
        }
    }
}
