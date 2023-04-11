using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueMath.Item_Pack
{
    internal class ConsumableTest : Item
    {
        public int max_stack { get; set; }
        public ConsumableTest(int price, int max_stack, string name) : base(price, name)
        {
            this.max_stack = max_stack;
        }
        public virtual void Use_Cons(CharacterTest player) { }
        public virtual void Sell_Cons(CharacterTest player) { }
        public virtual void Get_Cons(CharacterTest player) { }
        public virtual void Buy_Cons(CharacterTest player) { }
    }
}
