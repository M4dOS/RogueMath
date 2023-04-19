using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueMath.Item_Pack
{
    internal class Health_Cons : Consumable
    {
        public int max_health_stack;
        public int cur_health_stack;

        public Health_Cons(int price, int max_stack, string name) : base(price, max_stack, name)
        {
            this.cur_health_stack = 1;
            this.max_health_stack = max_stack;
            name = "вкусняха";
        }

        public void Use_Cons(Map map, Player player)
        {
            if (cur_health_stack > 0 && player._maxHp != player._hp)
            {
                cur_health_stack--;
                player._hp = player._hp + (player._maxHp / 3);
                if (player._hp > player._maxHp)
                {
                    player._hp = player._maxHp;
                }
                player.inventory.Notification($"Вы покушали", map );

            }
            else if (cur_health_stack > 0 && player._maxHp == player._hp)
            {
                player.inventory.Notification($"На ваше удивление, вы сейчас сыты", map);
            }
            else if (cur_health_stack <= 0)
            {
                player.inventory.Notification($"У вас нет еды", map);
            }
        }
        public void Sell_Cons(Map map, Player player)
        {
            if (cur_health_stack > 0)
            {
                cur_health_stack--;
                player._gold = player._gold + 10;
                player.inventory.Notification($"Вы отдали 1 вкусняху", map);

            }
            else
            {
                player.inventory.Notification($"У вас нет еды", map);
            }
        }
        public void Get_Cons(Map map, Player player)
        {
            if (cur_health_stack < max_health_stack)
            {
                cur_health_stack++;
                player.inventory.Notification($"Вы нашли себе покушать", map);

            }
            else
            {
                player.inventory.Notification($"Все карманы для еды забиты", map);
            }

        }
    }
}