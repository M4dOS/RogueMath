using RogueMath.Item_Pack;
using System.Text;

namespace RogueMath;


static class Program
{
    public static void Main(string[] args)

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
        const string version = "v1.4.0448 alpha";
        const string info = "RogueMath" + " " + version;

        //прописываем настройки консоли
        Console.SetWindowSize(consoleX, consoleY);
        if (isDebug) Console.SetBufferSize(consoleX, (consoleY + 1) * countBuferMaps);
        else Console.SetBufferSize(consoleX, consoleY);
        Console.CursorVisible = false;
        Console.Title = info;

        //задаём комнату(ы) вручную через список
        List<Room> manual_rooms1 = new() { new Room((consoleX / 2) - 11, (consoleY / 2) - 6, 23, 13, RoomType.Spawn) { isExplored = true },
                                      new Room((consoleX / 4) - 11, (consoleY / 4) - 6, 23, 13, RoomType.Mather) { isExplored = false }};

        List<Room> manual_rooms2 = new() { new Room((consoleX / 2) - 11, (consoleY / 2) - 6, 23, 13, RoomType.Spawn) { isExplored = true },
                                      new Room((consoleX / 4) - 11, (consoleY / 4) - 6, 23, 13, RoomType.Mather) { isExplored = false }};

        List<Room> manual_rooms3 = new() { new Room((consoleX / 2) - 11, (consoleY / 2) - 6, 23, 13, RoomType.Spawn) { isExplored = true },
                                      new Room((consoleX / 4) - 11, (consoleY / 4) - 6, 23, 13, RoomType.Mather) { isExplored = false }};

        List<Room> manual_rooms4 = new() { new Room((consoleX / 2) - 11, (consoleY / 2) - 6, 23, 13, RoomType.Spawn) { isExplored = true },
                                      new Room((consoleX / 4) - 11, (consoleY / 4) - 6, 23, 13, RoomType.Mather) { isExplored = false }};

        List<Room> manual_rooms5 = new() { new Room((consoleX / 2) - 11, (consoleY / 2) - 6, 23, 13, RoomType.Spawn) { isExplored = true },
                                      new Room((consoleX / 4) - 11, (consoleY / 4) - 6, 23, 13, RoomType.Mather) { isExplored = false }};

        List<List<Room>> level_prefs = new() { manual_rooms1, manual_rooms2, manual_rooms3, manual_rooms4, manual_rooms5 };

        foreach (List<Room> manual_rooms in level_prefs)
        {
            //задаём карту
            Map map = new(consoleX, consoleY, edge, manual_rooms);

            //спавним игрока
            Player player = new Player(Race.Human, ((2 * map.rooms[0].x) + map.rooms[0].wigth) / 2, ((2 * map.rooms[0].y) + map.rooms[0].height) / 2);
            map.AddChange(new(player.x, player.y, CellID.Player));

            //генерируем карту
            map.Create(player);

            //выводим первоначальную карту и спавним игрока
            map.Update();

            //процесс игры
            while (true)
            {
                player.Advenchuring(map);
            }
        }
    }

    //считывание эффектов с файла
    public static List<Effect> ReadEffects(string filename)
    {
        StreamReader sr = new StreamReader(filename);
        List<Effect> effects = new List<Effect>();

        string line;
        // Read and display lines from the file until the end of
        // the file is reached.
        while ((line = sr.ReadLine()) != null)
        {
            string[] effectArray = line.Split("|");

            if (effectArray.Length == 4)
            {
                PermanentEffect effect = new PermanentEffect(
                    Convert.ToInt32(effectArray[0]),
                    effectArray[1],
                    Convert.ToInt32(effectArray[2]),
                    effectArray[3]
                );
                effects.Add(effect);
            }
        }

        return effects;
    }
    //считывание артов
    public static List<Artefact> ReadArtefacts(string filename)
    {
        StreamReader sr = new StreamReader(filename);
        List<Artefact> artefacts = new List<Artefact>();

        List<Effect> effects = ReadEffects("Item_Pack/Const_Effects.txt");

        string line;
        // Read and display lines from the file until the end of
        // the file is reached.
        while ((line = sr.ReadLine()) != null)
        {
            string[] artLine = line.Split("|");

            if (artLine.Length == 6)
            {
                Artefact art = new Artefact(
                    Convert.ToInt32(artLine[0]),
                    Convert.ToInt32(artLine[1]),
                    artLine[2],
                    artLine[3],
                    Convert.ToInt32(artLine[4])
                );

                string[] artEffects = artLine[5].Split(",");

                for (int i = 0; i < artEffects.Length; i++)
                {
                    foreach (Effect effect in effects)
                    {
                        if (effect.Id_effect == Convert.ToInt32(artEffects[i]))
                        {
                            art.AddEffect(effect);
                            break;
                        }
                    }
                }

                artefacts.Add(art);
            }
        }

        return artefacts;
    }
}