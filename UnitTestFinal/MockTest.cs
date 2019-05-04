using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ClassLibraryFinal;
using Ninject;
using ClassLibraryFinal.NinjectModules;
using Ninject.Parameters;

namespace UnitTestFinal
{
    /// <summary>
    /// Summary description for MockTest
    /// </summary>
    [TestClass]
    public class MockTest
    {
       
       

        //ninject
        IKernel kernel;

        public MockTest()
        {
            //initalize kernel
            kernel=new StandardKernel(new ShippingServiceModule());
          

        }
        
        [TestMethod]
        public void TestNinjectShippingAndDeliveryServices()
        {
            //Arrange
            IShippingVehicle vehicle;
            IShippingService service;
            IDeliveryService delivery;
            IShippingLocation location;
            uint destZip = 77035;
            uint startZip = 60090;
            uint distance;
            uint numRefuels;

            //Act
            location = new ShippingLocation(startZip, destZip);
            vehicle = kernel.Get<Truck>();
            delivery = kernel.Get<UnclesTruck>();
            service = kernel.Get<DefaultShippingService>(new ConstructorArgument("Service",delivery), 
                new ConstructorArgument("Products", new List<IProduct>()), 
                new ConstructorArgument("Location", location));

            distance = (uint)Math.Abs(destZip - startZip);
            numRefuels = (uint)distance / vehicle.MaxDistancePerRefuel;

            //Assert
            Assert.IsInstanceOfType(vehicle,typeof(IShippingVehicle));
            Assert.IsInstanceOfType(vehicle,typeof(Truck));
            Assert.IsInstanceOfType(vehicle,typeof(MotorVehicle));

            Assert.IsInstanceOfType(delivery, typeof(IDeliveryService));
            Assert.IsInstanceOfType(delivery, typeof(UnclesTruck));
            Assert.IsInstanceOfType(delivery, typeof(DeliveryService));

            Assert.IsInstanceOfType(service, typeof(IShippingService));
            Assert.IsInstanceOfType(service, typeof(DefaultShippingService));
            Assert.AreEqual(distance, service.ShippingDistance);
            Assert.AreEqual(numRefuels, service.NumRefuels);

        }

        [TestMethod]
        public void TestMoqVehicle()
        {
            //setup
            uint maxDist = 5000;
            uint maxWeight = 1000;
            uint topSpeed = 200;
            Mock<IShippingVehicle> mockShippingVehicle = new Mock<IShippingVehicle>();

            //act

            //create mock vehicle
            mockShippingVehicle.SetupGet(vc => vc.MaxDistancePerRefuel).Returns(5000);
            mockShippingVehicle.SetupGet(vc => vc.MaxWeight).Returns(1000);
            mockShippingVehicle.SetupGet(vc => vc.TopSpeed).Returns(200);

            //Assert
            //shipping vehicles
            Assert.AreEqual(maxDist, mockShippingVehicle.Object.MaxDistancePerRefuel);
            Assert.AreEqual(maxWeight, mockShippingVehicle.Object.MaxWeight);
            Assert.AreEqual(topSpeed, mockShippingVehicle.Object.TopSpeed);

        }

        [TestMethod]
        public void TestMoqDeliveryServices()
        {
            //setup
            uint refuelCost = 500;
            string nameOfService = "Spaceship";
            Mock<IShippingVehicle> mockSpaceShip = new Mock<IShippingVehicle>();
            Mock<IDeliveryService> mockDeliveryService = new Mock<IDeliveryService>();

            //act

            //create mock vehicle
            mockSpaceShip.SetupGet(vc => vc.MaxDistancePerRefuel).Returns(10000);
            mockSpaceShip.SetupGet(vc => vc.MaxWeight).Returns(20000);
            mockSpaceShip.SetupGet(vc => vc.TopSpeed).Returns(5000);

            //create mock delivery service
            mockDeliveryService.SetupGet(ds => ds.NameOfService).Returns("Spaceship");
            mockDeliveryService.SetupGet(ds => ds.CostPerRefuel).Returns(500);
            mockDeliveryService.SetupGet(ds => ds.ShippingVehicle).Returns(mockSpaceShip.Object);

            //Assert
            //delivery service
            Assert.AreEqual(refuelCost, mockDeliveryService.Object.CostPerRefuel);
            Assert.AreEqual(nameOfService, mockDeliveryService.Object.NameOfService);
            Assert.AreEqual(mockSpaceShip.Object, mockDeliveryService.Object.ShippingVehicle);

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
            //moq
            Mock<IShippingService> mockShippingService = new Mock<IShippingService>();
            Mock<IShippingLocation> mockLocation = new Mock<IShippingLocation>();
            Mock<IDeliveryService> mockDeliveryService = new Mock<IDeliveryService>();
            Mock<IShippingVehicle> mockShippingVehicle = new Mock<IShippingVehicle>();

            //act

            //get values to test against
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
            mockLocation.SetupGet(l => l.DestinationZipCode).Returns(77035);
            mockLocation.SetupGet(l => l.StartZipCode).Returns(60090);


            //create mock shipping service
            mockShippingService.SetupGet(ss => ss.DeliveryService).Returns(mockDeliveryService.Object);
            mockShippingService.SetupGet(ss => ss.ShippingLocation).Returns(mockLocation.Object);
            mockShippingService.SetupGet(ss => ss.ShippingDistance).Returns((uint)Math.Abs(mockShippingService.Object.ShippingLocation.DestinationZipCode - mockShippingService.Object.ShippingLocation.StartZipCode));
            mockShippingService.SetupGet(ss => ss.NumRefuels).Returns((uint)mockShippingService.Object.ShippingDistance / (uint)mockShippingService.Object.DeliveryService.ShippingVehicle.MaxDistancePerRefuel);

            //Assert
            //shipping services
            Assert.AreEqual(mockLocation.Object, mockShippingService.Object.ShippingLocation);
            Assert.AreEqual(mockDeliveryService.Object, mockShippingService.Object.DeliveryService);
            Assert.AreEqual(distance, mockShippingService.Object.ShippingDistance);
            Assert.AreEqual(numRefuels, mockShippingService.Object.NumRefuels);
        }
    }
}
