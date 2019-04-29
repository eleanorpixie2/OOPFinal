using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryFinal
{
    public class Truck : MotorVehicle
    {
        public Truck()
        {
            MaxDistancePerRefuel = 200;
            MaxWeight = 1000;
            TopSpeed = 65;
        }
    }
}