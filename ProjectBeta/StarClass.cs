using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBeta
{
    public class StarClass : GameObject
    {
        // Collision
        public Guid colideWith;
        public bool currentCollision;
        public bool displayed;

        // Constructor
        public StarClass(PointStruct position, string sprite = "+", Objects type = Objects.Star) : base(position, sprite, type)
        {
            this.currentCollision = false;
            this.displayed = true;
        }

    }
}
