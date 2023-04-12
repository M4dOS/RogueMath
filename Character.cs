//using Windows.System.Threading.Core;


// Пришлось много коментировать, т.к. он просто отказывал  (ваши комменты не трогала)
// (извиняюсь за неудобство с этим)



using RogueMath.Item_Pack;

namespace RogueMath
{
    internal class Character
    {
        protected Race r;
        public int _maxHp;
        public int _hp;
        public int _def;
        public int _atk;
        public int _energy;
        public int _maxEnergy;


        //пока оружек нет, можно удалить
        //protected Weapon? w;


        public int _lvl;
        public int x;
        public int y;
        public int roomIDin;

        //временных эффектов тоже (Batle effects), могу попробовать добавить позже
        //protected List<Buff>? buffs;

        public int _exp;
        public int _gold;
        public CellID sign;

        public enum Phase // фаза
        {
            Adventure,
            Battle
        }

        protected void TestStats(Map map)
        {
            int forCx = Console.CursorLeft; int forCy = Console.CursorTop;

            Console.SetCursorPosition(5, map.maxY - 4);
            for (int i = 5; i < (map.maxX - 5 - 1 - 5); i++)
            {
                Console.Write(" ");
            }

            Console.SetCursorPosition(5 + 25, map.maxY - 4);
            Console.Write($"LVL:{_lvl} EXP:{_exp} HP:{_hp}/{_maxHp} DEF:{_def} ENG:{_energy}/{_maxEnergy} ATK:{_atk} GOLD:{_gold}");

            if (roomIDin > -1 && map.rooms[roomIDin].enemies.Count != 0)
            {
                Enemy e = map.rooms[roomIDin].enemies[0];
                Console.CursorLeft += 20;
                Console.Write($"LVL:{e._lvl} EXP:{e._exp} HP:{e._hp}/{e._maxHp} DEF:{e._def} ENG:{e._energy}/{e._maxEnergy} ATK:{e._atk} GOLD:{e._gold}");
            }

            Console.SetCursorPosition(forCx, forCy);
        }

        public void Bite(Character character)
        {
            if (_atk - character._def < 1)
            {
                --character._hp;
            }
            else
            {
                character._hp -= _atk - character._def;
            }
        }
        public void ULTRABite(Character character)
        {
            if (_energy > 0)
            {
                if (_atk + (_energy / 2) - character._def < 1)
                {
                    --character._hp;
                }
                else
                {
                    character._hp -= _atk + (_energy / 2) - character._def;
                }

                if (_energy % 2 == 0)
                {
                    _energy /= 2;
                }
                else
                {
                    _energy = (_energy / 2) + 1;
                }
            }
        }
    }

    internal class Enemy : Character
    {
        public bool dead;

        public Enemy(int _lvl, Race r, int x, int y)
        {
            switch (r) //добавить потом ещё рвсс врагов
            {
                case Race.Math:
                    this._lvl = 1;
                    _hp = 10;
                    _energy = 5;
                    _maxHp = _hp;
                    _maxEnergy = _energy;
                    _atk = 3;
                    _def = 0;
                    _exp = 0;
                    _gold = 0;
                    sign = CellID.Enemy;
                    break;
                case Race.Mather:
                    this._lvl = 1;
                    _hp = 100;
                    _maxHp = _hp;
                    _maxEnergy = _energy;
                    _energy = 15;
                    _atk = 6;
                    _def = 0;
                    _exp = 0;
                    _gold = 0;
                    sign = CellID.Boss;
                    break;
            }
            this._lvl = _lvl;
            if (_lvl > 1)
            {
                _hp += (_lvl * 3);
                _energy += (_lvl * 2);
                _atk += (_lvl * 5);
                _def += (_lvl * 2);
            }
            this.x = x;
            this.y = y;
            dead = false;
        }

        public bool Movement(Map map, Player player) // движение монстрика
        {
            if (dead)
            {
                return false;
            }

            Random random = new();
            int temp_x = x;
            int temp_y = y;
            switch (random.Next(1, 4 + 1))
            {
                case 1:
                    ++temp_x;
                    break;
                case 2:
                    --temp_x;
                    break;
                case 3:
                    ++temp_y;
                    break;
                case 4:
                    --temp_y;
                    break;
                default: break;
            }
            if (map.cellMap[temp_x, temp_y].cellID is CellID.None and not CellID.Player)
            {
                bool condition = true;
                foreach (CellInfo check in map.changesForCellMap)
                {
                    if (check.cellID == CellID.Player)
                    {
                        if (check.x == temp_x && check.y == temp_y) { condition = false; break; }
                    }
                }
                if (condition)
                {
                    map.AddChange(new(temp_x, temp_y, map.cellMap[x, y].cellID) { enemyId = map.cellMap[x, y].enemyId });
                    map.AddChange(new(x, y, map.cellMap[temp_x, temp_y].cellID) { enemyId = map.cellMap[temp_x, temp_y].enemyId });
                    y = temp_y;
                    x = temp_x;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

    }

    internal class Player : Character
    {
        public Inventory inventory;
        public int battlingWith;
        public Phase phase;
        public Player(Race r, int x, int y)
        {
            Health_Cons hp_potions = new(15, 5, "вкусняхи");
            Energy_Cons en_potions = new(10, 5, "кофе");
            _ = new List<Item>();
            List<Artefact> arts_equiped = new();
            switch (r) //добавить потом ещё расс врагов
            {
                case Race.Human:
                    _lvl = 1;
                    _hp = 1;
                    _maxHp = 11;
                    _energy = 1;
                    _maxEnergy = 10;
                    _atk = 5;
                    _def = 0;
                    _exp = 0;
                    _gold = 0;
                    break;
            }
            this.x = x;
            this.y = y;
            battlingWith = -1;
            phase = Phase.Adventure;
            inventory = new Inventory(6, hp_potions, en_potions, arts_equiped);
        }
        private int LvlUp(int _lvl) // опеределение кол-ва опыта для апа уровня
        {
            return _lvl * 10;
        }
        public int exp //ап уровня
        {
            set
            {
                _exp += value;
                if (_exp > LvlUp(_lvl + 1))
                {
                    ++_lvl;
                    _exp -= LvlUp(_lvl + 1);
                    _maxHp += (_lvl * 3);
                    _hp = _maxHp;
                    _maxEnergy += (_lvl * 2);
                    _energy = _maxEnergy;
                    _atk += (_lvl * 5);
                    _def += (_lvl * 2);
                }
            }
            get => _exp;
        }

        private CellID tempCell = CellID.None;


        public bool Movement(Map map) // движение чела
        {

            ConsoleKeyInfo consoleKey = Console.ReadKey(true);
            int temp_x = x;
            int temp_y = y;
            List<ConsoleKey> consoleKeysList = new() { ConsoleKey.W, ConsoleKey.A, ConsoleKey.S,ConsoleKey.D, ConsoleKey.Z
                                                       /*,ConsoleKey.UpArrow, ConsoleKey.LeftArrow, ConsoleKey.DownArrow, ConsoleKey.RightArrow*/};

            switch (consoleKey.Key)
            {
                /*case ConsoleKey.UpArrow:*/
                case ConsoleKey.W:
                    --temp_y;
                    break;

                /*case ConsoleKey.LeftArrow:*/

                case ConsoleKey.A:
                    --temp_x;
                    break;


                /*case ConsoleKey.DownArrow:*/

                case ConsoleKey.S:
                    ++temp_y;
                    break;


                /*case ConsoleKey.RightArrow:*/

                case ConsoleKey.D:
                    ++temp_x;
                    break;

                case ConsoleKey.Z:
                    inventory.InventoryTestPlus(this);
                    break;

                default: break;
            }

            if ((map.cellMap[temp_x, temp_y].isStepible && consoleKeysList.Contains(consoleKey.Key)) || (temp_x != x && temp_y != y))
            {
                map.AddChange(new(x, y, tempCell));
                y = temp_y;
                x = temp_x;
                tempCell = map.cellMap[x, y].cellID;

                foreach (Room room in map.rooms) { room.Exploring(this, map); }
                foreach (Tunel tunel in map.tunels) { tunel.Exploring(this, map); }

                map.AddChange(new(x, y, CellID.Player));
                return true;
            }
            else
            {
                return false;
            }
        }

        public int EnemyCheck(Map map) // проверка на врага
        {
            List<CellID> enemyIDs = new() { CellID.Enemy, CellID.Boss };
            if (enemyIDs.Contains(map.cellMap[x + 1, y].cellID))
            {
                return map.cellMap[x + 1, y].enemyId;
            }
            else if (enemyIDs.Contains(map.cellMap[x - 1, y].cellID))
            {
                return map.cellMap[x - 1, y].enemyId;
            }
            else if (enemyIDs.Contains(map.cellMap[x, y + 1].cellID))
            {
                return map.cellMap[x, y + 1].enemyId;
            }
            else
            {
                return enemyIDs.Contains(map.cellMap[x, y - 1].cellID) ? map.cellMap[x, y - 1].enemyId : -1;
            }
        }

        public void Advenchuring(Map map)
        {

            TestStats(map);

            Player player = this;

            if (player.phase == Player.Phase.Adventure)
            {
                if (player._hp < player._maxHp)
                {
                    ++player._hp;
                }
                else if (player._energy < player._maxEnergy /*&& player._hp < player._maxHp*/)
                {
                    ++player._energy;
                }

                if (player.Movement(map))
                {
                    for (int i = 0; i < map.rooms[player.roomIDin].enemies.Count; i++)
                    {
                        _ = map.rooms[player.roomIDin].enemies[i].Movement(map, player);
                    }

                    map.Update();
                }
                player.battlingWith = player.EnemyCheck(map);
                if (player.battlingWith > -1)
                {
                    player.phase = Player.Phase.Battle;
                }
            }

            else if (player.phase == Player.Phase.Battle)
            {

                if (player.Battle(map.rooms[player.roomIDin].enemies[0]))
                {
                    if (map.rooms[player.roomIDin].enemies[player.battlingWith]._hp > 0)
                    {
                        map.rooms[player.roomIDin].enemies[player.battlingWith].Bite(player);
                    }
                    else
                    {
                        map.AddChange(new(map.rooms[player.roomIDin].enemies[player.battlingWith].x, map.rooms[player.roomIDin].enemies[player.battlingWith].y, CellID.None));
                        player.phase = Player.Phase.Adventure;
                        map.rooms[player.roomIDin].enemies[player.battlingWith].dead = true;
                        map.rooms[player.roomIDin].ChangeDoorsStatus(map, player);
                        player.battlingWith = -1;
                        map.Update();
                    }
                }
            }

        }

        public bool Battle(Enemy enemy) //фаза боя
        {
            bool result = false;
            ConsoleKeyInfo consoleKey = Console.ReadKey(true);
            switch (consoleKey.Key)
            {
                case ConsoleKey.E:
                    Bite(enemy); //кусь
                    result = true;
                    break;
                case ConsoleKey.Q:
                    ULTRABite(enemy);//УЛЬТРАКУСЬ
                    result = true;
                    break;
            }
            return result;
        }
    }
}
