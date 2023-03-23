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
                    
                }
            }
        }

        public void PrintMap()
        { //печать карты
            for (int i = 0; i < this.maxX;++i)
            {
                for(int j = 0; i < this.maxY; ++j)
                {
                    Console.WriteLine(this.cellMap[i][j].cellID);
                }
            }
        }
        public Map(int maxX, int maxY) 
        { //конструктор (under construction)
            this.cellMap = new CellInfo[maxX][maxY];
        }
    }

    internal class CellInfo
    { //информация по клетке
        public int x, y; //координаты
        public CellID cellID; //id клетки
    }
}
