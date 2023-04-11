using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueMath.Item_Pack
{
    public abstract class Item
    {
        public int price_item { get; set; }
        public string name_item { get; set; }
         public Item( int price, string name)
        {
            this.price_item = price;
            this.name_item = name;
        }
    }
}
