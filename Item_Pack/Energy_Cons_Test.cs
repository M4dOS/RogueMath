using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueMath.Item_Pack
{
    internal class Energy_Cons_Test : ConsumableTest
    {
        public int max_energy_stack;
        public int cur_energy_stack;

        public Energy_Cons_Test(int price, int max_stack, string name) : base(price, max_stack, name)
        {
            this.cur_energy_stack = 1;
            this.max_energy_stack = max_stack;
            name = "энергия";
        }

        public override void Use_Cons(CharacterTest player)
        {
            if (cur_energy_stack > 0 && player.max_energy != player.cur_energy)
            {
                cur_energy_stack--;
                player.cur_energy = player.cur_energy + (player.max_energy / 3);
                if (player.cur_energy > player.max_energy)
                {
                    player.cur_energy = player.max_energy;
                }
                Console.WriteLine($"Вы восполнили силы");

            }
            else if (cur_energy_stack > 0 && player.max_energy == player.cur_energy)
            {
                Console.WriteLine($"На ваше удивление, вы переполнены энергией");
            }
            else if (cur_energy_stack <= 0)
            {
                Console.WriteLine($"У вас нет кофе");
            }
        }
        public override void Sell_Cons(CharacterTest player)
        {
            if (cur_energy_stack > 0)
            {
                cur_energy_stack--;
                player.wallet = player.wallet + 10;
                Console.WriteLine($"Вы отдали 1 кофе");

            }
            else
            {
                Console.WriteLine($"У вас нет кофе");
            }
        }
        public override void Get_Cons(CharacterTest player)
        {
            if (cur_energy_stack < max_energy_stack)
            {
                cur_energy_stack++;
                Console.WriteLine($"Вы нашли свежеваренный кофе");

            }
            else
            {
                Console.WriteLine($"Все кружки для кофе заняты");
            }

        }
    }
}
