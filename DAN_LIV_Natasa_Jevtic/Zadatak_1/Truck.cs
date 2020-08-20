using System;

namespace Zadatak_1
{
    class Truck : MotorVehicle
    {
        public double Capacity { get; set; }
        public double Height { get; set; }
        public int NumberOfSeats { get; set; }
        /// <summary>
        /// This method displays message that truck is loading items.
        /// </summary>
        public void Load()
        {
            Console.WriteLine("Truck is loading items.");
        }
        /// <summary>
        /// This method displays message that truck is unloading items.
        /// </summary>
        public void Unload()
        {
            Console.WriteLine("Truck is unloading items.");
        }
        /// <summary>
        /// This method displays message that truck is started moving.
        /// </summary>
        /// <param name="truck">Object of class Truck.</param>
        public override void Start(object truck)
        {
            Console.WriteLine("Truck started moving.");
        }
        /// <summary>
        /// This method displays message that truck is stopped moving.
        /// </summary>
        /// <param name="truck"></param>
        public override void Stop(object truck)
        {
            Console.WriteLine("Truck stopped moving.");
        }
    }
}
