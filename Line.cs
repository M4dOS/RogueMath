using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueMath
{
    internal class Line
    {
        int xStart;
        int yStart; //координаты начала
        int xEnd;
        int yEnd; //координаты конца
        int length; //длина отрезка
        public Line(int xStart, int yStart, int xEnd, int yEnd) 
        {//конструктор
            this.xStart = xStart;
            this.yStart = yStart;
            this.xEnd = xEnd;
            this.yEnd = yEnd;
            if (xEnd == xStart)
            {
                this.length = (yEnd - yStart);
            }
            else
            {
                this.length = (xEnd - xStart);
            }
        }
    }
}
