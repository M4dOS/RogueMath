
﻿using System;
using System.Collections.Generic;
using System.IO;
﻿using System.Text;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using Windows.UI.Text;
using Windows.Networking;



public class Weapon
{

}

public class Buff
{

}

public enum Race
{
    Math = -1, //враг
    Human = 0, // персонаж
    Mather = 1 // босс
}

internal class Program
{
    static void Main(string[] args)

    {
        Player a = new Player(1, Race.Human, 8, 8);
        Map map = new Map(10, 10);
        CellInfo[,] myArray = new CellInfo[10, 10];

        for (int i = 0; i < myArray.GetLength(0); i++)
        {
            for (int j = 0; j < myArray.GetLength(1); j++)
            {
                if (i == 0 || j == 0 || i == myArray.GetLength(0) - 1 || j == myArray.GetLength(1) - 1)
                {
                    myArray[i, j] = new CellInfo(i, j, CellID.Wall);
                }
                else
                {
                    myArray[i, j] = new CellInfo(i, j, CellID.None);
                }
            }
        }

        map.GenerateMap(myArray);
        myArray[8, 8].cellID = CellID.Player;
        map.PrintMap();
        while (true) 
        {
            
            //задаём кодировку
            Console.OutputEncoding = Encoding.UTF8;

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

            a.Movement( myArray );
            Console.Clear();
            map.PrintMap();

            /*Room room1 = new Room(4, 6, 23, 15);
            map.rooms.Add(room1);

            Room room2 = new Room(35, 7, 10, 16);
            map.rooms.Add(room2);*/
        }
        
    }
}