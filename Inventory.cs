using RogueMath.Item_Pack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using static System.Net.Mime.MediaTypeNames;


// инвентарь будет иметь только игрок, не монстры
// магазин будет иметь класс инвенторя

namespace RogueMath
{
    internal class Inventory
    {
        public int pockets_max;
        public int pockets_free;
        public Health_Cons hp_potions;
        public Energy_Cons en_potions;
        public List<Artefact> arts_equiped;
        public Inventory(int pockets_max, Health_Cons hp_potions, Energy_Cons en_potions, List<Artefact> arts_equiped)
        {
            this.pockets_max = pockets_max;
            this.hp_potions = hp_potions;
            this.hp_potions = hp_potions;
            this.en_potions = en_potions;
            this.arts_equiped = arts_equiped;
        }

        //добавить арт в инвентарь
        public void AddArt(Artefact art, Player p)
        {
            if (arts_equiped.Count < pockets_max)
            {
                arts_equiped.Add(art);
                foreach (Effect effect in art.effects_art)
                {
                    if (effect is PermanentEffect)
                    {
                        PermanentEffect effect2 = (PermanentEffect)effect;
                        if (effect2.Stat_type == "hp")
                        {
                            p._maxHp += effect.Value_effect;
                        }
                        if (effect2.Stat_type == "atk")
                        {
                            p._atk += effect.Value_effect;
                        }
                        if (effect2.Stat_type == "en")
                        {
                            p._maxEnergy += effect.Value_effect;
                        }
                    }
                }
            }
            else Console.WriteLine("не получилось взять предмет");
        }

        //выбрать рандомный арт из предложеного списка(pool), которого нет в инвентаре (arts_equiped)
        public Artefact RandArt(Map map, List<Artefact> arts_equiped, List<Artefact> pool)
        {
            var equipe_art = pool.Except(arts_equiped);
            if (equipe_art.Any())
            {
                Random random = new Random();
                var rand_art = equipe_art.ElementAt(random.Next(equipe_art.Count()));

                return rand_art;
            }
            else
            {
                this.Notification("Не получилось взять ранд. артефакт", map);
                return null;
            }
        }

        //добавиьть рандомный арт
        public void AddArt_Random(Map map, Player p, List<Artefact> arts_equiped, List<Artefact> pool)
        {
            Artefact art = RandArt(map, arts_equiped, pool);
            AddArt(art, p);
        }

        //убрать арт
        public void RemoveArt(Artefact art, Player p)
        {
            if(art == null) Console.WriteLine("Не удалось продать предмет");
            else
            {
                for (int i = 0; i < art.effects_art.Count; i++)
                {
                    foreach (Effect effect in art.effects_art)
                    {
                        if (effect is PermanentEffect)
                        {
                            PermanentEffect effect2 = (PermanentEffect)effect;
                            if (effect2.Stat_type == "hp")
                            {
                                p._maxHp -= effect.Value_effect;
                            }
                            if (effect2.Stat_type == "atk")
                            {
                                p._atk -= effect.Value_effect;
                            }
                            if (effect2.Stat_type == "en")
                            {
                                p._maxEnergy -= effect.Value_effect;
                            }
                        }
                    }
                }
                arts_equiped.Remove(art);
            }
        }

        //продать арт
        public void SellArt(Artefact art, Player p)
        {
            p._gold += art.price_item;
            RemoveArt(art, p);
        }

        //купить арт
        public void BuyArt(Artefact art, Player p)
        {
            if (p._gold >= art.price_item)
            {
                p._gold -= art.price_item;
                AddArt(art, p);
            }
            else Console.WriteLine("Вам не хватает стипы");
        }

        public Artefact SelectArt(List<Artefact> bag, Map map)
        {
            bool end = false;
            int pockets = bag.Count;
            int max_pockets = pockets_max;
            ConsoleKey key = Console.ReadKey(false).Key;
            int choise = Convert.ToInt32(key)-48;
            if (choise > max_pockets)
            {
                this.Notification("Не получилось выбрать", map);
                end = true;
                return null;
            }
            else
            {
                if (choise <= bag.Count )  return bag[choise - 1];
                else return null;
            }

        }

        //удалить выбранный арт (без подтверждения)
        public void RemoveSelectedArt(List<Artefact> bag, Player p, Map map)
        {
            this.Notification("Выберете предмет", map);
            Artefact art = SelectArt(bag, map);
            RemoveArt(art, p);
        }

        //продать выбранный арт
        public void SellSelectedArt(List<Artefact> bag, Player p, Map map)
        {
            Artefact art = SelectArt(bag, map);
            Console.WriteLine("Вы точно хотите продать: " + art.name_item + "?");
            Console.WriteLine("Нажмите '0' для подтверждения");
            int choise = Convert.ToInt32(Console.ReadLine());
            if (choise == 1)
            {
                Console.WriteLine("Вы получили " + art.price_item + " стипы");
                SellArt(art, p);
            }
        }

        //почитать описание арта из инвенторя(после выбора выходит из метода: можно потом доработать, чтобы выходил после нажатой клавиши выхода)
        public void LookArt(List<Artefact> bag, Player p, Map map)
        {
           // Console.WriteLine("Для выхода из меню инвентaря выберете номер несуществующей ячейки инвенторя");
           // Show_Inventory(items);
            this.Notification("Выберете предмет", map);
            Artefact art = SelectArt(bag, map);
            if (art != null)
            {
                Console.WriteLine();
                InfoArt(art, map);
                this.Notification("Для выхода нажмите любую клавишу", map);
                Console.ReadKey(false);
            }
        }

        //новый тест, с Player

        public void PrintAtCenter(string text, int aX, int bX, int y)
        {
            Console.SetCursorPosition(((aX + bX - text.Length)+1) / 2, y);
            if(text.Length>bX-aX) Console.WriteLine(text.Substring(bX - aX));
            else Console.WriteLine(text);
        }

        public void PrintAtFrames(string text, int aX, int bX, int y)
        {
            string[] words = text.Split(' ');
            Console.SetCursorPosition((aX + bX - text.Length) / 2, y);
            string wordLine = "";
            foreach(string str in words)
            {
                if (wordLine.Length + str.Length + 3 < bX - aX) wordLine += str + " ";
                else { PrintAtCenter(wordLine, aX+1, bX-1, Console.CursorTop); wordLine = str + " ";}
            }
            PrintAtCenter(wordLine, aX, bX, Console.CursorTop);
        }

        static List<CellInfo> notifBufer = new();
        static List<CellInfo> infoBufer = new();

        public int countOfStepsToClear=0;
        public void Notification(string text, Map map)
        {
            this.ClearNotification();
            countOfStepsToClear = 0;
            int maxX = map.maxX;
            int maxY = map.maxY;

            Console.SetCursorPosition((maxX - text.Length) / 2, maxY - 10);

            List<CellInfo> serve = new();

            int dotAX = (maxX - text.Length )/2-1-1;
            int dotAY = maxY - 10;
            int dotBX = (maxX + text.Length)/2+1;
            int dotBY = dotAY + 2;

            for (int x = dotAX; x <= dotBX; x++)
            {
                for (int y = dotAY; y <= dotBY; y++)
                {
                    serve.Add(map.cellMap[x, y]);

                    Console.SetCursorPosition(x, y);

                    if (x == dotAX && y == dotAY || x == dotBX && y == dotBY) //углы главной диагонали
                    {
                        Console.Write((char)CellID.MainVSpot);
                    }
                    else if (x == dotAX && y == dotBY || x == dotBX && y == dotAY) //углы побочной диагонали
                    {
                        Console.Write((char)CellID.SecondVSpot);
                    }
                    else if (x == dotAX && y < dotBY && y > dotAY || x == dotBX && y < dotBY && y > dotAY) //вертикальные стены
                    {
                        Console.Write((char)CellID.VWall);
                    }
                    else if (y == dotAY && x < dotBX && x > dotAX || y == dotBY && x < dotBX && x > dotAX)
                    {
                        Console.Write((char)CellID.HWall);
                    }
                    else
                    {
                        Console.Write((char)CellID.None);
                    }
                }
            }

            foreach (CellInfo cellInfo in serve) notifBufer.Add(cellInfo);

            Console.SetCursorPosition(dotAX+1+1, dotAY+1);
            Console.Write(text);
            
        }
        
        public  void ClearNotification()
        {
            countOfStepsToClear = 0;
            foreach (CellInfo cell in notifBufer)
            {
                Console.SetCursorPosition(cell.x, cell.y);
                Console.Write((char)cell.cellID);
            }
            notifBufer.Clear();
        }

        public static void CustomClear(List<CellInfo> changes)
        {
            foreach (CellInfo cell in changes)
            {
                Console.SetCursorPosition(cell.x, cell.y);
                Console.Write((char)cell.cellID);
            }
            changes.Clear();
        }

        //печать инфы об арте
        public void InfoArt(Artefact art, Map map)
        {
            int dotAX = map.maxX / 2 + 25;
            int dotAY = map.maxY / 2 - 10;
            int dotBX = map.maxX / 2 + 25 + 30;
            int dotBY = map.maxY / 2 + 10;

            for (int x = dotAX + 1; x <= dotBX; x++)
            {
                for (int y = dotAY; y <= dotBY; y++)
                {
                    infoBufer.Add(map.cellMap[x, y]);

                    Console.SetCursorPosition(x, y);

                    if ( x == dotBX && y == dotBY) //углы главной диагонали
                    {
                        Console.Write((char)CellID.SecondVSpot);
                    }
                    else if ( x == dotBX && y == dotAY) //углы побочной диагонали
                    {
                        Console.Write((char)CellID.MainVSpot);
                    }
                    else if (x == dotAX && y < dotBY && y > dotAY || x == dotBX && y < dotBY && y > dotAY) //вертикальные стены
                    {
                        Console.Write((char)CellID.VWall);
                    }
                    else if (y == dotAY && x < dotBX && x > dotAX || y == dotBY && x < dotBX && x > dotAX)
                    {
                        Console.Write((char)CellID.HWall);
                    }
                    else if (y != dotAY && x != dotAX || y != dotBY && x != dotAX)
                    {
                        Console.Write((char)CellID.None);
                    }
                }
            }

            Console.SetCursorPosition(dotAX + 1, dotAY + 1 + 1);

            PrintAtFrames(art.name_item, dotAX, dotBX, Console.CursorTop);

            Console.CursorTop = dotBY - 7;
            PrintAtFrames(art.description_art, dotAX, dotBX, Console.CursorTop);
            Console.CursorTop = dotBY - 2;
            PrintAtCenter("Цена: " + art.price_item + " стипы", dotAX, dotBX, Console.CursorTop);
        }

        private void ClearArea(int ax, int ay, int bx, int by)
        {
            for(int x = ax; x<= bx; x++)
            {
                for(int y = ay; y <= by; y++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(" ");
                }
            }
        }

        public void InventoryShow(Map map, Player player)
        {
            /*Console.Clear();*/ //ждёт красивую обёртку

            int dotAX = map.maxX / 2 - 25;
            int dotAY = map.maxY / 2 - 10;
            int dotBX = map.maxX / 2 + 25;
            int dotBY = map.maxY / 2 + 10;
            
            for(int x = dotAX; x <= dotBX; x++)
            {
                for(int y  = dotAY; y <= dotBY; y++)
                {
                    Console.SetCursorPosition(x,y);

                    if (x == dotAX && y == dotAY || x == dotBX  && y == dotBY ) //углы главной диагонали
                    {
                        Console.Write((char)CellID.MainVSpot);
                    }
                    else if (x == dotAX  && y == dotBY  || x == dotBX  && y == dotAY) //углы побочной диагонали
                    {
                        Console.Write((char)CellID.SecondVSpot);
                    }
                    else if (x == dotAX  && y < dotBY && y > dotAY  || x == dotBX  && y < dotBY  && y > dotAY ) //вертикальные стены
                    {
                        Console.Write((char)CellID.VWall);
                    }
                    else if (y == dotAY  && x < dotBX  && x > dotAX  || y == dotBY  && x < dotBX  && x > dotAX )
                    {
                        Console.Write((char)CellID.HWall);
                    }
                    else
                    {
                        Console.Write((char)CellID.None);
                    }
                }
            }

            Thread.Sleep(100);
            Console.SetCursorPosition(map.maxX / 2 - 25 + 1, map.maxY / 2 - 10 + 1);

            /*Health_Cons hp_potions = new Health_Cons(15, 5, "вкусняхи");
            Energy_Cons en_potions = new Energy_Cons(10, 5, "кофе");
            List<Item> items = new List<Item>();
            List<Artefact> arts_equiped = new List<Artefact>();*/

            Inventory bag = player.inventory;
                /*new Inventory(6, hp_potions, en_potions, arts_equiped);*/

            bool cond = true;
            while (cond)
            {
                Console.SetCursorPosition(map.maxX / 2 - 25 + 1, map.maxY / 2 - 10 + 1);

                Console.WriteLine();

                PrintAtCenter("1-use hp, 2-sell hp, 3-get hp", dotAX, dotBX, Console.CursorTop);
                PrintAtCenter("4-use en, 5-sell en, 6-get en", dotAX, dotBX, Console.CursorTop);
                PrintAtCenter("7-get art, 8-read art, 9-sell art", dotAX, dotBX, Console.CursorTop);
                PrintAtCenter("z - exit", dotAX, dotBX, Console.CursorTop);

                Console.WriteLine();

                PrintAtCenter($"Кол-во вкуснях: " + hp_potions.cur_health_stack , dotAX, dotBX, Console.CursorTop);
                PrintAtCenter($"Кол-во кофе: " + en_potions.cur_energy_stack, dotAX, dotBX, Console.CursorTop);

                Console.WriteLine();

                PrintAtCenter($"Артефакты: ", dotAX, dotBX, Console.CursorTop);
                if (pockets_max <= arts_equiped.Count)
                {
                    for (int j = 0; j < pockets_max; j++)
                    {
                        PrintAtCenter(j + 1 + $") " + arts_equiped[j].name_item, dotAX, dotBX, Console.CursorTop);
                    }
                }
                else if (pockets_max > arts_equiped.Count)
                {
                    int rest_space = pockets_max;
                    for (int j = 0; j < arts_equiped.Count; j++)
                    {
                        rest_space--;
                        PrintAtCenter(j + 1 + $") " + arts_equiped[j].name_item, dotAX, dotBX, Console.CursorTop);
                    }
                    for (int j = 0; j < rest_space; j++)
                        PrintAtCenter(j + 1 + arts_equiped.Count + $")__________", dotAX, dotBX, Console.CursorTop);
                }

                int rest = 100;

                switch (Console.ReadKey(false).Key)
                {
                    case ConsoleKey.D1:
                        hp_potions.Use_Cons(map, player);
                        Thread.Sleep(rest);
                        break;
                    case ConsoleKey.D2:
                        hp_potions.Sell_Cons(map, player);
                        Thread.Sleep(rest);
                        break;
                    case ConsoleKey.D3:
                        hp_potions.Get_Cons(map, player);
                        Thread.Sleep(rest);
                        break;
                    case ConsoleKey.D4:
                        en_potions.Use_Cons(map,player);
                        Thread.Sleep(rest);
                        break;
                    case ConsoleKey.D5:
                        en_potions.Sell_Cons(map,player);
                        Thread.Sleep(rest);
                        break;
                    case ConsoleKey.D6:
                        en_potions.Get_Cons(map,player);
                        Thread.Sleep(rest);
                        break;

                   case ConsoleKey.D7:
                        static List<Effect> ReadEffects(string filename)
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
                        static List<Artefact> ReadArtefacts(string filename)
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
                        List<Effect> effects = ReadEffects("Item_Pack/Const_Effects.txt");
                        List<Artefact> all_art = ReadArtefacts("Item_Pack/Arts.txt");
                        bag.AddArt_Random(map, player, arts_equiped, all_art);
                        Thread.Sleep(rest);
                        break;

                    case ConsoleKey.D8:
                        bag.LookArt(arts_equiped, player, map);
                        Thread.Sleep(rest);
                        CustomClear(infoBufer);
                        break;

                    case ConsoleKey.D9:
                        bag.RemoveSelectedArt(bag.arts_equiped, player, map);
                        Thread.Sleep(rest);
                        break;

                    case ConsoleKey.Z:
                        cond = false; break;

                    default:
                        
                        break;

                }
                ClearArea(dotAX+1, dotAY+1, dotBX-1, dotBY-1);
                ClearNotification();
                player.Stats(map);

            }
            map.PrintMap();
        }
    }
}
