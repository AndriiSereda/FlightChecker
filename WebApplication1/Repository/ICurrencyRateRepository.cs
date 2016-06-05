using WebApplication1.Models;

namespace WebApplication1.Repository
{
    interface ICurrencyRateRepository
    {
        CurrencyRate GetRateForCurrency(string currency);
    }
}
