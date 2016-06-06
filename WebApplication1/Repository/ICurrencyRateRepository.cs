using FlightChecker.Models;

namespace FlightChecker.Repository
{
    interface ICurrencyRateRepository
    {
        CurrencyRate GetRateForCurrency(string currency);
    }
}
