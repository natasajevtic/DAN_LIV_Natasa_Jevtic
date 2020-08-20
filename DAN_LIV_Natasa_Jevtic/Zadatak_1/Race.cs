using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Zadatak_1
{
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
                    Program.cars[i].TankVolume -= random.Next(1, 15);
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
                for (int i = 0; i < 2; i++)
                {
                    Console.WriteLine(colorsOnSemaphore[i] + " light.");
                    semaphoreColor = colorsOnSemaphore[i];
                    Thread.Sleep(2000);
                    //if all cars passed the semaphore, display message and stop thread
                    if (counterOfCarPassedSemaphore == 3)
                    {
                        Console.WriteLine("All cars passed the semaphore.");
                        return;
                    }
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
            Console.WriteLine(car.RegistrationNumber + " " + car.Color + " " + car.Manufacturer + " reached a traffic light. Currently is " + semaphoreColor + "light.");
            while (semaphoreColor == "red")
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
