using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ClassLibraryFinal;
using Ninject;
using ClassLibraryFinal.NinjectModules;

namespace UnitTestFinal
{
    /// <summary>
    /// Summary description for MockTest
    /// </summary>
    [TestClass]
    public class MockTest
    {
        //moq
        Mock<IShippingService> mockShippingService;
        Mock<IShippingLocation> mockLocation;
        Mock<IDeliveryService> mockDeliveryService;
        Mock<IShippingVehicle> mockShippingVehicle;

        //ninject
        IKernel kernel;

        public MockTest()
        {
            //initalize kernel
            kernel=new StandardKernel(new ShippingServiceModule());
            //intialize mock objects
            mockShippingService = new Mock<IShippingService>();
            mockLocation = new Mock<IShippingLocation>();
            mockDeliveryService = new Mock<IDeliveryService>();
            mockShippingVehicle = new Mock<IShippingVehicle>();

        }
        
        [TestMethod]
        public void TestNinjectShippingAndDeliveryServices()
        {

        }

        [TestMethod]
        public void TestMoqShippingAndDeliveryServices()
        {
            //setup
            uint destZip = 77035;
            uint startZip = 60090;
            uint maxDist = 5000;
            uint maxWeight = 1000;
            uint topSpeed = 200;
            uint refuelCost = 500;

            //act
            //Mock location
            uint distance = (uint)Math.Abs(destZip - startZip);
            uint numRefuels = (uint)distance / (uint)maxDist;

            //create mock vehicle
            mockShippingVehicle.SetupGet(vc => vc.MaxDistancePerRefuel).Returns(maxDist);
            mockShippingVehicle.SetupGet(vc => vc.MaxWeight).Returns(maxWeight);
            mockShippingVehicle.SetupGet(vc => vc.TopSpeed).Returns(topSpeed);

            //create mock delivery service
            mockDeliveryService.SetupGet(ds => ds.CostPerRefuel).Returns(refuelCost);
            mockDeliveryService.SetupGet(ds => ds.ShippingVehicle).Returns(mockShippingVehicle.Object);

            //mock location
            mockLocation.SetupGet(l => l.DestinationZipCode).Returns(destZip);
            mockLocation.SetupGet(l => l.StartZipCode).Returns(startZip);


            //create mock shipping service
            mockShippingService.SetupGet(ss => ss.DeliveryService).Returns(mockDeliveryService.Object);
            mockShippingService.SetupGet(ss => ss.ShippingLocation).Returns(mockLocation.Object);
            mockShippingService.SetupGet(ss => ss.ShippingDistance).Returns((uint)Math.Abs(mockShippingService.Object.ShippingLocation.DestinationZipCode - mockShippingService.Object.ShippingLocation.StartZipCode));
            mockShippingService.SetupGet(ss => ss.NumRefuels).Returns((uint)mockShippingService.Object.ShippingDistance / (uint)mockShippingService.Object.DeliveryService.ShippingVehicle.MaxDistancePerRefuel);

            //Assert
            //delivery service
            Assert.AreEqual(refuelCost, mockDeliveryService.Object.CostPerRefuel);
            Assert.AreEqual(mockShippingVehicle.Object, mockDeliveryService.Object.ShippingVehicle);
            //shipping vehicles
            Assert.AreEqual(maxDist, mockShippingVehicle.Object.MaxDistancePerRefuel);
            Assert.AreEqual(maxWeight, mockShippingVehicle.Object.MaxWeight);
            Assert.AreEqual(topSpeed, mockShippingVehicle.Object.TopSpeed);
            //shipping services
            Assert.AreEqual(mockLocation.Object, mockShippingService.Object.ShippingLocation);
            Assert.AreEqual(mockDeliveryService.Object, mockShippingService.Object.DeliveryService);
            Assert.AreEqual(distance, mockShippingService.Object.ShippingDistance);
            Assert.AreEqual(numRefuels, mockShippingService.Object.NumRefuels);
        }
    }
}
