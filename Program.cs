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

        Line statusBar = new(5, consoleY - 4, consoleX - 5 - 1, consoleY - 4, "dot-dot"); //линия статус-бара 

        //const int fontSize = 16;

        //для верхней панели
        const string version = "v0.4.2043 alpha";
        const string info = "RogueMath" + " " + version;

        //прописываем настройки консоли
        Console.SetWindowSize(consoleX, consoleY);
        if (isDebug) Console.SetBufferSize(consoleX, (consoleY + 1) * countBuferMaps);
        else Console.SetBufferSize(consoleX, consoleY);
        Console.CursorVisible = false;
        Console.Title = info;

        //задаём комнату(ы) вручную через список
        List<Room> manual_rooms = new() { new Room((consoleX / 2) - 11, (consoleY / 2) - 6, 23, 13, RoomType.Spawn) { isExplored = true } };

        //задаём карту
        Map map = new(consoleX, consoleY, edge, manual_rooms);

        //спавним игрока
        Player player = new Player(Race.Human, ((2 * map.rooms[0].x) + map.rooms[0].wigth) / 2, ((2 * map.rooms[0].y) + map.rooms[0].height) / 2);
        map.AddChange(new(player.x, player.y, CellID.Player));

        //генерируем карту
        map.Create(player);

        //выводим первоначальную карту и спавним игрока  
        Console.SetCursorPosition(player.x, player.y);
        Console.Write((char)map.cellMap[player.x, player.y].cellID);
        Console.SetCursorPosition(0, 0);
        map.Update();

        //процесс игры
        while (true)
        {
            player.Advenchuring(map);
        }
    }
}