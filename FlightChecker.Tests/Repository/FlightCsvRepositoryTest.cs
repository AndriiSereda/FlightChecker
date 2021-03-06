﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlightChecker.Repository;
using System.Linq;

namespace FlightChecker.Tests.Repository
{
    [TestClass]
    public class FlightCsvRepositoryTest
    {
       
        [TestMethod]
        public void FlightRepositoryIsFound()
        {
            var repository = new FlightCsvRepository("prices", TestSetup.GiveMeALocalDummyPathMapper("prices"));
            //ArgumentException will prevent otherwise
            Assert.IsTrue(true);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void NoRepositoryFoundThrowsException()
        {
            var repository = new FlightCsvRepository("foo", TestSetup.GiveMeALocalDummyPathMapper("foo"));
        }


        [TestMethod]
        public void FlightRepositoryReturnsProperAmountOfFlights()
        {
            int lon2berAmountOfFlights = 2645;
            var repository = new FlightCsvRepository("prices", TestSetup.GiveMeALocalDummyPathMapper("prices"));
            var lon2berFlights = repository.GetFlightsFromOriginToDestination("LON", "BER");
            Assert.AreEqual(lon2berAmountOfFlights, lon2berFlights.Count());
        }

       

    }   
}
