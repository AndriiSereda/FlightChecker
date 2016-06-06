using System.Collections.Generic;

namespace FlightChecker.BLL
{
    interface IDataSanitizer<T>
    {
        IEnumerable<T> Sanitize(IEnumerable<T> data);
    }
}
