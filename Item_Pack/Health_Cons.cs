namespace RogueMath.Item_Pack
{
    internal class Health_Cons : Consumable
    {
        public int max_health_stack;
        public int cur_health_stack;

        public Health_Cons(int price, int max_stack, string name) : base(price, max_stack, name)
        {
            cur_health_stack = 1;
            max_health_stack = max_stack;
        }

        public override void Use_Cons(Player player)
        {
            if (cur_health_stack > 0 && player._maxHp != player._hp)
            {
                cur_health_stack--;
                player._hp += (player._maxHp / 3);
                if (player._hp > player._maxHp)
                {
                    player._hp = player._maxHp;
                }
                Console.WriteLine($"Вы покушали");

            }
            else if (cur_health_stack > 0 && player._maxHp == player._hp)
            {
                Console.WriteLine($"На ваше удивление, вы сейчас сыты");
            }
            else if (cur_health_stack <= 0)
            {
                Console.WriteLine($"У вас нет еды");
            }
        }
        public override void Sell_Cons(Player player)
        {
            if (cur_health_stack > 0)
            {
                cur_health_stack--;
                player._gold += 10;
                Console.WriteLine($"Вы отдали 1 вкусняху");

            }
            else
            {
                Console.WriteLine($"У вас нет еды");
            }
        }
        public override void Get_Cons(Player player)
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
    }
}