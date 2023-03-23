using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueMath
{
    internal class Map
    {
        short maxX = 0;
        short maxY = 0;
        List<CellInfo> cellMap;
        List<Room> rooms;
        public void FindSize()
        {
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
        }

        public void SortCells()
        {
            List<CellInfo> sortedMap = new List<CellInfo>();
            for (int i = 0; i < (int)this.maxX; ++i)
            {
                for (int j = 0; i < (int)this.maxY; ++j)
                {
                    foreach (CellInfo axes in this.cellMap)
                    {
                        if(axes.x == i &&  axes.y == j)
                        {
                            sortedMap.Add(axes);
                            break;
                        }
                    }
                }
            }

            this.cellMap = sortedMap;
        }

        public void PrintMap()
        {
            for (int i = 0; i < (int)this.maxX;++i)
            {
                for(int j = 0; i < (int)this.maxY; ++j)
                {
                    Console.WriteLine(this.cellMap[i].cellID);
                }
            }
        }
        public Map() 
        {

        }
    }

    internal class CellInfo
    {
        public short x, y;
        public CellID cellID;
    }
}
