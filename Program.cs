using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueMath.Item_Pack;

namespace RogueMath
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Character player = new Character(100, 300, 10, 1, 10);
            //List<Item> pockets = new List<Item>();
            Health_Cons hp_potions = new Health_Cons(10, 5, "food");
            Energy_Cons en_potions = new Energy_Cons(10, 3, "cofe");
            List<Item> items = new List<Item>();
            List<Artefact> arts_equiped = new List<Artefact>();
            /*
            {
                new Art_Max_Stat_Hp(1, 25, "потрепанная тетрадь", "придаёт уверенность, особенно если ты туда что-то записал с лекций", 2, 50),
                new Art_Max_Stat_Hp(2, 15, "кусочек шпоры", "клочок бумаги из пенала, который ты забыл выбросить с прошлой контрольной", 1, 25),
                new Art_Max_Stat_Hp(3, 25, "аккуратная шпора", "пока её писал, ты уже всё выучил", 2, 50),
                new Art_Max_Stat_Hp(4, 50, "ключ от 738", "открывает дверь в клуб мазохистов", 2, 25),
                new Art_Max_Stat_Atk(5, 30, "учбник по высшей математики том 17", "ничего не понятно, но осуждаем", 2, 25),
                new Art_Max_Stat_Atk(6, 25, "сломанный будильник", "теперь ты выспишься только на том свете", 1, 15),
                //new Art_Max_Stat_Atk(7, 25, "сломанный будильник", "теперь ты выспишься только на том свете", 1, 15),
            };
            */
            Inventory bag = new Inventory(6, hp_potions, en_potions, arts_equiped);

            List<Effect> eff = new List<Effect>();
            PermanentEffect hp_lil = new PermanentEffect(1, "lil hp+", 25, "hp");
            PermanentEffect hp_third = new PermanentEffect(2, "norm hp+", 33, "hp");
            PermanentEffect hp_half = new PermanentEffect(3, "good hp+", 50, "hp");
            PermanentEffect hp_sev_five = new PermanentEffect(4, "a lot hp+", 75, "hp");
            PermanentEffect hp_double = new PermanentEffect(5, "double hp+", 100, "hp");
            PermanentEffect atk_lil = new PermanentEffect(6, "lil atk+", 25, "atk");
            PermanentEffect atk_third = new PermanentEffect(7, "norm atk+", 33, "atk");
            PermanentEffect atk_half = new PermanentEffect(8, "good atk+", 50, "atk");
            PermanentEffect atk_sev_five = new PermanentEffect(9, "a lot atk+", 75, "atk");
            PermanentEffect atk_double = new PermanentEffect(10, "double atk+", 100, "atk");
            eff.Add(hp_lil);
            eff.Add(hp_third);
            eff.Add(hp_half);
            eff.Add(hp_sev_five);
            eff.Add(hp_double);
            eff.Add(atk_lil);
            eff.Add(atk_third);
            eff.Add(atk_half);
            eff.Add(atk_sev_five);
            eff.Add(atk_double);

            /*
            for (int i = 0; i < eff.Count; i++)
            {
                Console.WriteLine( eff[i].Id_effect + " " + eff[i].Name_effect);
            }
            */
            player.Status(player);
            bag.Show_Inventory(items);

            //Console.WriteLine("-------------------------------------------------------------------------------------------------");

            List<Artefact> all_art = new List<Artefact>();

            Artefact notebook = new Artefact(1, 30, "потрепаная тетрадь", "+ хп | не зря сходил на лекцию", 2);
            notebook.AddEffect(hp_third);
            Artefact paper_sheet = new Artefact(2, 10, "кусочек шпоры", "+ атк | забыл выбросить", 1);
            paper_sheet.AddEffect(atk_lil);
            Artefact nice_shpora = new Artefact(3, 30, "аккуратная шпора", "+ атк + хп| пока её писал, ты уже всё выучил", 3);
            nice_shpora.AddEffect(atk_half);
            nice_shpora.AddEffect(hp_lil);
            Artefact key = new Artefact(4, 50, "ключ от 738", "+ хп +дорого продаётся| открывает дверь в клуб мазохистов", 2);
            key.AddEffect(hp_lil);
            Artefact studybook = new Artefact(5, 30, "учбник по высшей математики том 17", "+атк | ничего не понятно, но осуждаем", 3);
            studybook.AddEffect(atk_third);
            Artefact alarm_clock = new Artefact(6, 10, "сломанный будильник", "+хп | теперь ты выспишься только на том свете", 1);
            alarm_clock.AddEffect(hp_lil);

            all_art.Add(notebook);
            all_art.Add(paper_sheet);
            all_art.Add(nice_shpora);
            all_art.Add(key);
            all_art.Add(studybook);
            all_art.Add(alarm_clock);

            //пулы артефактов - списки, содержащие предметы одинакового качества
            var art_1_pool = all_art.Where(art => art.quality == 1).ToList();
            var art_2_pool = all_art.Where(art => art.quality == 2).ToList();
            var art_3_pool = all_art.Where(art => art.quality == 3).ToList();
            var art_4_pool = all_art.Where(art => art.quality == 4).ToList();
            var art_5_pool = all_art.Where(art => art.quality == 5).ToList();


            Artefact rand_1 = bag.RandArt(bag.arts_equiped, art_1_pool);
            bag.AddArt(rand_1, player);
            Artefact rand_2 = bag.RandArt(bag.arts_equiped, art_2_pool);
            bag.AddArt(rand_2, player);
            Artefact rand_3 = bag.RandArt(bag.arts_equiped, art_3_pool);
            bag.AddArt(rand_3, player);

            /*
            for (int i = 0; i < all_art.Count; i++)
            {
                Console.WriteLine(i+1 + ". " + all_art[i].name);
                Console.WriteLine("   " + all_art[i].description);
            }
            */
            // Console.WriteLine("-------------------------------------------------------------------------------------------------");

            Console.WriteLine("---get----------------------------------------------------------------------------------------------");

            player.Status(player);
            bag.Show_Inventory(items);

           //bag.RemoveArt(nice_shpora, player);

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
                    default:
                        break;
                }
                Console.WriteLine("____________________________________________");
            }
        }
    }
}