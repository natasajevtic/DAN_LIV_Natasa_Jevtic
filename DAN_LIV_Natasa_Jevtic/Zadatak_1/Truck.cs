using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak_1
{
    class Truck : MotorVehicle
    {
        public double Capacity { get; set; }
        public double Height { get; set; }
        public int NumberOfSeats { get; set; }

        public void Load()
        {
            Console.WriteLine("Truck is loading items.");
        }
        public void Unload()
        {
            Console.WriteLine("Truck is unloading items.");
        }

        public override void Start(object truck)
        {
            Console.WriteLine("Truck started moving.");
        }
        public override void Stop(object truck)
        {
            Console.WriteLine("Truck stopped moving.");
        }
    }
}
