using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBeta
{
    public class EnemyClass : GameObject
    {
        // State of enemy
        public Aggression state;

        public EnemyClass(PointStruct position, Aggression state = Aggression.Passive, string sprite = "<^>", Objects type = Objects.Enemy) : base(position, sprite, type)
        {
            this.state = state;
        }
    }
}
