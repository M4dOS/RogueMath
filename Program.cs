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
        const string version = "v1.6.2140 alpha";
        const string info = "RogueMath" + " " + version;

        //прописываем настройки консоли
        Console.SetWindowSize(consoleX, consoleY);
        if (isDebug) Console.SetBufferSize(consoleX, (consoleY + 1) * countBuferMaps);
        else Console.SetBufferSize(consoleX, consoleY);
        Console.CursorVisible = false;
        Console.Title = info;

        //задаём комнату(ы) вручную через список

        for(int i = 1; i<=5; i++)
        {
            //задаём карту
            Map map = new(consoleX, consoleY, edge);

            //генерируем карту
            map.Create(/*player*/);

            //спавним игрока
            int index = 0;
            foreach(Room room in map.rooms)
            {
                if(room.roomType == RoomType.Spawn) { index = map.rooms.IndexOf(room); room.isExplored = true ; break; }
            }
            Player player = new Player(Race.Human, map, index);
            
            map.AddEnemies(player);
            map.AddChange(new(player.x, player.y, CellID.Player));

            //выводим первоначальную карту и спавним игрока
            map.Update();

            //процесс игры
            while (true)
            {
                player.Advenchuring(map);
            }
        }
    }

 /*   //считывание эффектов с файла
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
    }*/
}