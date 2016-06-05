using System.Collections.Generic;

namespace WebApplication1.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
    }
}