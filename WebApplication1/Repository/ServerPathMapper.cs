using System;

namespace FlightChecker.Repository
{
    public class ServerPathMapper : IPathMapper
    {
        public string MapPath(string source)
        {
            var sourcePart = String.Format("~\\App_Data\\{0}.csv", source);
            var relativePath = System.Web.HttpContext.Current.Request.MapPath(sourcePart);
            return relativePath;
        }
    }
}