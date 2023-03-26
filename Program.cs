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

            //задаём неизменные параметры
            const int consoleX = 150;
            const int consoleY = 50;
            const int fontSize = 16;

            //прописываем настройки консоли
            Console.SetWindowSize(consoleX, consoleY);

            Map map = new Map(consoleX, consoleY);
            /*Room room1 = new Room(4, 6, 23, 15);
            map.rooms.Add(room1);

            Room room2 = new Room(35, 7, 10, 16);
            map.rooms.Add(room2);*/

            map.RoomPlace();
            map.GenerateMap();
            map.PrintMap();
        }
    }
}