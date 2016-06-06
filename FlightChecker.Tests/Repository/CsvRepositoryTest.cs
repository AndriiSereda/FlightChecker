using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FlightChecker.Repository;

namespace FlightChecker.Tests.Repository
{
    [TestClass]
    public class CsvRepositoryTest
    {
        [TestMethod]
        public void CheckCsvSupportedTypes()
        {
            var rateRepository = new CurrencyRateCsvRepository("currencies","EUR");
            //supported types
            Assert.AreEqual(true, rateRepository.IsTypeSupportedByRepository(typeof(Decimal)));
            Assert.AreEqual(true, rateRepository.IsTypeSupportedByRepository(typeof(DateTime)));
            Assert.AreEqual(true, rateRepository.IsTypeSupportedByRepository(typeof(DateTime?)));
            Assert.AreEqual(true, rateRepository.IsTypeSupportedByRepository(typeof(String)));
            //unsupported types
            Assert.AreEqual(false, rateRepository.IsTypeSupportedByRepository(typeof(Int32)));
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void MismatchBetweenTypePropertiesAndCsvPropertiesThrowsError()
        {
            var rateRepository = new CurrencyRateCsvRepository("currencies", "EUR");

            var properties = new string[] { "Rate", "Currency" };
            var values = new string[] {"3,234", "DKK"};
            var rate = rateRepository.GiveMeAnObject(properties, values);
            Assert.IsNotNull(rate);
            Assert.AreEqual(rate.Currency, "DKK");
            Assert.AreEqual(rate.Rate, 3.234m);

            properties = new string[] { "Rate", "Currency", "Notmapped" };
            values = new string[] { "3,234", "DKK", "Notmapped" };
            rate = rateRepository.GiveMeAnObject(properties, values);
            //should be caught by exception
            Assert.AreEqual(true, false);
        }

    }
}
