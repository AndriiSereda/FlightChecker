using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlightChecker.Models;

namespace FlightChecker.Repository
{
    public class FlightCsvRepository : CsvRepository<Flight>, IRepository<Flight>, IFlightRepository

    {
        public FlightCsvRepository(string source) : base(source)
        { }

        public IEnumerable<Flight> GetFlightsFromOriginToDestination(string origin, string destination)
        {
            try
            {
                using (TextFieldParser parser = new TextFieldParser(_source))
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
                        if ((item.Origin == origin)&&(item.Destination == destination))
                        {
                            result.Add(item);
                        }
                    }
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        IEnumerable<Flight> IRepository<Flight>.GetAll()
        {
            throw new NotImplementedException();
        }

       
    }
}