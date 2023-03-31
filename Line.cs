namespace RogueMath
{
    internal class Line
    {
        //координаты начала
        public int xStart;
        public int yStart;

        //координаты конца
        public int xEnd;
        public int yEnd;

        public int length; //длина отрезка
        public int mode; //направление

        public Line(int xStart, int yStart, int mode, int lenght)
        {
            this.xStart = xStart;
            this.yStart = yStart;
            this.mode = mode;
            length = lenght;
            switch (mode)
            {
                case 0: xEnd = xStart + length; break;
                case 1: yEnd = yStart + length; break;
            }
        }
    }
}
