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

//          id_item:
// 1-health
// 2-sp
// 3-gold
// 401, 402, 403... art
// 501, 502, 503... weapon
