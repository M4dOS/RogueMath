using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueMath.Item_Pack;
using System.Numerics;

namespace RogueMath
{
    internal class Program
    {
        static void Main(string[] args)
        {
            InventoryTestPlus();
        }
        //считывание эффектов с файла
        public static List<Effect> ReadEffects(String filename)
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
        public static List<Artefact> ReadArtefacts(String filename)
        {
            StreamReader sr = new StreamReader(filename);
            List<Artefact> artefacts = new List<Artefact>();

            List<Effect> effects = ReadEffects("Const_effects.txt");

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



        //это для теста функционала (CharacterTest), ничего полезного нет
        public static void InventoryTest()
        {
            CharacterTest player = new CharacterTest(100, 300, 10, 1, 10);
            Health_Cons_Test hp_potions = new Health_Cons_Test(10, 5, "food");
            Energy_Cons_Test en_potions = new Energy_Cons_Test(10, 3, "cofe");
            List<Item> items = new List<Item>();
            List<Artefact> arts_equiped = new List<Artefact>();
            InventoryTest bag = new InventoryTest(6, hp_potions, en_potions, arts_equiped);


            List<Effect> effects = ReadEffects("Const_Effects.txt");
            List<Artefact> all_art = ReadArtefacts("Arts.txt");

            /*
            for (int i =0; i < effects.Count; i++)
            {
                Console.WriteLine(effects[i].Name_effect);
            }

            Console.WriteLine("-------------------------------------------------------------------------------------------------");

            for (int i = 0; i < all_arts.Count; i++)
            {
                Console.WriteLine(all_arts[i].name);
                Console.WriteLine(all_arts[i].description);
                for (int j = 0; j < all_arts[i].Effects.Count; j++)
                {
                    Console.WriteLine(all_arts[i].Effects[j].Name_effect);
                }
                Console.WriteLine("");
            }
            */

            player.Status(player);
            bag.Show_Inventory(items);

            var art_1_pool = all_art.Where(art => art.quality_art == 1).ToList();
            var art_2_pool = all_art.Where(art => art.quality_art == 2).ToList();
            var art_3_pool = all_art.Where(art => art.quality_art == 3).ToList();
            var art_4_pool = all_art.Where(art => art.quality_art == 4).ToList();
            var art_5_pool = all_art.Where(art => art.quality_art == 5).ToList();


            Artefact rand_1 = bag.RandArt(bag.arts_equiped, art_1_pool);
            bag.AddArt(rand_1, player);
            Artefact rand_2 = bag.RandArt(bag.arts_equiped, art_2_pool);
            bag.AddArt(rand_2, player);
            Artefact rand_3 = bag.RandArt(bag.arts_equiped, art_3_pool);
            bag.AddArt(rand_3, player);
            Artefact rand_4 = bag.RandArt(bag.arts_equiped, all_art);
            bag.AddArt(rand_4, player);


            Console.WriteLine("---get----------------------------------------------------------------------------------------------");

            player.Status(player);
            bag.Show_Inventory(items);

            bag.RemoveArt(rand_3, player);

            Console.WriteLine("---remove----------------------------------------------------------------------------------------------");

            Console.WriteLine("1-use hp, 2-sell hp, 3-get hp");
            Console.WriteLine("4-use en, 5-sell en, 6-get en");
            while (true)
            {
                player.Status(player);
                Console.WriteLine("Инвентарь:");
                bag.Show_Inventory(items);
                int m = Convert.ToInt32(Console.ReadLine());
                switch (m)
                {
                    case 1:
                        bag.hp_potions.Use_Cons(player);
                        break;
                    case 2:
                        bag.hp_potions.Sell_Cons(player);
                        break;
                    case 3:
                        bag.hp_potions.Get_Cons(player);
                        break;
                    case 4:
                        bag.en_potions.Use_Cons(player);
                        break;
                    case 5:
                        bag.en_potions.Sell_Cons(player);
                        break;
                    case 6:
                        bag.en_potions.Get_Cons(player);
                        break;
                    case 7:
                        bag.AddArt_Random(player, arts_equiped, all_art);
                        break;
                    case 8:
                        bag.SellArt(rand_1, player);
                        break;
                    case 9:
                        bag.RemoveSelectedArt(bag.arts_equiped, player);
                        break;
                    default:
                        break;
                }
                Console.WriteLine("____________________________________________");
            }


        }


        //новый тест, с Player
        public static void InventoryTestPlus()
        {
            Player p = new Player(0, 0);
            p._hp = 50;
            p._maxHp = 100;
            p._energy = 25;
            p._maxEnergy = 100;
            p._atk = 10;
            
            Health_Cons hp_potions = new Health_Cons(15, 5, "вкусняхи");
            Energy_Cons en_potions = new Energy_Cons(10, 5, "кофе");
            List<Item> items = new List<Item>();
            List<Artefact> arts_equiped = new List<Artefact>();


            List<Effect> effects = ReadEffects("Const_Effects.txt");
            List<Artefact> all_art = ReadArtefacts("Arts.txt");
            
            //пул предметов нужен для более удачного рандома: он создаёт новые списки исходя из качества предметов, где 1 - плохо, 4 - имба
            //но реализовать это я сейчас не успею

            //poor - отстойные и средние арты: могут быть наградой за победу монстра/за зачистку комнаты, сундука, первых этажей
            var poor_pool = all_art.Where(art => art.quality_art == 1 || art.quality_art == 2).ToList(); 
            //good - средние и хорошие: для магаза и поздних этажей (на продажу выставить 2-4 предмета, т.к. таких предметов всего 10)
            var good_pool = all_art.Where(art => art.quality_art == 2 || art.quality_art == 3).ToList();
            //boss - эксклюзив, выпадает с босса
            var boss_pool = all_art.Where(art => art.quality_art == 4).ToList();


            Inventory bag = new Inventory(6, hp_potions, en_potions, arts_equiped);

            Console.WriteLine("1-use hp, 2-sell hp, 3-get hp");
            Console.WriteLine("4-use en, 5-sell en, 6-get en");
            Console.WriteLine("7-get art, 8-read art, 9-sell art");

            while (true)
            {
                Console.WriteLine("-------------------------------------------------------------------------------------------------");
                Console.WriteLine("hp " + p._hp + "/" + p._maxHp + "   en " + p._energy + "/" + p._maxEnergy + "   atk " + p._atk);
                bag.Show_Inventory(items);

                int m = Convert.ToInt32(Console.ReadLine());
                switch (m)
                {
                    case 1:
                        hp_potions.Use_Cons(p);
                        break;
                    case 2:
                        hp_potions.Sell_Cons(p);
                        break;
                    case 3:
                        hp_potions.Get_Cons(p);
                        break;
                    case 4:
                        en_potions.Use_Cons(p);
                        break;
                    case 5:
                        en_potions.Sell_Cons(p);
                        break;
                    case 6:
                        en_potions.Get_Cons(p);
                        break;
                    
                case 7:
                    bag.AddArt_Random(p, arts_equiped, all_art);
                    break;
                    
                case 8:
                    bag.LookArt(items, arts_equiped, p);
                    break;
                    
                case 9:
                    bag.RemoveSelectedArt(bag.arts_equiped, p);
                    break;
                    
                    default:
                        break;

                }
            }
        }
    }
}