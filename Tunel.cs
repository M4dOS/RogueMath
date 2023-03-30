
﻿namespace RogueMath
{
    internal class Tunel
    {
        protected Exit exit1; //один конец
        protected Exit exit2; //второй конец
        public List<Line> lines; //система линий
        public Tunel(Exit exit1, Exit exit2, List<Line> lines)
        {
            this.exit1 = exit1;
            this.exit2 = exit2;
            this.lines = lines;

        }
    }
}
