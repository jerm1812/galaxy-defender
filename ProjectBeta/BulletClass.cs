using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBeta
{
    public class BulletClass : GameObject
    {
        // Bullet velocity
        public int xVel;
        public int yVel;

        public Weapons bulletType;

        // Constructor
        public BulletClass(PointStruct position, int xVel, int yVel, Weapons bulletType, Objects type = Objects.Bullet, string sprite = "*") : base (position, sprite, type)
        {
            this.xVel = xVel;
            this.yVel = yVel;
            this.bulletType = bulletType;
        }

    }
}
