using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RogueMath
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.OutputEncoding = Encoding.UTF8;

            //задаём неизменные размеры окна
            const int consoleX = 150;
            const int consoleY = 50;
            const int fontSize = 16;

            //прописываем настройки консоли
            Console.SetWindowSize(consoleX, consoleY);

            Map map = new Map(consoleX, consoleY);
            CellInfo[,] myArray = new CellInfo[25, 14];

            for(int i = 0; i < myArray.GetLength(0); i++)
            {
                for(int j = 0; j < myArray.GetLength(1); j++)
                {
                    if (i==0 || j==0 || i==myArray.GetLength(0)-1 || j == myArray.GetLength(1)-1)
                    {
                        myArray[i,j] = new CellInfo(i,j,CellID.Wall);
                    }
                    else
                    {
                        myArray[i, j] = new CellInfo(i, j, CellID.None);
                    }
                }
            }

            map.GenerateMap(myArray);
            map.PrintMap();
        }
    }
}