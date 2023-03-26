using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueMath
{
    internal class CellInfo
    { //информация по клетке
        public int x, y; //координаты
        public CellID cellID { get; } //id клетки
        public char cellName { get; }
        public CellInfo(int x, int y, CellID cell, char chr)
        {
            this.x = x;
            this.y = y;
            this.cellID = cell;
            this.cellName = chr;

        }

        public CellInfo(int x, int y, CellID cell)
        {
            this.x = x;
            this.y = y;
            this.cellID = cell;
        }

        public CellInfo(CellInfo copy)
        {
            this.x = copy.x;
            this.y = copy.y;
            this.cellID = copy.cellID;
            this.cellName = copy.cellName;
        }
    }
}
