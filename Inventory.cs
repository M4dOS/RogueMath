using RogueMath.Item_Pack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// инвентарь будет иметь только игрок, не монстры
// магазин будет иметь класс инвенторя

namespace RogueMath
{
    internal class Inventory
    {
        Random rnd = new Random();

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
        public Artefact RandArt(List<Artefact> arts_equiped, List<Artefact> pool)
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
                Console.WriteLine("Не получилось взять ранд. артефакт");
                return null;
            }
        }

        //добавиьть рандомный арт
        public void AddArt_Random(Player p, List<Artefact> arts_equiped, List<Artefact> pool)
        {
            Artefact art = RandArt(arts_equiped, pool);
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

        //показ инвенторя (лучше потом красиво оформить)
        public void Show_Inventory(List<Item> bag)
        {
            Console.WriteLine($"Кол-во вкуснях: " + hp_potions.cur_health_stack);
            Console.WriteLine($"Кол-во кофе: " + en_potions.cur_energy_stack);
            Console.WriteLine($"Артефакты: ");
            if (pockets_max <= arts_equiped.Count)
            {
                for (int j = 0; j < pockets_max; j++)
                {
                    Console.WriteLine(j + 1 + $") " + arts_equiped[j].name_item);
                }
            }
            else if (pockets_max > arts_equiped.Count)
            {
                int rest_space = pockets_max;
                for (int j = 0; j < arts_equiped.Count; j++)
                {
                    rest_space--;
                    Console.WriteLine(j + 1 + $") " + arts_equiped[j].name_item);
                }
                for (int j = 0; j < rest_space; j++)
                    Console.WriteLine(j + 1 + arts_equiped.Count + $")__________");
            }
        }

        //выбор арта
        public Artefact SelectArt(List<Artefact> bag)
        {
            bool end = false;
            int pockets = bag.Count;
            int max_pockets = pockets_max;
            ConsoleKey key = Console.ReadKey().Key;
            int choise = Convert.ToInt32(key)-48;
            if (choise > max_pockets)
            {
                Console.WriteLine("Не получилось выбрать");
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
        public void RemoveSelectedArt(List<Artefact> bag, Player p)
        {
            Console.WriteLine("Выберете предмет");
            Artefact art = SelectArt(bag);
            RemoveArt(art, p);
        }

        //продать выбранный арт
        public void SellSelectedArt(List<Artefact> bag, Player p)
        {
            Artefact art = SelectArt(bag);
            Console.WriteLine("Вы точно хотите продать: " + art.name_item + "?");
            Console.WriteLine("Нажмите '0' для подтверждения");
            int choise = Convert.ToInt32(Console.ReadLine());
            if (choise == 1)
            {
                Console.WriteLine("Вы получили " + art.price_item + " стипы");
                SellArt(art, p);
            }
        }

        //печать инфы об арте (по хорошему, мне потом нужно добавить полезное описание (что арт прибавляет) )
        public void InfoArt(Artefact art)
        {
            Console.WriteLine(art.name_item);
            Console.WriteLine();
            Console.WriteLine(art.description_art);
            Console.WriteLine("Цена: " + art.price_item + " стипы");
        }

        //почитать описание арта из инвенторя(после выбора выходит из метода: можно потом доработать, чтобы выходил после нажатой клавиши выхода)
        public void LookArt(List<Item> items,List<Artefact> bag, Player p)
        {
           // Console.WriteLine("Для выхода из меню инвентaря выберете номер несуществующей ячейки инвенторя");
           // Show_Inventory(items);
            Console.WriteLine("Выберете предмет");
            Artefact art = SelectArt(bag);
            if (art != null)
            {
                Console.WriteLine();
                InfoArt(art);
                Console.WriteLine();
                Console.WriteLine("Для выхода нажмите лютую клавишу");
                Console.ReadKey();
            }
        }












        //считывание эффектов с файла
        List<Effect> ReadEffects(string filename)
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
        List<Artefact> ReadArtefacts(string filename)
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

        //новый тест, с Player
        public void InventoryTestPlus(Player player)
        {
            /*player._hp = 50;
            player._maxHp = 100;
            player._energy = 25;
            player._maxEnergy = 100;
            player._atk = 10;*/

            Health_Cons hp_potions = new Health_Cons(15, 5, "вкусняхи");
            Energy_Cons en_potions = new Energy_Cons(10, 5, "кофе");
            List<Item> items = new List<Item>();
            List<Artefact> arts_equiped = new List<Artefact>();


            List<Effect> effects = ReadEffects("Item_Pack/Const_Effects.txt");
            List<Artefact> all_art = ReadArtefacts("Item_Pack/Arts.txt");

            //пул предметов нужен для более удачного рандома: он создаёт новые списки исходя из качества предметов, где 1 - плохо, 4 - имба
            //но реализовать это я сейчас не успею

            //poor - отстойные и средние арты: могут быть наградой за победу монстра/за зачистку комнаты, сундука, первых этажей
            var poor_pool = all_art.Where(art => art.quality_art == 1 || art.quality_art == 2).ToList();
            //good - средние и хорошие: для магаза и поздних этажей (на продажу выставить 2-4 предмета, т.к. таких предметов всего 10)
            var good_pool = all_art.Where(art => art.quality_art == 2 || art.quality_art == 3).ToList();
            //boss - эксклюзив, выпадает с босса
            var boss_pool = all_art.Where(art => art.quality_art == 4).ToList();


            Inventory bag = new Inventory(6, hp_potions, en_potions, arts_equiped);

            

            bool cond = true;
            while (cond)
            {
                
                Console.WriteLine("1-use hp, 2-sell hp, 3-get hp");
                Console.WriteLine("4-use en, 5-sell en, 6-get en");
                Console.WriteLine("7-get art, 8-read art, 9-sell art");
                Console.WriteLine("0 - exit");
                Console.WriteLine("-------------------------------------------------------------------------------------------------");
                Console.WriteLine("hp " + player._hp + "/" + player._maxHp + "   en " + player._energy + "/" + player._maxEnergy + "   atk " + player._atk);
                bag.Show_Inventory(items);

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D1:
                        hp_potions.Use_Cons(player);
                        Thread.Sleep(3000);
                        break;
                    case ConsoleKey.D2:
                        hp_potions.Sell_Cons(player);
                        Thread.Sleep(3000);
                        break;
                    case ConsoleKey.D3:
                        hp_potions.Get_Cons(player);
                        Thread.Sleep(3000);
                        break;
                    case ConsoleKey.D4:
                        en_potions.Use_Cons(player);
                        Thread.Sleep(3000);
                        break;
                    case ConsoleKey.D5:
                        en_potions.Sell_Cons(player);
                        Thread.Sleep(3000);
                        break;
                    case ConsoleKey.D6:
                        en_potions.Get_Cons(player);
                        Thread.Sleep(3000);
                        break;

                    case ConsoleKey.D7:
                        bag.AddArt_Random(player, arts_equiped, all_art);
                        Thread.Sleep(3000);
                        break;

                    case ConsoleKey.D8:
                        bag.LookArt(items, arts_equiped, player);
                        Thread.Sleep(3000);
                        break;

                    case ConsoleKey.D9:
                        bag.RemoveSelectedArt(bag.arts_equiped, player);
                        Thread.Sleep(3000);
                        break;

                    case ConsoleKey.D0:
                        cond = false; break;

                    default:
                        
                        break;

                }
                Console.Clear();

            }
            

        }
    }
}
