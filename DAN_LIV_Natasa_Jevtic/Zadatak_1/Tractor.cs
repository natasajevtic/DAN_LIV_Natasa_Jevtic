using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak_1
{
    class Tractor : MotorVehicle
    {
        public double TireSize { get; set; }
        public int Wheelbase { get; set; }
        public string Drive { get; set; }

        public override void Start(object tractor)
        {
            Console.WriteLine("Tractor started moving.");
        }
        public override void Stop(object tractor)
        {
            Console.WriteLine("Tractor stopped moving.");
        }
    }
}
