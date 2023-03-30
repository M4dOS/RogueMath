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
            List<Item> pockets = new List<Item>();
            Health_Cons hp_potions = new Health_Cons(10, 5, "food");
            Energy_Cons en_potions = new Energy_Cons(10, 3, "cofe");

            Artefact[] arts = new Artefact[]
            {
                new Art_Max_Stat_Hp(1, 25, "потрепанная тетрадь", "придаёт уверенность, особенно если ты туда что-то записал с лекций", 2, 50),
                new Art_Max_Stat_Hp(2, 15, "кусочек шпоры", "клочок бумаги из пенала, который ты забыл выбросить с прошлой контрольной", 1, 25),
                new Art_Max_Stat_Hp(3, 25, "аккуратная шпора", "пока её писал, ты уже всё выучил", 2, 50),
                new Art_Max_Stat_Hp(4, 50, "ключ от 738", "открывает дверь в клуб мазохистов", 2, 25),
                new Art_Max_Stat_Atk(5, 30, "учбник по высшей математики том 17", "ничего не понятно, но осуждаем", 2, 25),
                new Art_Max_Stat_Atk(6, 25, "сломанный будильник", "теперь ты выспишься только на том свете", 1, 15),
                //new Art_Max_Stat_Atk(7, 25, "сломанный будильник", "теперь ты выспишься только на том свете", 1, 15),
            };

            //Artefact[] bag_arts = new Artefact[bag.pockets_max];

            // arts[0] = Art_Max_Stat_Hp
            Inventory bag = new Inventory(6, 6,/* pockets,*/ hp_potions, en_potions);
            Artefact[] bag_art;


            List<Item> items = new List<Item>();

            for (int i = 0; i < 6; i++)
            {
                Console.WriteLine(arts[i].name);
                Console.WriteLine(arts[i].description);
                Console.WriteLine("");
            }
            Console.WriteLine("-------------------------------------------------------------------------------------------------");

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