using System;

namespace ConsoleApplication10
{
    internal class Car
    {
        public int x;
        public int y;
        public ConsoleColor color = ConsoleColor.DarkRed;

        public Car(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void Draw()
        {
            try
            {
                Console.ForegroundColor = color;
                Console.CursorTop = y;
                Console.CursorLeft = x;
                Console.WriteLine("]--[");
                Console.CursorLeft = x;
                Console.WriteLine("`||´");
                Console.CursorLeft = x;
                Console.WriteLine("-\\/-");
                Console.ForegroundColor = ConsoleColor.Gray;
            } catch (Exception ex)
            {

            }
        }

        public void MoveRight(int amount = 1)
        {
            x = x + amount;
        }

        public void MoveLeft(int amount = 1)
        {
            x = x - amount;
        }

        public void MoveDown(int amount = 1)
        {
            y = y + amount;
        }

        public void MoveUp(int amount = 1)
        {
            y = y - amount;
        }
    }
}