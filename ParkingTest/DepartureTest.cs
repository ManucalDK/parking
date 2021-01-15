using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ParkingAPI.Services;
using ParkingAPI.Services.Interfaces;

namespace ParkingTest
{
    [TestClass]
    public class DepartureTest
    {
        Mock<IDepartureService> _departureService;

        [TestInitialize]
        public void Initialize()
        {
            _departureService = new Mock<IDepartureService>();
        }

        [TestMethod]
        public void TestHourMotoCharge()
        {
        


        }
    }
}
