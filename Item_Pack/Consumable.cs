namespace RogueMath.Item_Pack
{
    internal class Consumable : Item
    {
        public int max_stack { get; set; }
        public Consumable(int price, int max_stack, string name) : base(price, name)
        {
            this.max_stack = max_stack;
        }
        public virtual void Use_Cons(Player player) { }
        public virtual void Sell_Cons(Player player) { }
        public virtual void Get_Cons(Player player) { }
        public virtual void Buy_Cons(Player player) { }
    }
}
