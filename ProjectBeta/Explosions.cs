using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBeta
{

    public class Explosion
    {
        // Placeholders
        public PointStruct position;
        public int tick;
        public int size;
        public int[] colors = { 15, 12, 14, 9, 0 };

        // Constructor
        public Explosion(PointStruct position, int size)
        {
            this.position = position;
            this.size = size;
            this.tick = 0;
        }
    }
}
