using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueMath.Item_Pack
{
    internal class Artefact : Item
    {
        private int quality;
        private int id_art;
        public string description;
        public string stat_derscription;

         public Artefact(int id_art, int price, string name,  string description, int quality) : base( price, name)
        {
            this.id_art = id_art;
            this.quality = quality;
            this.description = description;
        }
        //public virtual void Use_Art(Artefact art, Character c) { }
        //public virtual void Get_Art(Artefact art) { }
        //public virtual void Lose_Art(Artefact art) { }
        //public virtual void Sell_Art(Artefact art) { }
    }
}