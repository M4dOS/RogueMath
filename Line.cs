using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueMath
{
    internal class Lines
    {
        short xStart, yStart;
        short xEnd, yEnd;
        short length;
        public Lines(short xStart, short yStart, short xEnd, short yEnd) 
        {
            this.xStart = xStart;
            this.yStart = yStart;
            this.xEnd = xEnd;
            this.yEnd = yEnd;
            if (xEnd == xStart)
            {
                this.length = (short)(yEnd - yStart);
            }
            else
            {
                this.length = (short)(xEnd - xStart);
            }
        }
    }
}
