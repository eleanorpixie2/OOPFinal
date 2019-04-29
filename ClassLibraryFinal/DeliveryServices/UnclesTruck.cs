using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryFinal
{
    public class UnclesTruck : DeliveryService
    {
        public UnclesTruck(IShippingVehicle vehicle) : base(vehicle)
        {
            costPerRefuel = 200;
        }
    }
}