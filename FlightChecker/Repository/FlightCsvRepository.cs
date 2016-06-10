using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using FlightChecker.Models;
using System.IO;

namespace FlightChecker.Repository
{
    public class FlightCsvRepository : CsvRepository<Flight>, IFlightPricesRepository

    {
        public FlightCsvRepository(string source, IPathMapper pathMapper) : base(source, pathMapper)
        {
        }

        public IEnumerable<Flight> GetFlightsFromOriginToDestination(string origin, string destination)
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

                        var result = new List<Flight>();
                        while (!parser.EndOfData)
                        {
                            //Process row
                            var fields = parser.ReadFields();
                            var item = this.GiveMeAnObject(properties, fields);
                            if ((item.Origin == origin) && (item.Destination == destination))
                            {
                                result.Add(item);
                            }
                        }
                        return result;
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