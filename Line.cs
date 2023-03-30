namespace RogueMath
{
    internal class Line
    {
        public int xStart;
        public int yStart; //координаты начала
        public int xEnd;
        public int yEnd; //координаты конца
        public int length; //длина отрезка
        public int mode;

/*        public Line(int xStart, int yStart, int xEnd, int yEnd)
        {//конструктор
            this.xStart = xStart;
            this.yStart = yStart;
            this.xEnd = xEnd;
            this.yEnd = yEnd;
            if (xEnd == xStart)
            {
                this.mode = 1;
                this.length = (yEnd - yStart);
            }
            else if (yEnd == yStart)
            {
                this.mode = 0;
                this.length = (xEnd - xStart);
            }
        }*/

        public Line(int xStart, int yStart, int mode, int lenght)
        {
            this.xStart = xStart;
            this.yStart = yStart;
            this.mode = mode;
            this.length = lenght;
            switch (mode)
            {
                case 0: this.xEnd = xStart + length; break;
                case 1: this.yEnd = yStart + length; break;
            }
        }
    }
}
