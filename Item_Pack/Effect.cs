using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueMath.Item_Pack
{
    internal class Effect
    {
        public Effect()
        {

        }

        //one use
        protected void Add_Max_Health(Character c, int add_hp)
        {
            c.max_hp = c.max_hp + add_hp;
        }
        protected void Add_Max_Health_and_Heal(Character c, int add_hp)
        {
            c.max_hp = c.max_hp + add_hp;
            c.cur_hp = c.max_hp;
        }
        protected void Add_Max_Energy(Character c, int add_en)
        {
            c.max_energy = c.max_energy + add_en;
        }
        protected void Add_Max_Energy_and_Recover(Character c, int add_en)
        {
            c.max_energy = c.max_energy + add_en;
            c.cur_energy = c.max_energy;
        }
        protected void Add_Base_Attack(Character c, int add_atk)
        {
            c.atk = c.atk + add_atk;
        }
        protected void Get_Exp(Character c, int exp)
        {
            c._exp = c._exp + exp;
        }
        protected void Get_Max_Potions_Hp(Health_Cons max_hp_potions, int add_number)
        {
            max_hp_potions.max_health_stack = max_hp_potions.max_health_stack + add_number;
        }
        protected void Get_Max_Potions_En(Energy_Cons max_en_potions, int add_number)
        {
            max_en_potions.max_energy_stack = max_en_potions.max_energy_stack + add_number;
        }

        //----------------------------------------------------------------------------------------------------------

        protected void Get_Const_Damage(Character c, int dmg)//poison|burn|curse etc.
        {
            c.cur_hp = c.cur_hp - dmg;
        }
        protected void Get_Const_Heal(Character c, int hp)//evry turn get healed a little
        {
            if (c.cur_hp + hp !> c.max_hp)
            { 
                c.cur_hp = c.cur_hp + hp;
            }
            else
            {
                c.cur_hp = c.max_hp;
            }
        }
        protected void Get_Const_Recover(Character c, int en)//evry turn recover some energy
        {
            if (c.cur_energy + en !> c.max_energy)
            {
                c.cur_energy = c.cur_energy + en;
            }
            else
            {
                c.cur_energy = c.max_energy;
            }
        }
    }
}
