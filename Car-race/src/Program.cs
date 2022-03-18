using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace ConsoleApplication10
{
    class Program
    {
        static Player player = new Player();
        static List<Car> carList = new List<Car>();
        static System.Timers.Timer timer = new System.Timers.Timer();
        private static int ticks = 0;
        private static bool isGameOver = false;
        private static int carsFreq = 5; // 1 - biggest amount
        private static int health = 3;
        private static int speed = 1;

        static void Main(string[] args)
        {
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
            Console.CursorVisible = false;
            Console.SetWindowSize(Console.WindowWidth, Convert.ToInt32(Console.LargestWindowHeight * 0.6));

            Thread inputThread = new Thread(ReadInput);
            inputThread.Start();

            while (!isGameOver)
            {
                //Console.Clear();
                if (ticks == 150) speed++;
                if (ticks == 500) speed++;
                if (ticks == 1200)
                {
                    speed++;
                    carsFreq--;
                }
                if (ticks == 1800) speed++;
                if (ticks == 2800)
                {
                    speed++;
                    carsFreq--;
                }
                if (ticks == 3500)
                {
                    speed++;
                    carsFreq--;
                }

                // Road drawing
                for (int i = 0; i < Console.WindowHeight; i++)
                {
                    Console.CursorTop = Console.WindowTop + i;
                    Console.CursorLeft = Console.WindowWidth / 2 - Console.WindowWidth / 4;
                    Console.WriteLine("||");
                }
                for (int i = 0; i < Console.WindowHeight; i++)
                {
                    Console.CursorTop = Console.WindowTop + i;
                    Console.CursorLeft = Console.WindowWidth / 2 + Console.WindowWidth / 4;
                    Console.WriteLine("||");
                }
                for (int i = 0; i < Console.WindowHeight - 1; i += 2)
                {
                    Console.CursorTop = Console.WindowTop + i;
                    Console.CursorLeft = Console.WindowWidth / 2;
                    Console.WriteLine("|");
                }

                Console.CursorTop = Console.WindowTop;
                Console.WriteLine(" SCORE: " + ticks);
                Console.WriteLine(" HEALTH: " + health);

                Random rand = new Random();
                if (rand.Next(0, carsFreq) == 1)
                {
                    Car car = new Car(rand.Next(Console.WindowWidth / 2 - Console.WindowWidth / 4, Console.WindowWidth / 2 + Console.WindowWidth / 4), Console.WindowTop);
                    carList.Add(car);
                }

                for (int i = 0; i < carList.Count; i++)
                {
                    Car car = carList[i];
                    if (car.y + 1 >= Console.WindowTop + Console.WindowHeight)
                    {
                        carList.RemoveAt(i);
                    }
                    else
                    {
                        car.MoveDown(speed);
                        car.Draw();
                    }
                }

                player.Draw();
                DetectCollision();

                if (health < 1) EndGame();

                ticks++;
                Thread.Sleep(25);
                Console.Clear();
            }
        }

        private static void DetectCollision()
        {
            if (!player.isImmortal) // with other cars
            {
                foreach (Car car in carList)
                {
                    if (((car.x <= player.x && car.x + 4 >= player.x) && (car.y + 3 >= player.y && car.y <= player.y)) ||
                        ((car.x <= player.x + 4 && car.x >= player.x) && (car.y + 3 >= player.y && car.y <= player.y))) // hitboxes
                    {
                        player.isImmortal = true;
                        health--;
                        timer.Elapsed += new ElapsedEventHandler(ChangeToMortal);
                        timer.Interval = 1000;
                        timer.Enabled = true;
                    }
                }
            }

            if (player.x < Console.WindowWidth / 2 - Console.WindowWidth / 4 ||
                player.x > Console.WindowWidth / 2 + Console.WindowWidth / 4 - 2) EndGame();
            if (player.y >= Console.WindowHeight - 1 ||
                player.y <= Console.WindowTop) EndGame();
        }
        private static void ChangeToMortal(Object o, ElapsedEventArgs e)
        {
            player.isImmortal = false;
            timer.Enabled = false;
        }

        private static void EndGame()
        {
            isGameOver = true;
            string msg = "GAME OVER";
            string scoreMsg = "Your score: " + ticks;
            Console.Clear();
            Console.CursorTop = Console.WindowHeight / 2;
            Console.CursorLeft = Console.WindowWidth / 2 - msg.Length / 2;
            Console.WriteLine(msg);
            Console.CursorLeft = Console.WindowWidth / 2 - scoreMsg.Length / 2;
            Console.WriteLine(scoreMsg);
            Thread.Sleep(100000);
        }

        private static void ReadInput()
        {
            while (true)
            {
                ConsoleKey readKey = Console.ReadKey().Key;

                if (readKey == ConsoleKey.LeftArrow || readKey == ConsoleKey.A) player.MoveLeft(speed);
                else if (readKey == ConsoleKey.RightArrow || readKey == ConsoleKey.D) player.MoveRight(speed);
                else if (readKey == ConsoleKey.UpArrow || readKey == ConsoleKey.W) player.MoveUp(speed);
                else if (readKey == ConsoleKey.DownArrow || readKey == ConsoleKey.S) player.MoveDown(speed);
                else if (readKey == ConsoleKey.Enter)
                {
                    
                }
            }
        }
    }
}
