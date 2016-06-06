using System.Collections.Generic;

namespace FlightChecker.BLL
{
    interface IDataSanitizer<T>
    {
        IEnumerable<T> SanitizeCollection(IEnumerable<T> data);
    }
}
