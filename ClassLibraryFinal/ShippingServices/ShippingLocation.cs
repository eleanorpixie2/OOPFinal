using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryFinal
{
    public class ShippingLocation : IShippingLocation
    {
        public uint StartZipCode { get {return startZip; } }
        protected uint startZip;

        public uint DestinationZipCode { get { return destZip; } }
        protected uint destZip;

        public ShippingLocation(uint start,uint end)
        {
            startZip = start;
            destZip = end;
        }
    }
}
