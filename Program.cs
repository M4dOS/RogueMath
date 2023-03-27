using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using RogueMath;



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
            a.Movement( myArray );
            Console.Clear();
            map.PrintMap();
        }
        
    }
}