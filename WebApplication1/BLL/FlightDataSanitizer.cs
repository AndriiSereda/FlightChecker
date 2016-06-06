using System;
using System.Collections.Generic;
using System.Linq;
using FlightChecker.Models;

namespace FlightChecker.BLL
{
    public class FlightDataSanitizer : IDataSanitizer<Flight>
    {
        //lazy dictionary for precalculated coefficients
        private readonly Dictionary<int, decimal> _precalculatedThomsonTau; 

        public FlightDataSanitizer()
        {
            _precalculatedThomsonTau = LoadThompsonTauCoefficients();
        }

        public IEnumerable<Flight> SanitizeCollection(IEnumerable<Flight> dataCollection)
        {
            if (dataCollection == null || !dataCollection.Any())
            {
                throw new ArgumentException("Empty data set can't be sanitized");
            }

            var sortedList = dataCollection.ToList();
            sortedList.RemoveAll(x => x.Price <= 0);
            sortedList.Sort();                  
                     
            var total = sortedList.Count();       
          
            decimal average = sortedList.Average(x=> x.Price);
            decimal sumOfSquaresOfDifferences = sortedList.Select(x => (x.Price - average) * (x.Price - average)).Sum();
            double sd = Math.Sqrt((double)sumOfSquaresOfDifferences / total);
            decimal tau = GiveMeAThompsonTauCoefficient(total);


            //what if discounts?
            //sortedList.RemoveAll(x => x.Price > extremeOutlierMaxBoundary || x.Price < extremeOutlierMinBoundary);
            sortedList.RemoveAll(x => Math.Abs(x.Price - average) > tau * (decimal)sd);
            return sortedList;
        }     
        

        #region Lazy Modified Thompson Tau
        private Dictionary<int,decimal> LoadThompsonTauCoefficients()
        {
            var precalculatedThomsonTau = new Dictionary<int, decimal>();
            precalculatedThomsonTau.Add(3, 1.1511m);
            precalculatedThomsonTau.Add(4, 1.4250m);
            precalculatedThomsonTau.Add(5, 1.5712m);
            precalculatedThomsonTau.Add(6, 1.6563m);
            precalculatedThomsonTau.Add(7, 1.7110m);
            precalculatedThomsonTau.Add(8, 1.7491m);
            precalculatedThomsonTau.Add(9, 1.7770m);
            precalculatedThomsonTau.Add(10, 1.7984m);
            precalculatedThomsonTau.Add(100, 1.9459m);
            precalculatedThomsonTau.Add(1000, 1.9586m);
            return precalculatedThomsonTau;
        }

        private decimal GiveMeAThompsonTauCoefficient(int number)
        {
            if (number <= 0)
            {
                throw new ArgumentException("Number of data for Thompson Tau may not be negative");
            }


            if (number >= 1000)
            {
                return _precalculatedThomsonTau[1000];
            }

            if (number >= 100)
            {
                return _precalculatedThomsonTau[100];
            }

            if (number >= 10)
            {
                return _precalculatedThomsonTau[10];
            }

            if (number >= 3)
            {
                return _precalculatedThomsonTau[number];
            }

            return 1;
        }
        #endregion

    }
}