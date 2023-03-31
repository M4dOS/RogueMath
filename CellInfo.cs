namespace RogueMath
{
    internal class CellInfo
    { //информация по клетке
        public int x, y; //координаты
        public CellID cellID { get; set; } //id клетки

        /*public char cellName { get; } 
        public CellInfo(int x, int y, CellID cell, char chr)
        {
            this.x = x;
            this.y = y;
            this.cellID = cell;
            this.cellName = chr;

        }*/

        public CellInfo(int x, int y, CellID cell)
        {
            this.x = x;
            this.y = y;
            cellID = cell;
        }

        public CellInfo(CellInfo copy)
        {
            x = copy.x;
            y = copy.y;
            cellID = copy.cellID;
            /*this.cellName = copy.cellName;*/
        }
    }
}
