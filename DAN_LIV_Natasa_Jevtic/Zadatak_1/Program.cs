using System;
using System.Collections.Generic;
using System.Threading;

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
            Race race = new Race();
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
            //threads for every car
            for (int i = 0; i < cars.Count; i++)
            {
                int temp = i;
                Thread car = new Thread(() => race.Racing(cars[temp]));
                car.Start();
            }
            //creating orange golf
            Car orangeGolf = new Car
            {
                Color = "orange",
                Manufacturer = "Golf",
                TankVolume = 55
            };
            cars.Add(orangeGolf);
            Console.WriteLine(orangeGolf.RegistrationNumber + " " + orangeGolf.Color + " " + orangeGolf.Manufacturer + " joined the race.");

            //thread for orange golf
            Thread threadForOrangeGolf = new Thread(() => race.Racing(orangeGolf));
            threadForOrangeGolf.Start();
            //thread for reducing the fuel
            Thread reducerOfFuel = new Thread(race.ReduceFuel)
            {
                IsBackground = true
            };
            reducerOfFuel.Start();
            //thread for semaphore
            Thread changeSemaphoreColor = new Thread(race.Semaphore)
            {
                IsBackground = true
            };
            changeSemaphoreColor.Start();            
            //thread for displaying race result
            Thread result = new Thread(Race.PrintResult);
            result.Start();
            Console.ReadLine();
        }
    }
}
