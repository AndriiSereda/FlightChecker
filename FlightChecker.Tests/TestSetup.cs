using FlightChecker.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightChecker.Tests
{
    public static class TestSetup
    {
        public static IPathMapper GiveMeALocalDummyPathMapper(string source)
        {
            var localPath = String.Format(@"Repository/csv/{0}.csv", source);
            var mock = new Mock<IPathMapper>();
            mock.Setup(m => m.MapPath(source)).Returns(localPath);
            return mock.Object;
        }
    }
}
