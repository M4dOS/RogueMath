namespace RogueMath.Item_Pack
{
    internal class Energy_Cons : Consumable
    {
        public int max_energy_stack;
        public int cur_energy_stack;

        public Energy_Cons(int price, int max_stack, string name) : base(price, max_stack, name)
        {
            cur_energy_stack = 1;
            max_energy_stack = max_stack;
        }

        public override void Use_Cons(Player player)
        {
            if (cur_energy_stack > 0 && player._maxEnergy != player._energy)
            {
                cur_energy_stack--;
                player._energy += (player._maxEnergy / 3);
                if (player._energy > player._maxEnergy)
                {
                    player._energy = player._maxEnergy;
                }
                Console.WriteLine($"Вы восполнили силы");

            }
            else if (cur_energy_stack > 0 && player._maxEnergy == player._energy)
            {
                Console.WriteLine($"На ваше удивление, вы переполнены энергией");
            }
            else if (cur_energy_stack <= 0)
            {
                Console.WriteLine($"У вас нет кофе");
            }
        }
        public override void Sell_Cons(Player player)
        {
            if (cur_energy_stack > 0)
            {
                cur_energy_stack--;
                player._gold += 10;
                Console.WriteLine($"Вы отдали 1 кофе");

            }
            else
            {
                Console.WriteLine($"У вас нет кофе");
            }
        }
        public override void Get_Cons(Player player)
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
