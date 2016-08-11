
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Models
{
    public class Direction
    {
        public string Arrow { get; private set; }
        public int Chance { get; set; }
        public float X { get; private set; }
        public float Y { get; private set;}

        public Direction(float x, float y, int chance, string arrow)
        {
            X = x;
            Y = y;
            Chance = chance;
            Arrow = arrow;
        }
    }
}


