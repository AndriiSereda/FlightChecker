using System;

namespace WebApplication1.Models
{
    public class CurrencyRate
    {
        //test change

        public string FromCurrencyCode { get; set; }
        public decimal ExchangeRate { get; set; }

        private string _toCurrency;
        public string ToCurrencyCode { get; }

        public CurrencyRate(string toCurrency)
        {
            _toCurrency = toCurrency;
        }
    }
}