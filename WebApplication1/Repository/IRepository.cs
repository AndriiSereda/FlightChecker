using System.Collections.Generic;

namespace FlightChecker.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
    }
}