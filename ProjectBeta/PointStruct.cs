using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBeta
{
    public struct PointStruct
    {
        // Console limits
        static int yLimit = Math.Min(Console.BufferHeight, Console.WindowHeight) - 2;
        static int xLimit = Math.Min(Console.BufferWidth, Console.WindowWidth);

        // Cords
        public int x { get; set; }
        public int y { get; set; }

        // Constructor
        public PointStruct(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        // Moves cords
        public bool Move(GameObject gameObject, int x, int y)
        {
            this.x += x;
            this.y += y;

            // bullet out of bounds
            if (gameObject.type == Objects.Bullet && (this.x == 0 || this.y == 1 || this.x >= xLimit || this.y > yLimit))
            {
                return true;
            }
            else
            {
                // Boundaries
                if (this.x < 1) this.x = 1;
                else if (this.y < 1) this.y = 1;
                else if (this.x > xLimit - gameObject.sprite.Length) this.x = xLimit - gameObject.sprite.Length;
                else if (this.y > yLimit) this.y = yLimit;
            }
            
            return false;
        }
    }
}
