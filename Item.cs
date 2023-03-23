using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueMath
{
    public class Item
    {
        int id_item;
        int price;
        protected string Name;
        protected string Description;

        public Item(int id_item, int price, string Name, string Description)
        {
            this.id_item = id_item;
            this.price = price;
            this.Name = Name;
            this.Description = Description;
        }
    }
}
