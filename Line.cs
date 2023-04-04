namespace RogueMath
{
    internal class Line //линии 
    {
        //координаты начала
        public int xStart;
        public int yStart;

        //координаты конца
        public int xEnd;
        public int yEnd;

        public int length; //длина отрезка
        public int mode; //направление

        public bool isExplored;

        public Line(int xStart, int yStart, int param1, int param2, string str) //создание по длине
        {
            isExplored = true;
            switch (str)
            {
                case "dot-lenght":
                    this.xStart = xStart;
                    this.yStart = yStart;
                    mode = param1;
                    length = param2;
                    switch (param1)
                    {
                        case 0: yEnd = yStart - length; break;
                        case 1: yEnd = yStart + length; break;
                        case 2: xEnd = xStart - length; break;
                        case 3: xEnd = xStart + length; break;
                    }
                    break;

                case "dot-dot":
                    this.xStart = xStart;
                    this.yStart = yStart;
                    xEnd = param1;
                    yEnd = param2;

                    if (this.xStart - xEnd == 0 && this.yStart - yEnd > 0) //вверх
                    { length = Math.Abs(this.yStart - yEnd); mode = 0; }

                    else if (this.xStart - xEnd == 0 && this.yStart - yEnd < 0) //вниз
                    { length = Math.Abs(this.yStart - yEnd); mode = 1; }

                    else if (this.xStart - xEnd > 0 && this.yStart - yEnd == 0) //влево
                    { length = Math.Abs(this.xStart - xEnd); mode = 2; }

                    else if (this.xStart - xEnd < 0 && this.yStart - yEnd == 0) //вправл
                    { length = Math.Abs(this.xStart - xEnd); mode = 3; }

                    else //вкось
                    {
                        mode = -1;
                        length = (int)Math.Sqrt(Math.Pow(xEnd - this.xStart, 2) + Math.Pow(yEnd - this.yStart, 2));
                    }
                    break;

                default: break;
            }
        }

        static public int Where(int xFrom, int yFrom, int xTo, int yTo, int lastMode) //куда заворачивать дальше 
        {
            if (lastMode == 0 || lastMode == 1)
            {
                if (Math.Abs(xTo - (xFrom + 1)) < Math.Abs(xTo - (xFrom - 1)))
                {
                    return 3;
                }
                else return 2;
            }
            if (lastMode == 2 || lastMode == 3)
            {
                if (Math.Abs(yTo - (yFrom + 1)) < Math.Abs(yTo - (yFrom - 1)))
                {
                    return 1;
                }
                else return 0;
            }

            else return -1;
        }

        public void DrawMapPlacer(Map map) //рисование занятости для туннеля 
        {
            for (int x = xStart; x < xEnd; x++)
            {
                for (int y = yStart; y < yEnd; y++)
                {

                    for (int overX = xStart - 1; overX < xEnd + 1; overX++)
                    {
                        for (int overY = yStart - 1; overX < yEnd + 1; overY++)
                        {
                            map.mapplace[overX, overY] = true;
                        }
                    }

                }
            }
        }

        public void Exploring(Player player)
        {
            if ((player.x == xStart + 1 && player.y == yStart)
                || (player.x == xStart - 1 && player.y == yStart)
                || (player.x == xStart && player.y == yStart + 1)
                || (player.x == xStart && player.y == yStart - 1)
                || (player.x == xEnd + 1 && player.y == yEnd)
                || (player.x == xEnd - 1 && player.y == yEnd)
                || (player.x == xEnd && player.y == yEnd + 1)
                || (player.x == xEnd && player.y == yEnd - 1))
            {
                isExplored = true;
            }
        }
    }
}
