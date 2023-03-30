using RogueMath.Item_Pack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueMath
{
    internal class Art_Max_Stat : Artefact
    {
        public int add_stat;
        public Art_Max_Stat(int id_art, int price, string name, string description, int quality, int add_stat) : base(id_art, price, name, description, quality)
        {
            this.add_stat = add_stat;
        }

    }
}
