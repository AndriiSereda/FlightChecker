using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using FlightChecker.Models;
using FlightChecker.BLL;
using FlightChecker.Contracts;

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

            var result = dataCalculator.CalculatePriceRange(input);

            Assert.AreEqual(72.92m, result.MaximumPrice);
            Assert.AreEqual(62.46m, result.MinimumPrice);

        }

        [TestMethod]
        public void CalculatorReturnsResult_WithoutOneWayFlights_2()
        {
            var input = LoadTestDataSet_WithoutOneWayFlights_2();

            var dataSanitizer = new FlightDataSanitizer();
            var dataCalculator = new FlightDataCalculator(dataSanitizer);

            var result = dataCalculator.CalculatePriceRange(input);

            Assert.AreEqual(74.5m, result.MaximumPrice);
            Assert.AreEqual(60.5m, result.MinimumPrice);

        }

        [TestMethod]
        public void CalculatorReturnsResult_WithOneWayFlights_2()
        {
            var input = LoadTestDataSet_WithOneWayFlights_1();

            var dataSanitizer = new FlightDataSanitizer();
            var dataCalculator = new FlightDataCalculator(dataSanitizer);

            var result = dataCalculator.CalculatePriceRange(input);

            Assert.AreEqual(10.26m, result.MinimumPrice);
            Assert.AreEqual(82.92m, result.MaximumPrice);
        }

        [TestMethod]
        public void CalculatorReturnsConvertedResult()
        {
            var input = new FlightPriceRangeContract { MinimumPrice = 1m, MaximumPrice = 2.1m };
            var dataSanitizer = new FlightDataSanitizer();
            var dataCalculator = new FlightDataCalculator(dataSanitizer);
            var result = dataCalculator.ConvertPriceRange(input, 1.2345m);
            Assert.AreEqual(1.23m, result.MinimumPrice);
            Assert.AreEqual(2.59m, result.MaximumPrice);
        }

        private Flight[] LoadTestDataSet_WithoutOneWayFlights_1()
        {
            var input = new Flight[]
            {
                new Flight {Inbound = new DateTime(2010, 1, 1), Price = 65.06m },
                //min
                new Flight {Inbound = new DateTime(2010, 1, 1), Price = 62.46m },  
                //max                
                new Flight {Inbound = new DateTime(2010, 1, 1), Price = 72.92m },
                new Flight {Inbound = new DateTime(2010, 1, 1), Price = 70.25m },
                new Flight {Inbound = new DateTime(2010, 1, 1), Price = 70.5m  },
                new Flight {Inbound = new DateTime(2010, 1, 1), Price = 70.35m },
                //outlier
                new Flight {Inbound = new DateTime(2010, 1, 1), Price = 310m   },
                //outlier 2nd gen
                new Flight {Inbound = new DateTime(2010, 1, 1), Price = 210m   }
            };
            return input;
        }

        private IEnumerable<Flight> LoadTestDataSet_WithoutOneWayFlights_2()
        {
            var input = new Flight[]
            {
                new Flight { Inbound = new DateTime(2010, 1, 1), Price = 65.06m },
                new Flight { Inbound = new DateTime(2010, 1, 1), Price = 73.15m },
                new Flight { Inbound = new DateTime(2010, 1, 1), Price = 62.46m },
                new Flight { Inbound = new DateTime(2010, 1, 1), Price = 84.92m },
                new Flight { Inbound = new DateTime(2010, 1, 1), Price = 70.25m },
                new Flight { Inbound = new DateTime(2010, 1, 1), Price = 65.06m },
                new Flight { Inbound = new DateTime(2010, 1, 1), Price = 73.15m },
                new Flight { Inbound = new DateTime(2010, 1, 1), Price = 62.46m },
                //outlier
                new Flight { Inbound = new DateTime(2010, 1, 1), Price = 84.92m },
                new Flight { Inbound = new DateTime(2010, 1, 1), Price = 70.25m },
                //outlier
                new Flight {Inbound = new DateTime(2010, 1, 1), Price = 50.5m },
                new Flight {Inbound = new DateTime(2010, 1, 1), Price = 60.5m },
                new Flight {Inbound = new DateTime(2010, 1, 1), Price = 73.5m },
                //max
                new Flight {Inbound = new DateTime(2010, 1, 1), Price = 74.5m },
                //min
                new Flight {Inbound = new DateTime(2010, 1, 1), Price = 60.5m },
                new Flight {Inbound = new DateTime(2010, 1, 1), Price = 73.5m },
                new Flight {Inbound = new DateTime(2010, 1, 1), Price = 74.5m },
                new Flight {Inbound = new DateTime(2010, 1, 1), Price = 72.5m },
                new Flight {Inbound = new DateTime(2010, 1, 1), Price = 70.35m },
                new Flight{Inbound = new DateTime(2010, 1, 1),Price = 73.45m },
                //outlier
                new Flight{Inbound = new DateTime(2010, 1, 1),Price = 230m },
                //outlier
                new Flight{Inbound = new DateTime(2010, 1, 1),Price = 1m }
            };
            return input;
        }

        private IEnumerable<Flight> LoadTestDataSet_WithOneWayFlights_1()
        {
            var input = new Flight[]   {
                //min
                new Flight {Inbound = null, Price = 10.26m},
                new Flight {Inbound = null, Price = 11.06m},
                new Flight {Inbound = null, Price = 12.06m},
                new Flight {Inbound = null, Price = 11.06m},
                new Flight {Inbound = null, Price = 80.06m},
                new Flight {Inbound = new DateTime(2010, 1, 1), Price = 73.15m},
                new Flight {Inbound = new DateTime(2010, 1, 1), Price = 82.46m},
                //max
                new Flight {Inbound = new DateTime(2010, 1, 1),Price = 82.92m},
                new Flight {Inbound = new DateTime(2010, 1, 1),Price = 70.25m},
                new Flight { Inbound = new DateTime(2010, 1, 1),Price = 65.06m},
                new Flight {Inbound = new DateTime(2010, 1, 1), Price = 73.15m}
            };

            return input;

        }

        
    }
}
