using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBeta
{
    public class ViewClass
    {
        // Lock to prevent the console courses to be moved during the function call
        private readonly object _lock = new object();

        // Sets window size
        public ViewClass()
        {
            Console.SetWindowSize(80, 30);
        }

        // Gets user input
        public async Task<ConsoleKeyInfo> GetInput()
        {
            await Task.Delay(1);
            ConsoleKeyInfo input = Console.ReadKey(true);
            if (input.KeyChar == '?') DisplayHelp();
            return input;
        }

        // Updates the displayed score
        public void UpdateScore(int score)
        {
            lock (_lock)
            {
                Console.SetCursorPosition(70, 0);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"Score: {score}");
            }
        }

        // Updates the displayed level
        public void UpdateLevel(int level)
        {
            lock (_lock)
            {
                Console.SetCursorPosition(60, 0);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"Level: {level}");
            }
        }

        // Displays directions
        public async void DisplayHelp()
        {
            // Displays help
            lock (_lock)
            {
                Console.SetCursorPosition(0, 0);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Move with arrows or wasd, 1-9 to fire.");
            }

            // waits 5 seconds
            await Task.Delay(5000);

            // clears line
            lock (_lock)
            {
                char[] blankline = new char[80];
                Console.Write(blankline);
            }
        }

        // Displays
        public void Display(GameObject gameObject)
        {
            lock (_lock)
            {
                Console.SetCursorPosition(gameObject.position.x, gameObject.position.y); // Setting cursor position
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(gameObject.sprite);
                Console.SetCursorPosition(0, 0);
                Console.ForegroundColor = ConsoleColor.Black;
            }
        }

        // Remove Object
        public void RemoveObject(GameObject gameObject)
        {
            lock (_lock)
            {
                Console.SetCursorPosition(gameObject.position.x, gameObject.position.y);
                Console.Write(gameObject.cleaner);
                Console.SetCursorPosition(0, 0);
            }
        }

        // Explosion
        public void ExplodeObject(Explosion gameObject)
        {
            lock (_lock)
            {
                Console.SetCursorPosition(gameObject.position.x, gameObject.position.y);
                Console.BackgroundColor = (ConsoleColor)gameObject.colors.ElementAt(gameObject.tick);
                Console.ForegroundColor = Console.BackgroundColor;
                Console.Write(String.Concat(Enumerable.Repeat(" ", gameObject.size)));
                Console.SetCursorPosition(0, 0);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = Console.BackgroundColor;
            }
        }

        // Displays game over message
        public void GameOver()
        {
            lock (_lock)
            {
                Console.SetCursorPosition(0, 0);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Game Over!");
                Console.ReadLine();
            }
        }

        // Displays the current weapon
        public void DisplayWeapon(Weapons weapon)
        {
            lock (_lock)
            {
                Console.SetCursorPosition(0, Math.Min(Console.BufferHeight, Console.WindowHeight) - 1);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Current Weapon {weapon}");
            }
        }

    }
}
