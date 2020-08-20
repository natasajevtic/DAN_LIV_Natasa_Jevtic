using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zadatak_1
{
    class Program
    {
        //creating collections of vehicles
        public static List<Car> cars = new List<Car>();
        public static List<Truck> trucks = new List<Truck>();
        public static List<Tractor> tractors = new List<Tractor>();

        static void Main(string[] args)
        {
            //creating objects and adding to collections
            for (int i = 0; i < 2; i++)
            {
                Car car = new Car();
                cars.Add(car);
                Tractor tractor = new Tractor();
                tractors.Add(tractor);
                Truck truck = new Truck();
                trucks.Add(truck);
            }
            //countdown of 5 seconds
            int seconds = 5;
            for (int i = seconds; i > 0; i--)
            {
                Console.WriteLine(i);
                Thread.Sleep(1000);
            }
        }
    }
}
