using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class CurrencyRateCsvRepository : CsvRepository, IRepository<CurrencyRate>, ICurrencyRateRepository
    {
        private string _toCurrency;

        public CurrencyRateCsvRepository (string source, string toCurrency) : base(source)
        {
            if (toCurrency != "EUR")
            {
                throw new NotImplementedException("Not implemented");
            }
            _toCurrency = toCurrency;

        }

        public IEnumerable<CurrencyRate> GetAll()
        {
            try
            {
                using (TextFieldParser parser = new TextFieldParser(_source))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(";");
                    parser.ReadLine(); //skip meta

                    var result = new List<CurrencyRate>();
                    while (!parser.EndOfData)
                    {
                        //Process row
                        string[] fields = parser.ReadFields();
                        var item = new CurrencyRate(_toCurrency)
                        {                            
                            FromCurrencyCode = fields[0],
                            ExchangeRate = Decimal.Parse(fields[1])
                        };
                        result.Add(item);
                    }
                    return result;
                }                
            }
            catch (Exception)
            {
                throw;
            }
        }

        public CurrencyRate GetRateForCurrency(string currency)
        {
            var allData = GetAll();
            var exchangeRateForCurrency = allData.Where(x => x.FromCurrencyCode == currency).ToList();
            if (exchangeRateForCurrency.Count > 1)
            {
                throw new ArgumentException("Ambiguous query");
            }
            return exchangeRateForCurrency.FirstOrDefault();
        }


    }
}