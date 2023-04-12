namespace RogueMath.Item_Pack
{
    public abstract class Item
    {
        public int price_item { get; set; }
        public string name_item { get; set; }
        public Item(int price, string name)
        {
            price_item = price;
            name_item = name;
        }
    }
}
