using RogueMath.Item_Pack;
using System.Text;

namespace RogueMath;


static class Program
{
    public static void Main(string[] args)

    {
        //задаём кодировку
        Console.OutputEncoding = Encoding.Unicode;
        Console.InputEncoding = Encoding.Unicode;

        //задаём неизменные параметры
        const int consoleX = 165;
        const int consoleY = 55;
        const int edge = 4;
        const int countBuferMaps = 5;
        const bool isDebug = false;

        Line statusBar = new(5, consoleY - 4, consoleX - 5 - 1, consoleY - 4, "dot-dot"); //линия статус-бара 

        //const int fontSize = 16;

        //для верхней панели
        const string version = "v2.0.0059 beta";
        const string info = "RogueMath" + " " + version;

        //прописываем настройки консоли
        Console.SetWindowSize(consoleX, consoleY);
        if (isDebug) Console.SetBufferSize(consoleX, (consoleY + 1) * countBuferMaps);
        else Console.SetBufferSize(consoleX, consoleY);
        Console.CursorVisible = false;
        Console.Title = info;

        //задаём комнату(ы) вручную через список
        int i = 1;
        while(i<=6)
        {
            //задаём карту
            Map map = new(consoleX, consoleY, edge) { floor = i };

            //генерируем карту
            map.Create(5);

            //спавним игрока
            int index = 0;
            if (i == 6)
            {
                if (map.Win())
                {
                    i = 1;
                    foreach (Room room in map.rooms)
                    {
                        if (room.roomType == RoomType.Spawn) { index = map.rooms.IndexOf(room); room.isExplored = true; map.floor = i; break; }
                    }
                }
            }

            foreach (Room room in map.rooms)
            {
                if (room.roomType == RoomType.Spawn) { index = map.rooms.IndexOf(room); room.isExplored = true; map.floor = i; break; }
            }

            Player player = new Player(map, index);
            
            map.AddEnemies(player);
            map.AddChange(new(player.x, player.y, player.sign));

            //выводим первоначальную карту и спавним игрока
            map.Update();

            //процесс игры
            while (player.Advenchuring(map)) { }
            if (player.dead) i = 1;
            else i++;
        }
    }
}