using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueMath.Item_Pack
{
    internal class Artefact : Item
    {
        public int quality { get; set; }
        public int id_art { get; set; }
        public string description { get; set; }
        public string stat_derscription { get; set; }
        public List<Effect> Effects { get; set; }

        public Artefact(int id_art, int price, string name, string description, int quality) : base( price, name)
        {
            this.id_art = id_art;
            this.quality = quality;
            this.description = description;
            Effects = new List<Effect>();
        }
        public void AddEffect(Effect effect)
        {
            Effects.Add(effect);
        }
        public void RemoveEffect(Effect effect)
        {
            Effects.Remove(effect);
        }
    }
}