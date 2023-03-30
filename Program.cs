using System.Text;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using Windows.UI.Text;
using Windows.Networking;

namespace RogueMath
{
class Programm
    {
        static void Main(string[] args)
        {

            //задаём кодировку
            Console.OutputEncoding = Encoding.Unicode;

            //задаём неизменные параметры
            const int consoleX = 165;
            const int consoleY = 55;
            const int edge = 4;
            const int countBuferMaps = 5;

            //const int fontSize = 16;

            //для верхней панели
            const string version = "v0.52b";
            const string info = "RogueMath" + " " + version;

            //прописываем настройки консоли
            Console.SetWindowSize(consoleX, consoleY);
            Console.SetBufferSize(consoleX, (consoleY + 1) * countBuferMaps);
            Console.Title = info;

            //генерация карты
            Map map = new(consoleX, consoleY, edge);


            map.PrintMap();

            /*Room room1 = new Room(4, 6, 23, 15);
            map.rooms.Add(room1);

            Room room2 = new Room(35, 7, 10, 16);
            map.rooms.Add(room2);*/

        }
    }
}