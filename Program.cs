using RogueMath;
using System.Text;

public class Weapon
{

}

public class Buff
{

}

internal class Program
{

    static void Main(string[] args)

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
        const string version = "v0.1.1927 alpha";
        const string info = "RogueMath" + " " + version;

        //прописываем настройки консоли
        Console.SetWindowSize(consoleX, consoleY);
        Console.SetBufferSize(consoleX, (consoleY + 1) * countBuferMaps);
        Console.Title = info;

        //генерация карты
        Map map = new(consoleX, consoleY, edge);

        Player player = new Player(1, Race.Human, map.rooms[0].x + 3, map.rooms[0].y + 3);

        map.cellMap[player.x, player.y].cellID = CellID.Player;
        map.PrintMap();

        while (true)
        {
            player.Movement(map);
            Console.Clear();
            map.PrintMap();

            /*Room room1 = new Room(4, 6, 23, 15);
            map.rooms.Add(room1);

            Room room2 = new Room(35, 7, 10, 16);
            map.rooms.Add(room2);*/
        }

    }
}