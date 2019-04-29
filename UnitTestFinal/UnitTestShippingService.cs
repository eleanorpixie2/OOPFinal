using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibraryFinal;

namespace UnitTestFinal
{
    [TestClass]
    public class UnitTestShippingService
    {

        IShippingService ss;
        

        public UnitTestShippingService()
        {
            ss = (IShippingService)new TestShippingServiceProvider().GetService(typeof(IShippingService));
        }

        [TestMethod]
        public void ShippingServive_DefaultFromServiceInjection()
        {
            //Arrange
            IDeliveryService defaultService;
            IShippingVehicle defaultVehicle;

            //Act
            defaultService = ss.DeliveryService;
            defaultVehicle = ss.DeliveryService.ShippingVehicle;

            //Assert
            Assert.IsInstanceOfType(defaultService, typeof(UnclesTruck));
            Assert.IsInstanceOfType(defaultVehicle, typeof(Truck));
        }

        [TestMethod]
        public void ShippingServive_Properties()
        {
            //Arrange
            uint numRefuels, shippingDistance, startZip, destZip;

            //Act
            //numRefuels = ss.NumRefuels;
            //Default injection startZip 60605 destZip 60805
            numRefuels = 1;
            shippingDistance = 200;
            startZip = 60605;
            destZip = 60805;

            //Assert
            //Assert.AreEqual(numRefuels, 1);
            Assert.AreEqual(ss.ShippingLocation.StartZipCode, startZip);
            Assert.AreEqual(ss.ShippingLocation.DestinationZipCode, destZip);
            Assert.AreEqual(ss.ShippingDistance, shippingDistance);
            Assert.AreEqual(ss.NumRefuels, numRefuels);
        }

        
    }
}
