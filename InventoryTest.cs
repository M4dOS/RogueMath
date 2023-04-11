using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using RogueMath.Item_Pack;

namespace RogueMath
{
    internal class InventoryTest
    {
        Random rnd = new Random();

        public int pockets_max;
        public int pockets_free;
        public Health_Cons_Test hp_potions;
        public Energy_Cons_Test en_potions;
        public List<Artefact> arts_equiped;
        public InventoryTest(int pockets_max, Health_Cons_Test hp_potions, Energy_Cons_Test en_potions, List<Artefact> arts_equiped)
        {
            this.pockets_max = pockets_max;
            this.hp_potions = hp_potions;
            this.hp_potions = hp_potions;
            this.en_potions = en_potions;
            this.arts_equiped = arts_equiped;
        }

        //добавить арт в инвентарь
        public void AddArt(Artefact art, CharacterTest c)
        {
            if (arts_equiped.Count < pockets_max)
            {
                arts_equiped.Add(art);
                foreach (Effect effect in art.effects_art)
                {
                    if (effect is PermanentEffect)
                    {
                        PermanentEffect effect2 = (PermanentEffect)effect;
                        if (effect2.Stat_type == "hp")
                        {
                            c.max_hp += effect.Value_effect;
                        }
                        if (effect2.Stat_type == "atk")
                        {
                            c.atk += effect.Value_effect;
                        }
                        if (effect2.Stat_type == "en")
                        {
                            c.max_energy += effect.Value_effect;
                        }
                    }
                }
            }
            else Console.WriteLine("не получилось взять предмет");
        }
        //выбрать рандомный арт из предложеного списка(pool), которого нет в инвентаре (arts_equiped)
        public Artefact RandArt(List<Artefact> arts_equiped, List<Artefact> pool)
        {
            var equipe_art = pool.Except(arts_equiped);
            if (equipe_art.Any())
            {
                Random random = new Random();
                var rand_art = equipe_art.ElementAt(random.Next(equipe_art.Count()));

                return rand_art;
            }
            else
            {
                Console.WriteLine("Не получилось взять ранд. артефакт");
                return null;
            }
        }
        //добавиьть рандомный арт
        public void AddArt_Random(CharacterTest c, List<Artefact> arts_equiped, List<Artefact> pool)
        {
            Artefact art = RandArt(arts_equiped, pool);
            AddArt(art, c);
        }
        //убрать арт
        public void RemoveArt(Artefact art, CharacterTest c)
        {
            for (int i = 0; i < art.effects_art.Count; i++)
            {
                foreach (Effect effect in art.effects_art)
                {
                    if (effect is PermanentEffect)
                    {
                        PermanentEffect effect2 = (PermanentEffect)effect;
                        if (effect2.Stat_type == "hp")
                        {
                            c.max_hp -= effect.Value_effect;
                        }
                        if (effect2.Stat_type == "atk")
                        {
                            c.atk -= effect.Value_effect;
                        }
                        if (effect2.Stat_type == "en")
                        {
                            c.max_energy -= effect.Value_effect;
                        }
                    }
                }
            }
            arts_equiped.Remove(art);
        }
        //продать арт
        public void SellArt(Artefact art, CharacterTest c)
        {
            c.wallet += art.price_item;
            RemoveArt(art, c );
        }
        //купить арт
        public void BuyArt(Artefact art, CharacterTest c)
        {
            if (c.wallet >= art.price_item)
            {
                c.wallet -= art.price_item;
                AddArt(art, c);
            }
            else Console.WriteLine("Вам не хватает стипы");
        }

        public void Show_Inventory(List<Item> bag)
        {
            Console.WriteLine($"Кол-во вкуснях: " + hp_potions.cur_health_stack);
            Console.WriteLine($"Кол-во кофе: " + en_potions.cur_energy_stack);
            Console.WriteLine($"Артефакты: ");
                if (pockets_max <= arts_equiped.Count)
                {
                    for (int j = 0; j < pockets_max; j++)
                    {
                        Console.WriteLine(j+1 + $") " + arts_equiped[j].name_item);
                    }
                }
                else if (pockets_max > arts_equiped.Count)
                {
                    int rest_space = pockets_max;
                    for (int j = 0; j < arts_equiped.Count; j++)
                    {
                        rest_space--;
                        Console.WriteLine(j+1 + $") " + arts_equiped[j].name_item);
                    }
                    for (int j = 0;j < rest_space; j++)
                        Console.WriteLine(j+1+ arts_equiped.Count + $")__________");
                }
        }
        
        public Artefact SelectArt(List<Artefact> bag)
        {
            bool end = false;
            int pockets = bag.Count;
            int max_pockets = pockets_max;
            int choise = Convert.ToInt32(Console.ReadLine());
            //while (end == false)
            //{
                if (choise > max_pockets)
                {
                    Console.WriteLine("Не получилось выбрать");
                    end = true;
                    return null;
                }
                else if (choise <= max_pockets && choise > pockets)
                {
                    Console.WriteLine("Пусто");
                    end = true;
                    return null;
                }
                else
                {
                    switch (choise)
                    {
                        case 1:
                            return bag[0];
                            break;
                        case 2:
                            return bag[1];
                            break;
                        case 3:
                            return bag[2];
                            break;
                        case 4:
                            return bag[3];
                            break;
                        case 5:
                            return bag[4];
                            break;
                        case 6:
                            return bag[5];
                            break;
                        default:
                            Console.WriteLine("Не получилось выбрать");
                            return null;
                            break;
                    }
                    end = true;
                }
            //}
            //return null;
            
        }
        public void RemoveSelectedArt(List<Artefact> bag, CharacterTest c)
        {
            Artefact art = SelectArt(bag);
            RemoveArt(art, c);
        }

    }
}
