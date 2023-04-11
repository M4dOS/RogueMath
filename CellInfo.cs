namespace RogueMath
{
    internal class CellInfo
    { //информация по клетке
        public int enemyId;  /// добавлено для кода врага, не удалять

        public int x, y; //координаты
        public CellID cellID; //id клетки
        public bool isStepible; //наступать можно?

        public CellInfo(int x, int y, CellID cell, bool isStepible) //создание 
        {
            this.x = x;
            this.y = y;
            cellID = cell;
            this.isStepible = isStepible;
        }

        public CellInfo(int x, int y, CellID cell) //создание 
        {
            this.x = x;
            this.y = y;
            cellID = cell;
            List<CellID> notStepible = new() {CellID.HWall, CellID.VWall, CellID.ExitClose, /*CellID.Void,*/
                                              CellID.Enemy, CellID.Chest, CellID.Shop, CellID.MainVSpot,
                                              CellID.SecondVSpot, CellID.Player, CellID.Boss};

            if (notStepible.Contains(cell)) isStepible = false;
            else isStepible = true;
        }

        public CellInfo(CellInfo copy) //создание копии 
        {
            x = copy.x;
            y = copy.y;
            cellID = copy.cellID;
            enemyId = copy.enemyId;
        }
    }
}
