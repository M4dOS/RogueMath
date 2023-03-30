using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueMath.Item_Pack;

namespace RogueMath
{
    internal class Inventory
    {
        public int pockets_max;
        public int pockets_free;
        public Artefact[] Artefacts { get; set; }
        //public List<Artefact> pockets;
        public Health_Cons hp_potions;
        public Energy_Cons en_potions;

        public Inventory(int pockets_max, int pockets_free, /*List<Artefact> pockets,*/ Health_Cons hp_potions, Energy_Cons en_potions)
        {
            this.pockets_max = pockets_max;
            this.pockets_free = pockets_max;
            this.hp_potions = hp_potions;
            //this.pockets = pockets;
            this.hp_potions = hp_potions;
            this.en_potions = en_potions;
            Artefact[] bag_art = new Artefact[pockets_max];
        }

        void Get_Art(Artefact art, Character c)
        {
            for (int i = 0; i < this.pockets_free; i++)
            {

            }
        }
        public void Show_Inventory(List<Item> bag)
        {
            Console.WriteLine($"Кол-во вкуснях: " + hp_potions.cur_health_stack);
            Console.WriteLine($"Кол-во кофе: " + en_potions.cur_energy_stack);
            //Console.WriteLine($"Оставшаяся стипендия: " + "0");
            Console.WriteLine($"Артефакты: ");
            for (int i =1; i < pockets_max + 1; i++)
            {
                Console.WriteLine(i+ $")__________");
            }
        }
        /*
        public void Equip_art(Artefact art)
        { 
            if (pockets_free < pockets_max)
            {
                /*for (int i = 0; i <= pockets_ocupaid; i++)
                {

                }
                pockets.Add(art);
                pockets_free = pockets.Count;
            }
            else { Console.WriteLine("Недостаточно места в инвенторе."); }

        }

        public void Sell_art(Artefact art) { }
        */
    }
}
