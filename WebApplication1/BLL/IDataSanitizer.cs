using System.Collections.Generic;

namespace WebApplication1.BLL
{
    interface IDataSanitizer<T>
    {
        IEnumerable<T> Sanitize(IEnumerable<T> data);
    }
}
