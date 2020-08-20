using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;

namespace Zadatak_1
{
    /// <summary>
    /// This class contains members for simulation car racing.
    /// </summary>
    class Race
    {
        public static EventWaitHandle waitingAllCars = new ManualResetEvent(false);
        public static Random random = new Random();
        public static bool isRaceOver = false;
        public static string semaphoreColor;
        public static string[] colorsOnSemaphore = { "Red", "Green" };
        public static int counterOfCarPassedSemaphore;
        public static SemaphoreSlim fillFuel = new SemaphoreSlim(1, 1);
        public static Barrier barrier = new Barrier(3);
        public static ConcurrentQueue<Car> carsFinishedRace = new ConcurrentQueue<Car>();
        public static ConcurrentStack<Car> carsOutOfFuel = new ConcurrentStack<Car>();
        public static CountdownEvent finishedRace = new CountdownEvent(3);
        /// <summary>
        /// This method reduces tank volume of car for every 1 second.
        /// </summary>
        public void ReduceFuel()
        {
            //waiting all cars to start the race
            waitingAllCars.WaitOne();
            //while the race is not over, reduce tank volume
            while (isRaceOver == false)
            {
                for (int i = 0; i < Program.cars.Count; i++)
                {
                    Program.cars[i].TankVolume -= random.Next(5, 10);
                    Thread.Sleep(1000);
                }
            }
        }
        /// <summary>
        /// This method changes traffic light for every 2 seconds.
        /// </summary>
        public void Semaphore()
        {
            //waiting all cars to start the race
            waitingAllCars.WaitOne();
            Console.WriteLine("Semaphore is active.");
            //while race is not over, change the traffic light
            while (isRaceOver == false)
            {
                if (counterOfCarPassedSemaphore != 3)
                {
                    semaphoreColor = colorsOnSemaphore[1];
                    Console.WriteLine(semaphoreColor + " light.");
                    Thread.Sleep(2000);
                }
                if (counterOfCarPassedSemaphore != 3)
                {
                    semaphoreColor = colorsOnSemaphore[0];
                    Console.WriteLine(semaphoreColor + " light.");
                    Thread.Sleep(2000);
                }
                if (counterOfCarPassedSemaphore == 3)
                {
                    Console.WriteLine("All cars passed the semaphore.");
                    return;
                }
            }
        }
        /// <summary>
        /// This method fills the tank volume of car, one by one.
        /// </summary>
        /// <param name="car">Car whose tank will be filled.</param>
        public void FillFuel(Car car)
        {
            //until one car fills the tank the others wait
            fillFuel.Wait();
            Console.WriteLine(car.RegistrationNumber + " " + car.Color + " " + car.Manufacturer + " is filling the tank. Tank volume: " + car.TankVolume);
            car.TankVolume = 55;
            Console.WriteLine(car.RegistrationNumber + " " + car.Color + " " + car.Manufacturer + " filled the tank. Tank volume: " + car.TankVolume);
            fillFuel.Release();
        }
        /// <summary>
        /// This method displays on the console cars rank.
        /// </summary>
        public static void PrintResult()
        {
            //waiting all car the finish the race
            finishedRace.Wait();
            isRaceOver = true;
            Console.WriteLine("Race result:");
            //display cars that finished race in order fifo
            if (carsFinishedRace.Count > 0)
            {
                for (int i = 0; i < carsFinishedRace.Count; i++)
                {
                    Console.WriteLine("{0}. {1} {2} {3}", i + 1, carsFinishedRace.ElementAt(i).RegistrationNumber, carsFinishedRace.ElementAt(i).Color, carsFinishedRace.ElementAt(i).Manufacturer);
                }
            }
            //display cars that ran out of fuel in order lifo
            if (carsOutOfFuel.Count > 0)
            {
                for (int i = 0; i < carsOutOfFuel.Count; i++)
                {
                    Console.WriteLine("{0}. {1} {2} {3}", carsFinishedRace.Count + i + 1, carsOutOfFuel.ElementAt(i).RegistrationNumber, carsOutOfFuel.ElementAt(i).Color, carsOutOfFuel.ElementAt(i).Manufacturer);
                }
            }
            //finding first red car
            Car firstRedCar = carsFinishedRace.Where(x => x.Color == "red").FirstOrDefault();
            if (firstRedCar != null)
            {
                Console.WriteLine("Fastest red car is: {0} {1} {2}.", firstRedCar.RegistrationNumber, firstRedCar.Color, firstRedCar.Manufacturer);
            }
            else
            {
                Console.WriteLine("There is no fastest red car.");
            }
        }
        /// <summary>
        /// This method simulates car racing.
        /// </summary>
        /// <param name="car">Car participating in a race.</param>
        public void Racing(Car car)
        {
            //waiting all 3 cars
            barrier.SignalAndWait();
            //starting race
            car.Start(car);
            //signal background threads that race is started
            barrier.SignalAndWait();
            waitingAllCars.Set();
            Thread.Sleep(10000);
            Console.WriteLine(car.RegistrationNumber + " " + car.Color + " " + car.Manufacturer + " reached a traffic light. Currently is " + semaphoreColor + " light.");
            while (semaphoreColor == "Red")
            {
                Thread.Sleep(0);
            }
            //increment count of cars that passed semaphore
            Interlocked.Increment(ref counterOfCarPassedSemaphore);
            Thread.Sleep(3000);
            //checking tank volume, if is less then 15 ivoke method for filling tank volume

            if (car.TankVolume < 15)
            {
                FillFuel(car);
            }
            else
            {
                Console.WriteLine(car.RegistrationNumber + " " + car.Color + " " + car.Manufacturer + " passed the gas station. Tank volume: " + car.TankVolume);
            }

            Thread.Sleep(7000);
            //add to collection of cars that ran out the fuel
            if (car.TankVolume <= 0)
            {
                car.Stop(car);
                Console.WriteLine(car.RegistrationNumber + " " + car.Color + " " + car.Manufacturer + " is out of the race because he ran out of fuel.");
                carsOutOfFuel.Push(car);
            }
            //add to collection of cars that finished the race
            else if (car.TankVolume > 0)
            {
                Console.WriteLine(car.RegistrationNumber + " " + car.Color + " " + car.Manufacturer + " crossed the finishing line.");
                carsFinishedRace.Enqueue(car);
                car.Stop(car);
            }
            //signal that car finished the race        
            finishedRace.Signal();
        }
    }
}
