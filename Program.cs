using RogueMath;
using System.Text;
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
        const bool isDebug = false;

        //const int fontSize = 16;

        //для верхней панели
        const string version = "v0.1.1927 alpha";
        const string info = "RogueMath" + " " + version;

        //прописываем настройки консоли
        Console.SetWindowSize(consoleX, consoleY);
        if (isDebug) Console.SetBufferSize(consoleX, (consoleY + 1) * countBuferMaps);
        else Console.SetBufferSize(consoleX, consoleY);
        Console.CursorVisible = false;
        Console.Title = info;

        //задаём комнату(ы) вручную через список
        List<Room> manual_rooms = new() { new Room(35, 7, 10, 16) };

        //производим генерацию карты
        Map map = new(consoleX, consoleY, edge, manual_rooms); map.Create();

        //спавним игрока
        Player player = new Player(1, Race.Human, map.rooms[0].x + 3, map.rooms[0].y + 3);
        map.cellMap[player.x, player.y].cellID = CellID.Player;

        //выводим первоначальную карту
        map.PrintMap();

        //процесс игры
        while (true)
        {
            if (player.Movement(map)) { Console.Clear(); map.PrintMap(); }
            else { }
        }
    }
}