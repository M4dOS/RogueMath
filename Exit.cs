﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueMath
{
    internal class Exit
    {
        public int x, y; //координаты
        /*protected int width; //ширина двери (вероятно не реализуется)*/
        protected bool isOpen; //открыта ли дверь?
        protected bool isNone; //можно ли наступать на клетку? (1 для пустоты и туннелей, остальное редактируется отдельно)
        public Exit(int x, int y) 
        {
            this.x = x;
            this.y = y;
            isOpen = true;
            isNone = true;
        }
    }
}
