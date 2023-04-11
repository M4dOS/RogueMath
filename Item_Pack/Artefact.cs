using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueMath.Item_Pack
{
    internal class Artefact : Item
    {
        public int quality_art { get; set; }
        public int id_art { get; set; }
        public string description_art { get; set; }
        public string stat_derscription { get; set; }
        public List<Effect> effects_art { get; set; }
        public Artefact(int id_art, int price, string name, string description, int quality) : base( price, name)
        {
            this.id_art = id_art;
            this.quality_art = quality;
            this.description_art = description;
            effects_art = new List<Effect>();
        }
        public void AddEffect(Effect effect)
        {
            effects_art.Add(effect);
        }
        public void RemoveEffect(Effect effect)
        {
            effects_art.Remove(effect);
        }
    }
}