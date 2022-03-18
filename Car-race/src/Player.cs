using System;

namespace ConsoleApplication10
{
    public class Player
    {
        public int x = Console.WindowWidth / 2;
        public int y = Console.WindowHeight + Console.WindowHeight / 2 - 5;
        public bool isImmortal = false;
        public ConsoleColor color = ConsoleColor.Green;

        private int blinkPhase = 0;

        public Player()
        {
            //Draw();
        }

        public void Draw()
        {
            if (isImmortal) // blinking
            {
                blinkPhase++;

                if (blinkPhase % 3 == 0) return;

                if (blinkPhase % 2 == 0)
                {
                    color = ConsoleColor.Red;
                }
                else
                {
                    color = ConsoleColor.Green;
                }
            }
            else color = ConsoleColor.Green;

            Console.ForegroundColor = color;
            Console.CursorTop = y;
            Console.CursorLeft = x;
            Console.WriteLine("|/\\|");
            Console.CursorLeft = x;
            Console.WriteLine("`||´");
            Console.CursorLeft = x;
            Console.WriteLine("|<>|");
            Console.ForegroundColor = ConsoleColor.Gray;
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
