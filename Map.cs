using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueMath
{
    internal class Map
    {
        int maxX, maxY; //границы карты
        CellInfo[,] cellMap; //карта "клеток" (вероятно будет двойным массивом(списком))
        public List<Room> rooms; //список комнат

        /*public void FindSize()
        { //находит размеры
            foreach (CellInfo axes in this.cellMap)
            {
                if (axes.x > maxX)
                {
                    maxX = axes.x;
                }
                if (axes.y > maxY)
                {
                    maxY = axes.y;
                }
            }
        }*/

        /* public void SortCells()
        { //сортировка клеток
            List<List<CellInfo>> sortedMap = new List<List<CellInfo>>();
            for (int i = 0; i < this.maxX; ++i)
            {
                for (int j = 0; i < this.maxY; ++j)
                {
                    foreach (List<CellInfo> axes1 in this.cellMap)
                    {
                        foreach (CellInfo axes in axes1)
                        {
                            if (axes.x == i && axes.y == j)
                            {
                                sortedMap.Add(axes1);
                                break;
                            }
                        }
                    }
                }
            }

            this.cellMap = sortedMap;
        }*/

        public void GenerateMap()
        {
            //генерация крайних стен и пустот
            for (int i = 0; i < this.maxY; i++)
            {
                for (int j = 0; j < this.maxX; j++)
                {
                    if (j == this.maxX - 1 && i == 0 || i == this.maxY - 1 && j == 0) //углы побочной диагонали
                    {
                        cellMap[j, i] = new CellInfo(j, i, CellID.SecondVSpot);
                    }
                    else if (i == 0 && j == 0 || j == this.maxX - 1 && i == this.maxY - 1) //углы главной диагонали
                    {
                        cellMap[j, i] = new CellInfo(j, i, CellID.MainVSpot);
                    }
                    else if (i == this.maxY - 1 || i == 0) //горизонтальные стены
                    {
                        cellMap[j, i] = new CellInfo(j, i, CellID.HWall);
                    }
                    else if (j == this.maxX - 1 || j == 0) //вертикальные стены
                    {
                        cellMap[j, i] = new CellInfo(j, i, CellID.VWall);
                    }
                    else
                    {
                        cellMap[j, i] = new CellInfo(j, i, CellID.Void);
                    }
                }
            }

            //генерация комнат

            foreach (Room room in rooms)
            {
                for (int i = room.y; i < room.height+room.y; ++i)
                {
                    for (int j = room.x; j < room.wigth+room.x; ++j)
                    {
                        if (j == room.wigth + room.x - 1 && i == room.y || i == room.height + room.y - 1 && j == room.x) //углы побочной диагонали
                        {
                            cellMap[j, i] = new CellInfo(j, i, CellID.SecondVSpot);
                        }
                        else if (i == room.y && j == room.x || j == room.wigth + room.x - 1 && i == room.height + room.y - 1) //углы главной диагонали
                        {
                            cellMap[j, i] = new CellInfo(j, i, CellID.MainVSpot);
                        }
                        else if (i == room.height + room.y - 1 || i == room.y) //горизонтальные стены
                        {
                            cellMap[j, i] = new CellInfo(j, i, CellID.HWall);
                        }
                        else if (j == room.wigth + room.x - 1 || j == room.x) //вертикальные стены
                        {
                            cellMap[j, i] = new CellInfo(j, i, CellID.VWall);
                        }
                        else
                        {
                            cellMap[j, i] = new CellInfo(j, i, CellID.None);
                        }
                    }
                }
            }
        }

        public void PrintMap()
        { //печать карты
            for (int j = 0; j < this.maxY;++j)
            {
                for(int i = 0; i < this.maxX; ++i)
                {
                        Console.Write((char)this.cellMap[i, j].cellID);
                }
                Console.WriteLine();
            }
        }
        public Map(int maxX, int maxY) 
        { //конструктор (under construction)
            this.cellMap = new CellInfo[maxX,maxY];
            this.rooms = new List<Room>();
            this.maxX = maxX;
            this.maxY = maxY;
        }
    }

    internal class CellInfo
    { //информация по клетке
        public int x, y; //координаты
        public CellID cellID { get; } //id клетки
        public char cellName { get;  }
        public CellInfo (int x, int y, CellID cell, char chr) 
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
    }
}
