using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueMath.Item_Pack
{
    public abstract class Item
    {
        public int price { get; set; }
        public string name { get; set; }

         public Item( int price, string name)
        {
            this.price = price;
            this.name = name;
        }
    }
}
