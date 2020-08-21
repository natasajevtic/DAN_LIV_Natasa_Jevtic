using System;

namespace Zadatak_1
{
    /// <summary>
    /// Class derived from MotorVehicle class.
    /// </summary>
    class Car : MotorVehicle
    {
        public string RegistrationNumber { get; set; }
        public int NumberOfDoors { get; set; }
        public int TankVolume { get; set; }
        public string TransmissionType { get; set; }
        public string Manufacturer { get; set; }
        public int TrafficLicenseNumber { get; set; }
        public string[] Manufacturers { get; set; } = { "Ford", "Nissan", "Honda", "BMW" };
        public int[] Doors { get; set; } = { 3, 5 };
        /// <summary>
        /// Parameterless constructor.
        /// </summary>
        /// <remarks>Sets tank volume, manufacturere and registration number of car.</remarks>
        public Car() : base()
        {
            Manufacturer = Manufacturers[random.Next(0, Manufacturers.Length)];
            TankVolume = 55;
            RegistrationNumber = random.Next(1000, 2000).ToString();
            NumberOfDoors = Doors[random.Next(0, Doors.Length)];
            TrafficLicenseNumber = random.Next(1000, 5000);
        }
        /// <summary>
        /// This method displays message that car started moving.
        /// </summary>
        /// <param name="car">Object of class Car.</param>
        public override void Start(object car)
        {
            Car newCar = (Car)car;
            Console.WriteLine(newCar.RegistrationNumber + " " + newCar.Color + " " + newCar.Manufacturer + " started moving.");
        }
        /// <summary>
        /// This method displays message that car stopped moving.
        /// </summary>
        /// <param name="car">Object of class Car.</param>
        public override void Stop(object car)
        {
            Car newCar = (Car)car;
            Console.WriteLine(newCar.RegistrationNumber + " " + newCar.Color + " " + newCar.Manufacturer + " stopped moving.");
        }
        /// <summary>
        /// This method sets color and registration number of color.
        /// </summary>
        /// <param name="color">Color of car.</param>
        /// <param name="registrationNumber">Registration number of car.</param>
        public void Repaint(string color, string registrationNumber)
        {
            Color = color;
            RegistrationNumber = registrationNumber;
        }
    }
}
