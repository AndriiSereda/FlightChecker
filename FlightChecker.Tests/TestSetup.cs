using FlightChecker.Repository;
using Moq;
using System;

namespace FlightChecker.Tests
{
    public static class TestSetup
    {
        public static IPathMapper GiveMeALocalDummyPathMapper(string source)
        {
            var localPath = String.Format(@"App_Data/{0}.csv", source);
            var mock = new Mock<IPathMapper>();
            mock.Setup(m => m.MapPath(source)).Returns(localPath);
            return mock.Object;
        }
    }
}
