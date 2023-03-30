using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueMath.Item_Pack
{

    internal class Health_Cons : Consumable
    {
       // Random rnd = new Random();
        public int max_health_stack;
        public int cur_health_stack;
        
        public Health_Cons(int price, int max_stack, string name) : base(price, max_stack, name)
        {
            this.cur_health_stack = 1;
            this.max_health_stack = max_stack;
            name = "вкусняха";
        }

        public override void Use_Cons(Character player)
        {
            if (cur_health_stack > 0 && player.max_hp != player.cur_hp)
            {
                cur_health_stack--;
                player.cur_hp = player.cur_hp + (player.max_hp / 3);
                if (player.cur_hp > player.max_hp)
                {
                    player.cur_hp = player.max_hp;
                }
                Console.WriteLine($"Вы покушали");

            }
            else if (cur_health_stack > 0 && player.max_hp == player.cur_hp)
            {
                Console.WriteLine($"На ваше удивление, вы сейчас сыты");
            }
            else if (cur_health_stack <= 0)
            {
                Console.WriteLine($"У вас нет еды");
            }
        }
        public override void Sell_Cons(Character player)
        {
            if (cur_health_stack > 0)
            {
                cur_health_stack--;
                player.wallet = player.wallet + 10;
                Console.WriteLine($"Вы отдали 1 вкусняху");

            }
            else
            {
                Console.WriteLine($"У вас нет еды");
            }
        }
        public override void Get_Cons(Character player)
        {
            if (cur_health_stack < max_health_stack)
            {
                cur_health_stack++;
                Console.WriteLine($"Вы нашли себе покушать");

            }
            else
            {
                Console.WriteLine($"Все карманы для еды забиты");
            }

        }
        /*
        public enum Print_Hp
        {
            "доширак" = 1
            "пельмешки" = 2
            "шавуху" = 3
            "что-то черное и липкое" = 4
            "гречу" = 5
            "обед препода" = 6
            "собачий корм" = 7
            "высококаларийнных тараканов" = 0
        }
        */
    }
}
