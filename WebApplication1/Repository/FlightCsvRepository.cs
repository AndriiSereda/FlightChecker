using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class FlightCsvRepository : CsvRepository, IRepository<Flight>, IFlightRepository

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
                    parser.SetDelimiters(";");
                    parser.ReadLine(); //skip meta

                    var result = new List<Flight>();
                    while (!parser.EndOfData)
                    {
                        //Process row
                        string[] fields = parser.ReadFields();
                        DateTime parsedDate;
                        var item = new Flight
                        {
                            Origin = fields[0],
                            Destination = fields[1],
                            OutboundDate = DateTime.Parse(fields[2]),
                            InboundDate = DateTime.TryParse(fields[3], out parsedDate) ? parsedDate : (DateTime?)null,
                            Price = Decimal.Parse(fields[4])
                        };

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