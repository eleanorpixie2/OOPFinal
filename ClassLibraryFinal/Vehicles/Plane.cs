using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryFinal
{
    public class Plane : MotorVehicle
    {
        public Plane()
        {
            MaxDistancePerRefuel = 5000;
            MaxWeight = 1000;
            TopSpeed = 200;
        }
    }
}