using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryFinal
{
    public interface IDeliveryService
    {
        IShippingVehicle ShippingVehicle { get; }

        double CostPerRefuel { get; }
    }
}