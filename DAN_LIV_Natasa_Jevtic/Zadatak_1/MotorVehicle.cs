using System;

namespace Zadatak_1
{
    /// <summary>
    /// Abstract class MotorVehicle.
    /// </summary>
    abstract class MotorVehicle
    {
        public double EngineDisplacement { get; set; }
        public int Weight { get; set; }
        public string Category { get; set; }
        public string EngineType { get; set; }
        public string Color { get; set; }
        public int EngineNumber { get; set; }
        public string[] Colors { get; set; } = { "blue", "red", "black", "white", "grey" };
        public static Random random = new Random();
        public string[] MotorTypes { get; set; } = { "SRM", "PMSM", "DC" };
        /// <summary>
        /// Parameterless constructor.
        /// </summary>
        /// <remarks>Sets color of vehicle to random color.</remarks>
        public MotorVehicle()
        {
            Color = Colors[random.Next(0, Colors.Length)];
            EngineType = MotorTypes[random.Next(0, MotorTypes.Length)];
            Weight = random.Next(1000, 5000);
        }

        public abstract void Start(object obj);
        public abstract void Stop(object obj);
    }
}
