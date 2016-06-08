using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlightChecker.Repository;
using System.Linq;
using Moq;

namespace FlightChecker.Tests.Repository
{
    [TestClass]
    public class CurrencyRateCsvRepositoryTest
    {
        [TestCategory("Integration tests")]
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void NoRepositoryFoundThrowsException()
        {
            var repository = new CurrencyRateCsvRepository("foo", "EUR", TestSetup.GiveMeALocalDummyPathMapper("currencies"));
        }

        [TestCategory("Integration tests")]
        [TestMethod]
        public void CurrencyRateRepositoryIsFound()
        {
            var repository = new CurrencyRateCsvRepository("currencies", "EUR", TestSetup.GiveMeALocalDummyPathMapper("currencies"));
            //ArgumentException will prevent otherwise
            Assert.IsTrue(true);
        }

        [ExpectedException(typeof(NotImplementedException))]
        [TestCategory("Integration tests")]
        [TestMethod]
        public void CurrencyRateRepositoryForWrongCurrencyNotImplemented()
        {
            var repository = new CurrencyRateCsvRepository("currencies", "DKK", TestSetup.GiveMeALocalDummyPathMapper("currencies"));
            //ArgumentException will prevent otherwise
            Assert.IsTrue(true);
        }

        [TestMethod]
        [TestCategory("Integration tests")]
        public void ReadAllCurrencyRateDataReturnsAllData()
        {
            var repository = new CurrencyRateCsvRepository("currencies", "EUR", TestSetup.GiveMeALocalDummyPathMapper("currencies"));
            var result = repository.GetAll().ToList();
            Assert.AreEqual(result.Count, 10);
        }

        [TestCategory("Integration tests")]
        [TestMethod]
        public void GetRateForCurrencyRateReturnsOneCurrencyRate()
        {
            var repository = new CurrencyRateCsvRepository("currencies", "EUR", TestSetup.GiveMeALocalDummyPathMapper("currencies"));
            var result = repository.GetRateForCurrency("YER");
            Assert.AreEqual(result.Rate, 287.304992675781m);
        }

        [TestCategory("Integration tests")]
        [TestMethod]
        public void GetRateForNonExistingCurrencyReturnsNull()
        {
            var repository = new CurrencyRateCsvRepository("currencies", "EUR", TestSetup.GiveMeALocalDummyPathMapper("currencies"));
            var result = repository.GetRateForCurrency("FFF");
            Assert.IsNull(result);
        }

        

    }
}
