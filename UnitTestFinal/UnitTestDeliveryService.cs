using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibraryFinal;

namespace UnitTestFinal
{
    [TestClass]
    public class UnitTestDeliveryService
    {
        IShippingVehicle vehicle;
        IDeliveryService delivery;

        public UnitTestDeliveryService()
        {
            
        }

        /// <summary>
        /// Tests defaults for Class
        /// </summary>
        [TestMethod]
        public void DeliveryService_UnclesTruckDefaults()
        {
            //Arrange
            double costPerRefuel = 200;
            vehicle = new Truck();
            delivery = new UnclesTruck(vehicle);
            //Act

            //Assert
            Assert.IsNotNull(delivery);
            Assert.IsNotNull(delivery.ShippingVehicle);
            Assert.AreEqual(delivery.CostPerRefuel, costPerRefuel);
        }

        [TestMethod]
        public void DeliveryService_AirExpressDefaults()
        {
            //Arrange
            double costPerRefuel = 2000;
            vehicle = new Plane();
            delivery = new AirExpress(vehicle);
            //Act

            //Assert
            Assert.IsNotNull(delivery);
            Assert.IsNotNull(delivery.ShippingVehicle);
            Assert.AreEqual(delivery.CostPerRefuel, costPerRefuel);
        }

        [TestMethod]
        public void DeliveryService_SnailServiceDefaults()
        {
            //Arrange
            double costPerRefuel = 2;
            vehicle = new ShippingSnail();
            delivery = new SnailService(vehicle);
            //Act

            //Assert
            Assert.IsNotNull(delivery);
            Assert.IsNotNull(delivery.ShippingVehicle);
            Assert.AreEqual(delivery.CostPerRefuel, costPerRefuel);
        }
    }
}
