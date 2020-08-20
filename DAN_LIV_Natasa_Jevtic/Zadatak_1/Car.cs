using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak_1
{
    class Car : MotorVehicle
    {
        public string RegistrationNumber { get; set; }
        public int NumberOfDoors { get; set; }
        public int MyProperty { get; set; }
        public int TankVolume { get; set; }
        public string TransmissionType { get; set; }
        public string Manufacturer { get; set; }
        public int TrafficLicenseNumber { get; set; }

        public override void Start(object car)
        {
            Car newCar = (Car)car;
            Console.WriteLine(newCar.RegistrationNumber + " " + newCar.Color + " " + newCar.Manufacturer + " started moving.");
        }
        public override void Stop(object car)
        {
            Car newCar = (Car)car;
            Console.WriteLine(newCar.RegistrationNumber + " " + newCar.Color + " " + newCar.Manufacturer + " stopped moving.");
        }
        public void Repaint(string color, string registrationNumber)
        {
            Color = color;
            RegistrationNumber = registrationNumber;
        }
    }
}
