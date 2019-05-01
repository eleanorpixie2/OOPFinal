using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryFinal
{
    public abstract class DeliveryService : IDeliveryService
    {

        protected double costPerRefuel;

        public double CostPerRefuel { get { return costPerRefuel; } set { costPerRefuel = value; } }


        public IShippingVehicle ShippingVehicle { get { return shippingVehicle; } set { shippingVehicle = value; } }


        protected IShippingVehicle shippingVehicle;

        public string NameOfService { get; protected set; }

        public DeliveryService(IShippingVehicle vehicle)
        {
            shippingVehicle = vehicle;
        }

    }

    
}