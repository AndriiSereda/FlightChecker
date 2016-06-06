﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlightChecker.Models;
using FlightChecker.BLL;
using System.Linq;

namespace FlightChecker.Tests.BLL
{
    [TestClass]
    public class DataSanitizerTest
    {
        [TestMethod]
        public void SanitizeReturnsFlightsWithoutTheOrNegativePrices()
        {
            var input = new Flight[]
            {
                new Flight
                {
                    Price = 1
                },
                new Flight
                {
                    Price = 1
                },
                  new Flight
                {
                    Price = 0
                }                  ,
                  new Flight
                {
                    Price = -1
                }
            };

            var dataSanitizer = new FlightDataSanitizer();
            var result = dataSanitizer.SanitizeCollection(input);
            
            Assert.AreEqual(result.ToList().Count, 2);

        }

        [TestMethod]
        public void SanitizeReturnsFlightsSorted()
        {
            var input = new Flight[]
            {
                new Flight
                {
                    Price = 1
                },
                new Flight
                {
                    Price = 1
                },
                  new Flight
                {
                    Price = 0
                },
                  new Flight
                {
                    Price = 1.54m
                },
                  new Flight
                {
                    Price = 1.34m
                },
                   new Flight
                {
                    Price = 0.97m
                },
                  new Flight
                {
                    Price = 1.01m
                }
            };

            var dataSanitizer = new FlightDataSanitizer();
            var result = dataSanitizer.SanitizeCollection(input);

            Assert.AreEqual(0.97m, result.First().Price);
            Assert.AreEqual(1.34m, result.Last().Price);

        }
        [TestMethod]
        public void SanitizeRemovesOutliers()
        {
            var input = LoadTestWithMaximumOutlier();
            var initialSize = input.Length;
            var dataSanitizer = new FlightDataSanitizer();
            var result = dataSanitizer.SanitizeCollection(input);
            Assert.AreEqual(initialSize - 1, result.Count());

            input = LoadTestWithMinimumOutlier();
            initialSize = input.Length;
            result = dataSanitizer.SanitizeCollection(input);
            Assert.AreEqual(initialSize - 1, result.Count());

        }

        private Flight[] LoadTestWithMaximumOutlier()
        {
            var input = new Flight[]
            {
                new Flight
                {
                    Price = 65.06m
                },
                new Flight
                {
                    Price = 62.46m
                },                  
                  new Flight
                {
                    Price = 72.92m
                },
                   new Flight
                {
                    Price = 70.25m
                },
                   new Flight
                {
                    Price = 70.5m
                },

                   new Flight
                {
                    Price = 70.35m
                },
                  new Flight
                {
                    Price = 310m
                }
            };
            return input;
        }

        private Flight[] LoadTestWithMinimumOutlier()
        {
            var input = new Flight[]
            {
                new Flight
                {
                    Price = 65.06m
                },
                new Flight
                {
                    Price = 62.46m
                },
                new Flight
                {
                    Price = 72.92m
                },
                   new Flight
                {
                    Price = 70.25m
                },
                    new Flight
                {
                    Price = 70.5m
                },
                    new Flight
                {
                    Price = 70.2m
                },
                  new Flight
                {
                    Price = 1m
                }
            };
            return input;
        }
    }
}
