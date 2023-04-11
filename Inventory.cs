using RogueMath.Item_Pack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// инвентарь будет иметь только игрок, не монстры
// магазин будет иметь класс инвенторя

namespace RogueMath
{
    internal class Inventory
    {
        Random rnd = new Random();

        public int pockets_max;
        public int pockets_free;
        public Health_Cons hp_potions;
        public Energy_Cons en_potions;
        public List<Artefact> arts_equiped;
        public Inventory(int pockets_max, Health_Cons hp_potions, Energy_Cons en_potions, List<Artefact> arts_equiped)
        {
            this.pockets_max = pockets_max;
            this.hp_potions = hp_potions;
            this.hp_potions = hp_potions;
            this.en_potions = en_potions;
            this.arts_equiped = arts_equiped;
        }

        //добавить арт в инвентарь
        public void AddArt(Artefact art, Player p)
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
                            p._maxHp += effect.Value_effect;
                        }
                        if (effect2.Stat_type == "atk")
                        {
                            p._atk += effect.Value_effect;
                        }
                        if (effect2.Stat_type == "en")
                        {
                            p._maxEnergy += effect.Value_effect;
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
        public void AddArt_Random(Player p, List<Artefact> arts_equiped, List<Artefact> pool)
        {
            Artefact art = RandArt(arts_equiped, pool);
            AddArt(art, p);
        }

        //убрать арт
        public void RemoveArt(Artefact art, Player p)
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
                            p._maxHp -= effect.Value_effect;
                        }
                        if (effect2.Stat_type == "atk")
                        {
                            p._atk -= effect.Value_effect;
                        }
                        if (effect2.Stat_type == "en")
                        {
                            p._maxEnergy -= effect.Value_effect;
                        }
                    }
                }
            }
            arts_equiped.Remove(art);
        }

        //продать арт
        public void SellArt(Artefact art, Player p)
        {
            p._gold += art.price_item;
            RemoveArt(art, p);
        }

        //купить арт
        public void BuyArt(Artefact art, Player p)
        {
            if (p._gold >= art.price_item)
            {
                p._gold -= art.price_item;
                AddArt(art, p);
            }
            else Console.WriteLine("Вам не хватает стипы");
        }

        //показ инвенторя (лучше потом красиво оформить)
        public void Show_Inventory(List<Item> bag)
        {
            Console.WriteLine($"Кол-во вкуснях: " + hp_potions.cur_health_stack);
            Console.WriteLine($"Кол-во кофе: " + en_potions.cur_energy_stack);
            Console.WriteLine($"Артефакты: ");
            if (pockets_max <= arts_equiped.Count)
            {
                for (int j = 0; j < pockets_max; j++)
                {
                    Console.WriteLine(j + 1 + $") " + arts_equiped[j].name_item);
                }
            }
            else if (pockets_max > arts_equiped.Count)
            {
                int rest_space = pockets_max;
                for (int j = 0; j < arts_equiped.Count; j++)
                {
                    rest_space--;
                    Console.WriteLine(j + 1 + $") " + arts_equiped[j].name_item);
                }
                for (int j = 0; j < rest_space; j++)
                    Console.WriteLine(j + 1 + arts_equiped.Count + $")__________");
            }
        }

        //выбор арта
        public Artefact SelectArt(List<Artefact> bag)
        {
            bool end = false;
            int pockets = bag.Count;
            int max_pockets = pockets_max;
            int choise = Convert.ToInt32(Console.ReadLine());
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

        }

        //удалить выбранный арт (без подтверждения)
        public void RemoveSelectedArt(List<Artefact> bag, Player p)
        {
            Artefact art = SelectArt(bag);
            RemoveArt(art, p);
        }

        //продать выбранный арт
        public void SellSelectedArt(List<Artefact> bag, Player p)
        {
            Artefact art = SelectArt(bag);
            Console.WriteLine("Вы точно хотите продать: " + art.name_item + "?");
            Console.WriteLine("Нажмите '0' для подтверждения");
            int choise = Convert.ToInt32(Console.ReadLine());
            if (choise == 1)
            {
                Console.WriteLine("Вы получили " + art.price_item + " стипы");
                SellArt(art, p);
            }
        }

        //печать инфы об арте (по хорошему, мне потом нужно добавить полезное описание (что арт прибавляет) )
        public void InfoArt(Artefact art)
        {
            Console.WriteLine(art.name_item);
            Console.WriteLine();
            Console.WriteLine(art.description_art);
            Console.WriteLine("Цена: " + art.price_item + " стипы");
        }

        //почитать описание арта из инвенторя(после выбора выходит из метода: можно потом доработать, чтобы выходил после нажатой клавиши выхода)
        public void LookArt(List<Item> items,List<Artefact> bag, Player p)
        {
           // Console.WriteLine("Для выхода из меню инвентaря выберете номер несуществующей ячейки инвенторя");
           // Show_Inventory(items);
            Console.WriteLine("Выберете предмет:");
            Artefact art = SelectArt(bag);
            if (art != null)
            {
                Console.WriteLine();
                InfoArt(art);
                Console.WriteLine();
                Console.WriteLine("Для выхода нажмите лютую клавишу");
                Console.ReadKey();
            }
        }
    }
}
