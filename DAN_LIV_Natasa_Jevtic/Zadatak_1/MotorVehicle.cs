using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak_1
{
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

        public MotorVehicle()
        {
            Color = Colors[random.Next(0, Colors.Length)];
        }

        public abstract void Start(object obj);
        public abstract void Stop(object obj);
    }
}
