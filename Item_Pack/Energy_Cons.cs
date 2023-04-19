using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueMath.Item_Pack
{
    internal class Energy_Cons : Consumable
    {
        public int max_energy_stack;
        public int cur_energy_stack;

        public Energy_Cons(int price, int max_stack, string name) : base(price, max_stack, name)
        {
            this.cur_energy_stack = 1;
            this.max_energy_stack = max_stack;
            name = "энергия";
        }

        public void Use_Cons(Map map,Player player)
        {
            if (cur_energy_stack > 0 && player._maxEnergy != player._energy)
            {
                cur_energy_stack--;
                player._energy = player._energy + (player._maxEnergy / 3);
                if (player._energy > player._maxEnergy)
                {
                    player._energy = player._maxEnergy;
                }
                player.inventory.Notification($"Вы восполнили силы", map);

            }
            else if (cur_energy_stack > 0 && player._maxEnergy == player._energy)
            {
                player.inventory.Notification($"На ваше удивление, вы переполнены энергией", map);
            }
            else if (cur_energy_stack <= 0)
            {
                player.inventory.Notification($"У вас нет кофе", map);
            }
        }
        public void Sell_Cons(Map map, Player player)
        {
            if (cur_energy_stack > 0)
            {
                cur_energy_stack--;
                player._gold = player._gold + 10;
                player.inventory.Notification($"Вы отдали 1 кофе", map);

            }
            else
            {
                player.inventory.Notification($"У вас нет кофе", map);
            }
        }
        public void Get_Cons(Map map,Player player)
        {
            if (cur_energy_stack < max_energy_stack)
            {
                cur_energy_stack++;
                player.inventory.Notification($"Вы нашли свежеваренный кофе", map);

            }
            else
            {
                player.inventory.Notification($"Все кружки для кофе заняты", map);
            }

        }
    }
}
