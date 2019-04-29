using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibraryFinal;

namespace UnitTestFinal
{
    [TestClass]
    public class UnitTestShippingVehicle
    {

        IShippingVehicle sv;

        public UnitTestShippingVehicle()
        {
            
        }

        [TestMethod]
        public void ShippingVehicle_TruckDefaults()
        {
            //Arrange
            sv = new Truck();
            uint MaxDistance, MaxWeight, TopSpeed;
            //Act
            MaxDistance = 200;
            MaxWeight = 1000;
            TopSpeed = 65;
            //Assert
            Assert.AreEqual(MaxDistance, sv.MaxDistancePerRefuel);
            Assert.AreEqual(MaxWeight, sv.MaxWeight);
            Assert.AreEqual(TopSpeed, sv.TopSpeed);
        }

        [TestMethod]
        public void ShippingVehicle_SnailDefaults()
        {
            //Arrange
            sv = new ShippingSnail();
            uint MaxDistance, MaxWeight, TopSpeed;
            //Act
            MaxDistance = 20;
            MaxWeight = 1;
            TopSpeed = 1;
            //Assert
            Assert.AreEqual(MaxDistance, sv.MaxDistancePerRefuel);
            Assert.AreEqual(MaxWeight, sv.MaxWeight);
            Assert.AreEqual(TopSpeed, sv.TopSpeed);
        }

        [TestMethod]
        public void ShippingVehicle_PlaneDefaults()
        {
            //Arrange
            sv = new Plane();
            uint MaxDistance, MaxWeight, TopSpeed;
            //Act
            MaxDistance = 5000;
            MaxWeight = 1000;
            TopSpeed = 200;
            //Assert
            Assert.AreEqual(MaxDistance, sv.MaxDistancePerRefuel);
            Assert.AreEqual(MaxWeight, sv.MaxWeight);
            Assert.AreEqual(TopSpeed, sv.TopSpeed);
        }

       
    }
}
