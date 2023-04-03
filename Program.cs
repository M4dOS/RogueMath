using RogueMath;
using System;
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
        const bool isDebug = true;

        Line statusBar = new(5, consoleY - 4, consoleX - 5 - 1, consoleY - 4, "dot-dot"); //линия статус-бара 
        
        //const int fontSize = 16;

        //для верхней панели
        const string version = "v0.3.2340 alpha";
        const string info = "RogueMath" + " " + version;

        //прописываем настройки консоли
        Console.SetWindowSize(consoleX, consoleY);
        if (isDebug) Console.SetBufferSize(consoleX, (consoleY + 1) * countBuferMaps);
        else Console.SetBufferSize(consoleX, consoleY);
        Console.CursorVisible = false;
        Console.Title = info;

        //задаём комнату(ы) вручную через список
        List<Room> manual_rooms = new() { new Room(consoleX / 2 - 11, consoleY / 2 - 6, 23, 13, RoomType.Spawn) { isExplored = true } };

        //производим генерацию карты
        Map map = new(consoleX, consoleY, edge, manual_rooms); map.Create();

        //спавним игрока
        Player player = new Player(Race.Human, map.rooms[0].x + 3, map.rooms[0].y + 3);
        map.cellMap[player.x, player.y].cellID = CellID.Player;

        List<Enemy> enemies = new();
        for (int i = 0; i < map.rooms.Count; ++i)
        {
            if (map.rooms[i].roomType == RoomType.Empty)
            {
                enemies.Add(new Enemy(player._lvl, Race.Math, map.rooms[i].x + 1, map.rooms[i].y + 1));
                map.cellMap[enemies[i].x, enemies[i].y].cellID = CellID.Enemy;
            }
           // else if (map.rooms[i].roomType == RoomType.Mather)
            //{
                /*enemies.Add(new Enemy(Race.Mather, map.rooms[i].x + 1, map.rooms[i].y + 1));
                map.cellMap[enemies[i].x, enemies[i].y].cellID = CellID.Boss;*/
            //}
            map.cellMap[enemies[i].x, enemies[i].y].enemyId = i;
        }

        //выводим первоначальную карту
        map.PrintMap();
        //спавним игрока и выводим первоначальную карту
        //Player player = new Player(1, Race.Human, (2*map.rooms[0].x + map.rooms[0].wigth)/2, (2*map.rooms[0].y + map.rooms[0].height)/2);
        //map.cellMap[player.x, player.y].cellID = CellID.Player;

        //map.Update(); Console.SetCursorPosition(player.x, player.y); Console.Write((char)map.cellMap[player.x, player.y].cellID);
        //Console.SetCursorPosition(0, 0);

        //процесс игры
        while (true)
        {
            if (player.phase == Player.Phase.Nothing)
            {
                if (player._hp == player._maxHp && player._energy < player._maxEnergy) ++player._energy;
                if (player._hp < player._maxHp) ++player._hp;
                if (player.Movement(map))
                {
                    for (int i = 0; i < enemies.Count; i++) enemies[i].Movement(map);
                    Console.Clear();
                    map.PrintMap();
                }
                player.battlingWith = player.EnemyCheck(map);
                if (player.battlingWith > -1) player.phase = Player.Phase.Battle;
            }
            else if (player.phase == Player.Phase.Battle)
            {
                if (player.Battle(enemies[player.battlingWith]))
                {
                    if (enemies[player.battlingWith]._hp > 0) enemies[player.battlingWith].Bite(player);
                    else
                    {
                        map.cellMap[enemies[player.battlingWith].x, enemies[player.battlingWith].y].cellID = CellID.None;
                        player.phase = Player.Phase.Nothing;
                        enemies[player.battlingWith].dead = true;
                        player.battlingWith = -1;
                        Console.Clear();
                        map.PrintMap();
                    }
                }
            }
            
            //if (player.Movement(map)) {/* Console.Clear();*/ /*map.rooms[0].ChangeDoorsStatus(map);*/ map.Update(); }
            //else { }
        }
    }
}