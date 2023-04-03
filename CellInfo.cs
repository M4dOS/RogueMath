namespace RogueMath
{
    internal class CellInfo //информация по клетке 
    { 
        public int x, y; //координаты
        public CellID cellID; //id клетки
        public bool isStepible; //наступать можно?

        public CellInfo(int x, int y, CellID cell, bool isStepible) //создание 
        {
            this.x = x;
            this.y = y;
            this.cellID = cell;
            this.isStepible = isStepible;
        }

        public CellInfo(int x, int y, CellID cell) //создание 
        {
            this.x = x;
            this.y = y;
            this.cellID = cell;
            List<CellID> notStepible = new() {CellID.HWall, CellID.VWall, CellID.ExitClose, /*CellID.Void,*/
                                              CellID.Enemy, CellID.Chest, CellID.Shop, CellID.MainVSpot,
                                              CellID.SecondVSpot};
            if (notStepible.Contains(cell)) this.isStepible = false;
            else this.isStepible = true;
        }

        public CellInfo(CellInfo copy) //создание копии 
        {
            this.x = copy.x;
            this.y = copy.y;
            this.cellID = copy.cellID;
        }
    }
}
