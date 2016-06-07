using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using FlightChecker.Models;
using FlightChecker.BLL;

namespace FlightChecker.Tests.BLL
{
    [TestClass]
    public class FlightCalculatorTest
    {
        [TestMethod]
        public void CalculatorReturnsResult_WithoutOneWayFlights_1()
        {
            var input = LoadTestDataSet_WithoutOneWayFlights_1();
            
            var dataSanitizer = new FlightDataSanitizer();
            var dataCalculator = new FlightDataCalculator(dataSanitizer);

            var result = dataCalculator.CalculateFlightPriceRange(input);

            Assert.AreEqual(210m, result.MaximumPrice);
            Assert.AreEqual(62.46m, result.MinimumPrice);

        }

        [TestMethod]
        public void CalculatorReturnsResult_WithoutOneWayFlights_2()
        {
            var input = LoadTestDataSet_WithoutOneWayFlights_2();

            var dataSanitizer = new FlightDataSanitizer();
            var dataCalculator = new FlightDataCalculator(dataSanitizer);

            var result = dataCalculator.CalculateFlightPriceRange(input);

            Assert.AreEqual(84.92m, result.MaximumPrice);
            Assert.AreEqual(50.5m, result.MinimumPrice);

        }

        [TestMethod]
        public void CalculatorReturnsResult_WithOneWayFlights_2()
        {
            var input = LoadTestDataSet_WithOneWayFlights_1();

            var dataSanitizer = new FlightDataSanitizer();
            var dataCalculator = new FlightDataCalculator(dataSanitizer);

            var result = dataCalculator.CalculateFlightPriceRange(input);

            Assert.AreEqual(10.06m, result.MinimumPrice);
            Assert.AreEqual(82.92m, result.MaximumPrice);
        }

        private Flight[] LoadTestDataSet_WithoutOneWayFlights_1()
        {
            var input = new Flight[]
            {
                new Flight
                {
                    Inbound = new DateTime(2010, 1, 1),
                    Price = 65.06m
                },
                new Flight
                {
                    //min
                    Inbound = new DateTime(2010, 1, 1),
                    Price = 62.46m
                },                  
                  new Flight
                {
                    Inbound = new DateTime(2010, 1, 1),
                    Price = 72.92m
                },
                   new Flight
                {
                    Inbound = new DateTime(2010, 1, 1),
                    Price = 70.25m
                },
                   new Flight
                {
                    Inbound = new DateTime(2010, 1, 1),
                    Price = 70.5m
                },

                   new Flight
                {
                    Inbound = new DateTime(2010, 1, 1),
                    Price = 70.35m
                },
                  new Flight
                {
                    //outlier
                    Inbound = new DateTime(2010, 1, 1),
                    Price = 310m
                },
                  new Flight
                {
                    //max
                    Inbound = new DateTime(2010, 1, 1),
                    Price = 210m
                }
            };
            return input;
        }

        private IEnumerable<Flight> LoadTestDataSet_WithoutOneWayFlights_2()
        {
            var input = new Flight[]
                       {
                new Flight
                {
                    Inbound = new DateTime(2010, 1, 1),
                    Price = 65.06m
                },
                new Flight
                {
                    Inbound = new DateTime(2010, 1, 1),
                    Price = 73.15m
                },
                new Flight
                {
                    Inbound = new DateTime(2010, 1, 1),
                    Price = 62.46m
                },
                  new Flight
                {
                    //max
                    Inbound = new DateTime(2010, 1, 1),
                    Price = 84.92m
                },
                   new Flight
                {
                    Inbound = new DateTime(2010, 1, 1),
                    Price = 70.25m
                },
                   new Flight
                {
                    Inbound = new DateTime(2010, 1, 1),
                    Price = 65.06m
                },
                new Flight
                {
                    Inbound = new DateTime(2010, 1, 1),
                    Price = 73.15m
                },
                new Flight
                {
                    Inbound = new DateTime(2010, 1, 1),
                    Price = 62.46m
                },
                  new Flight
                {
                    //max
                    Inbound = new DateTime(2010, 1, 1),
                    Price = 84.92m
                },
                   new Flight
                {
                    Inbound = new DateTime(2010, 1, 1),
                    Price = 70.25m
                },
                   new Flight
                {
                    //min
                    Inbound = new DateTime(2010, 1, 1),
                    Price = 50.5m
                },
                   new Flight
                {
                    
                    Inbound = new DateTime(2010, 1, 1),
                    Price = 60.5m
                },

                   new Flight
                {

                    Inbound = new DateTime(2010, 1, 1),
                    Price = 73.5m
                },
                   new Flight
                {

                    Inbound = new DateTime(2010, 1, 1),
                    Price = 74.5m
                },
                    new Flight
                {

                    Inbound = new DateTime(2010, 1, 1),
                    Price = 60.5m
                },

                   new Flight
                {

                    Inbound = new DateTime(2010, 1, 1),
                    Price = 73.5m
                },
                   new Flight
                {

                    Inbound = new DateTime(2010, 1, 1),
                    Price = 74.5m
                },
                   new Flight
                {

                    Inbound = new DateTime(2010, 1, 1),
                    Price = 72.5m
                },

                   new Flight
                {
                    Inbound = new DateTime(2010, 1, 1),
                    Price = 70.35m
                },
                   new Flight
                {
                    Inbound = new DateTime(2010, 1, 1),
                    Price = 73.45m
                },
                  new Flight
                {
                    //outlier
                    Inbound = new DateTime(2010, 1, 1),
                    Price = 230m
                },
                  new Flight
                {
                    //outlier
                    Inbound = new DateTime(2010, 1, 1),
                    Price = 1m
                }
                       };
            return input;
        }

        private IEnumerable<Flight> LoadTestDataSet_WithOneWayFlights_1()
        {
            var input = new Flight[]   {
                new Flight
                {
                    Inbound = null,
                    Price = 10.06m
                },
                 new Flight
                {
                    Inbound = null,
                    Price = 11.06m
                },
                  new Flight
                {
                    Inbound = null,
                    Price = 12.06m
                },
                   new Flight
                {
                    Inbound = null,
                    Price = 11.06m
                },
                new Flight
                {
                    Inbound = null,
                    Price = 80.06m
                },
                new Flight
                {
                    Inbound = new DateTime(2010, 1, 1),
                    Price = 73.15m
                },
                new Flight
                {
                    Inbound = new DateTime(2010, 1, 1),
                    Price = 82.46m
                },
                  new Flight
                {
                    //max
                    Inbound = new DateTime(2010, 1, 1),
                    Price = 82.92m
                },
                   new Flight
                {
                    Inbound = new DateTime(2010, 1, 1),
                    Price = 70.25m
                },
                   new Flight
                {
                    Inbound = new DateTime(2010, 1, 1),
                    Price = 65.06m
                },
                new Flight
                {
                    Inbound = new DateTime(2010, 1, 1),
                    Price = 73.15m
                } };

            return input;

        }

        
    }
}
