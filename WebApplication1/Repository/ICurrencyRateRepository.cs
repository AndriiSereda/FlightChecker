using FlightChecker.Models;

namespace FlightChecker.Repository
{
    public interface ICurrencyRateRepository
    {
        CurrencyRate GetRateForCurrency(string currency);
    }
}
