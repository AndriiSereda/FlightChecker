using System.Collections.Generic;

namespace FlightChecker.BLL
{
    public interface IDataSanitizer<T>
    {
        IEnumerable<T> SanitizeAndSortCollection(IEnumerable<T> data);
    }
}
