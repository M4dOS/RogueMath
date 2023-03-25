using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueMath
{
    internal class Map
    {
        protected int maxX, maxY; //границы карты
        CellInfo[,] cellMap; //карта "клеток" (вероятно будет двойным массивом(списком))
        List<Room> rooms; //список комнат

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

        public void GenerateMap(CellInfo[,] inputMap)
        {
            for (int i = 0; i < inputMap.GetLength(0); ++i)
            {
                for (int j = 0; j < inputMap.GetLength(1); ++j)
                {
                    cellMap[i, j] = inputMap[i, j];
                }
            }

            for (int i = 0; i < this.cellMap.GetLength(0); ++i)
            {
                for (int j = 0; j < this.cellMap.GetLength(1); ++j)
                {
                    if (cellMap[i, j] == null)
                    {
                        cellMap[i, j] = new CellInfo(i, j, CellID.Void);
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
                    if (j == this.maxY - 1 && i == 0 || i == this.maxY - 1 && j == 0) //углы побочной диагонали
                    {
                        Console.Write("⋱");
                    }
                    else if (j == 0 && i == 0 || i == this.maxY - 1 && j == this.maxY - 1) //углы главной диагонали
                    {
                        Console.Write("⋰");
                    }
                    else if (j == this.maxY - 1 || j == 0) //горизонтальные стены
                    {
                        Console.Write("⋯");
                    }
                    else if (i == this.maxX - 1 || i == 0) //вертикальные стены
                    {
                        Console.Write("⋮");
                    }
                    else
                    {
                        Console.Write((char)this.cellMap[i, j].cellID);
                    }
                }
                Console.WriteLine();
            }
        }
        public Map(int maxX, int maxY) 
        { //конструктор (under construction)
            this.cellMap = new CellInfo[maxX,maxY];
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
