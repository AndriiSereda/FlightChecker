using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using FlightChecker.Models;
using System.IO;

namespace FlightChecker.Repository
{
    public class CurrencyRateCsvRepository : CsvRepository<CurrencyRate>, ICurrencyRateRepository
    {
        private string _toCurrency;

        public CurrencyRateCsvRepository (string source, string toCurrency, IPathMapper pathMapper) : base(source, pathMapper)
        {
            if (toCurrency != "EUR")
            {
                throw new NotImplementedException("Not implemented");
            }
            _toCurrency = toCurrency;

        }

        public CurrencyRate GetRateForCurrency(string currency)
        {
            try
            {                
                using (var instream = new FileStream(_source, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (TextFieldParser parser = new TextFieldParser(instream))
                    {
                        parser.TextFieldType = FieldType.Delimited;
                        parser.SetDelimiters(_delimiter);
                        //get meta
                        var properties = parser.ReadFields();
                        var result = new List<CurrencyRate>();
                        while (!parser.EndOfData)
                        {
                            //Process row
                            string[] fields = parser.ReadFields();
                            var item = this.GiveMeAnObject(properties, fields);
                            result.Add(item);
                        }
                        return result.FirstOrDefault(x => x.Currency == currency);
                    }
                }                     
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}