using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueMath.Item_Pack
{
    internal class Art_Max_Stat_Hp : Art_Max_Stat
    {
        public Art_Max_Stat_Hp(int id_art, int price, string name, string description, int quality, int add_stat) : base(id_art, price, name, description, quality, add_stat)
        {
        }
        public void Use_Art(Art_Max_Stat_Hp art, Character c)
        {
            c.max_hp = c.max_hp + art.add_stat;
        }
        public void Sell_Art(Art_Max_Stat_Hp art, Character c)
        {
            Console.WriteLine("Вы уверены, что хотите продать " + art.name + " ?");
            Console.WriteLine("1 - да      2 - нет ");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    if (c.max_hp - art.add_stat > 0)
                    {
                        c.max_hp = c.max_hp - art.add_stat;
                    }
                    else
                    {
                        c.max_hp = 1;
                    }
                    Console.WriteLine("Вы продали " + art.name + " и получили $" + art.price);
                    break;
                case 2:
                    Console.WriteLine("Вы разочаровали торговца :(");
                    break;
                default:
                    break;
            }
        }
    }
}
