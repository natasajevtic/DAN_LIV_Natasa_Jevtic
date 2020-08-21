using System;

namespace Zadatak_1
{
    /// <summary>
    /// Class derived from class MotorVehicle.
    /// </summary>
    class Tractor : MotorVehicle
    {
        public double TireSize { get; set; }
        public int Wheelbase { get; set; }
        public string Drive { get; set; }
        /// <summary>
        /// Parameterless constructor.
        /// </summary>
        public Tractor() : base ()
        {
            TireSize = random.NextDouble() * 100;
            Wheelbase = random.Next() * 1000;
        }
        /// <summary>
        /// This method displays message that tractor is started moving.
        /// </summary>
        /// <param name="tractor">Object of class Tractor.</param>
        public override void Start(object tractor)
        {
            Console.WriteLine("Tractor started moving.");
        }
        /// <summary>
        /// This method displays message that tractor is stopped moving.
        /// </summary>
        /// <param name="tractor">Object of class Tractor.</param>
        public override void Stop(object tractor)
        {
            Console.WriteLine("Tractor stopped moving.");
        }
    }
}
