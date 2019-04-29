using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryFinal
{
    public interface IShippingService
    {
        IShippingLocation ShippingLocation { get; }

        uint ShippingDistance { get; }

        uint NumRefuels { get; }
        IDeliveryService DeliveryService { get; set; }
    }

}