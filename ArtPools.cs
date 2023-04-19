using RogueMath.Item_Pack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueMath
{
    internal class ArtPools
    {/*
        //считывание эффектов с файла
        public static List<Effect> ReadEffects(string filename)
        {
            StreamReader sr = new StreamReader(filename);
            List<Effect> effects = new List<Effect>();

            string line;
            // Read and display lines from the file until the end of
            // the file is reached.
            while ((line = sr.ReadLine()) != null)
            {
                string[] effectArray = line.Split("|");

                if (effectArray.Length == 4)
                {
                    PermanentEffect effect = new PermanentEffect(
                        Convert.ToInt32(effectArray[0]),
                        effectArray[1],
                        Convert.ToInt32(effectArray[2]),
                        effectArray[3]
                    );
                    effects.Add(effect);
                }
            }

            return effects;
        }
        //считывание артов
        public static List<Artefact> ReadArtefacts(string filename)
        {
            StreamReader sr = new StreamReader(filename);
            List<Artefact> artefacts = new List<Artefact>();

            List<Effect> effects = ReadEffects("Item_Pack/Const_Effects.txt");

            string line;
            // Read and display lines from the file until the end of
            // the file is reached.
            while ((line = sr.ReadLine()) != null)
            {
                string[] artLine = line.Split("|");

                if (artLine.Length == 6)
                {
                    Artefact art = new Artefact(
                        Convert.ToInt32(artLine[0]),
                        Convert.ToInt32(artLine[1]),
                        artLine[2],
                        artLine[3],
                        Convert.ToInt32(artLine[4])
                    );

                    string[] artEffects = artLine[5].Split(",");

                    for (int i = 0; i < artEffects.Length; i++)
                    {
                        foreach (Effect effect in effects)
                        {
                            if (effect.Id_effect == Convert.ToInt32(artEffects[i]))
                            {
                                art.AddEffect(effect);
                                break;
                            }
                        }
                    }

                    artefacts.Add(art);
                }
            }

            return artefacts;
        }

        List<Effect> effects = ReadEffects("Item_Pack/Const_Effects.txt");
        List<Artefact> all_art = ReadArtefacts("Item_Pack/Arts.txt");

        //пул предметов нужен для более удачного рандома: он создаёт новые списки исходя из качества предметов, где 1 - плохо, 4 - имба

        //poor - отстойные и средние арты: могут быть наградой за победу монстра/за зачистку комнаты, сундука, первых этажей
        //List<Artefact> poor_pool = all_art.Where(art => art.quality_art == 1 || art.quality_art == 2).ToList();
        List<Artefact> poor_pool = new List<Artefact>();
        //good - средние и хорошие: для магаза и поздних этажей (на продажу выставить 2-4 предмета, т.к. таких предметов всего 10)
        var good_pool = all_art.Where(art => art.quality_art == 2 || art.quality_art == 3).ToList();
        //boss - эксклюзив, выпадает с босса
        var boss_pool = all_art.Where(art => art.quality_art == 4).ToList();*/
    }
}
