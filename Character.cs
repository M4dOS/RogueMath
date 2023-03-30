using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueMath
{
    public class Character
    {
        public int max_hp { get; set; }
        public int cur_hp { get; set; }
        public int cur_energy { get; set; }
        public int max_energy { get; set; }
        public int atk { get; set; }
        protected int _lvl { get; set; }
        protected short x;
        protected short y;
        public int _exp { get; set; }
        public int wallet;

        private Inventory inventory;

        public Character(int max_hp, int energy, int atk, int lvl, int exp)
        {
            this.max_hp = max_hp;
            this.cur_hp = max_hp -25;
            this.max_energy = energy;
            this.cur_energy = energy - 100;
            this.atk = atk;
            this._lvl = lvl;
            this._exp = exp;
            this.wallet = 0;
            this.inventory = new Inventory(
                25,
                25,
                new Item_Pack.Health_Cons(25, 10, "heal"),
                new Item_Pack.Energy_Cons(25, 10, "Energy")
            );
        }

        public void Status(Character c)
        {
            Console.WriteLine($"Stats:   hp " + c.cur_hp + "|" + c.max_hp + "    energy" + c.cur_energy + "|" + c.max_energy + "     atk " + c.atk + "      $ " +c.wallet);
        }
    }
}
