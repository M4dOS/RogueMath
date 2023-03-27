using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueMath
{
    internal class Lines
    {
        int xStart, yStart; //координаты начала
        int xEnd, yEnd; //координаты конца
        int length; //длина отрезка
        public Lines(int xStart, int yStart, int xEnd, int yEnd) 
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
