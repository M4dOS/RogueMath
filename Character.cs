

namespace RogueMath
{
    internal class Character
    {
        protected Race r;
        public int _maxHp;
        public int _hp;
        protected int _def;
        protected int _atk;
        public int _energy;
        public int _maxEnergy;
        protected Weapon? w;
        public int _lvl;
        public int x;
        public int y;
        public int roomIDin;
        protected List<Buff>? buffs;
        protected int _exp;
        protected int _gold;
        public enum Phase // фаза
        {
            Adventure,
            Battle
        }

        public void Bite(Character character)
        {
            if (_atk - character._def < 1) --character._hp;
            else character._hp -= _atk - character._def;
        }
        public void ULTRABite(Character character)
        {
            if (_energy > 0)
            {
                if (_atk + (_energy / 2) - character._def < 1) --character._hp;
                else character._hp -= _atk + (_energy / 2) - character._def;
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
        public Enemy(int _lvl, Race r, int x, int y)
        {
            switch (r) //добавить потом ещё рвсс врагов
            {
                case Race.Math:
                    _lvl = 1;
                    _hp = 10;
                    _energy = 5;
                    _atk = 3;
                    _def = 0;
                    _exp = 0;
                    _gold = 0;
                    break;
                case Race.Mather:
                    _lvl = 1;
                    _hp = 10;
                    _energy = 5;
                    _atk = 3;
                    _def = 0;
                    _exp = 0;
                    _gold = 0;
                    break;
            }

            this._lvl = _lvl;
            if (_lvl > 1)
            {
                _hp = _hp + (_lvl * 3);
                _energy = _energy + (_lvl * 2);
                _atk = _atk + (_lvl * 5);
                _def = _def + (_lvl * 2);
            }
            this.x = x;
            this.y = y;
            dead = false;
        }
        public bool Movement(Map map, Player player) // движение монстрика
        {
            if (dead) return false;

            Random random = new Random();
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
            }
            if (map.cellMap[temp_x, temp_y].cellID == CellID.None && map.cellMap[temp_x, temp_y].cellID != CellID.Player)
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
                    /*map.cellMap[temp_x, temp_y].enemyId = map.cellMap[x, y].enemyId;
                    map.cellMap[x, y].enemyId = -1;*/
                    map.AddChange(new(temp_x, temp_y, map.cellMap[x, y].cellID) { enemyId = map.cellMap[x, y].enemyId });
                    map.AddChange(new(x, y, map.cellMap[temp_x, temp_y].cellID) { enemyId = map.cellMap[temp_x, temp_y].enemyId });
                    y = temp_y;
                    x = temp_x;
                    return true;
                }
                else return false;
            }
            else return false;

        }
        public bool dead;
    }


    internal class Player : Character
    {
        public int battlingWith;
        public Phase phase;
        public Player(Race r, int x, int y)
        {
            switch (r) //добавить потом ещё расс врагов
            {
                case Race.Human:
                    _lvl = 1;
                    _hp = 11;
                    _maxHp = 11;
                    _energy = 10;
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
                    _maxHp = _maxHp + (_lvl * 3);
                    _hp = _maxHp;
                    _maxEnergy = _maxEnergy + (_lvl * 2);
                    _energy = _maxEnergy;
                    _atk = _atk + (_lvl * 5);
                    _def = _def + (_lvl * 2);
                }
            }
            get { return _exp; }
        }

        CellID tempCell = CellID.None;
        public bool Movement(Map map) // движение чела
        {
            int forCx = Console.CursorLeft; int forCy = Console.CursorTop;
            Console.SetCursorPosition(5 + 50, map.maxY - 4);
            Console.Write($"LVL:{_lvl} EXP:{_exp} HP:{_hp}/{_maxHp} DEF:{_def} ENG:{_energy}/{_maxEnergy} ATK:{_atk} GOLD:{_gold}");

            Console.SetCursorPosition(forCx, forCy);


            ConsoleKeyInfo consoleKey = Console.ReadKey(true);
            int temp_x = x;
            int temp_y = y;
            List<ConsoleKey> consoleKeysList = new() { ConsoleKey.W, ConsoleKey.A, ConsoleKey.S,ConsoleKey.D
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
            else return false;
        }

        public int EnemyCheck(Map map) // проверка на врага
        {
            if (map.cellMap[x + 1, y].cellID == CellID.Enemy) return map.cellMap[x + 1, y].enemyId;
            else if (map.cellMap[x - 1, y].cellID == CellID.Enemy) return map.cellMap[x - 1, y].enemyId;
            else if (map.cellMap[x, y + 1].cellID == CellID.Enemy) return map.cellMap[x, y + 1].enemyId;
            else if (map.cellMap[x, y - 1].cellID == CellID.Enemy) return map.cellMap[x, y - 1].enemyId;
            else return -1;
        }

        public void Advenchuring(Map map)
        {
            Player player = this;

            if (player.phase == Player.Phase.Adventure)
            {
                if (player._energy < player._maxEnergy) ++player._energy;
                else if (player._hp < player._maxHp) ++player._hp;

                if (player.Movement(map))
                {
                    for (int i = 0; i < map.rooms[player.roomIDin].enemies.Count; i++) map.rooms[player.roomIDin].enemies[i].Movement(map, player);
                    map.Update();
                }
                /*else
                {*/
                player.battlingWith = player.EnemyCheck(map);
                if (player.battlingWith > -1) player.phase = Player.Phase.Battle;
                /*}*/
            }

            else if (player.phase == Player.Phase.Battle)
            {

                if (player.Battle(map.rooms[player.roomIDin].enemies[0]))
                {
                    if (map.rooms[player.roomIDin].enemies[player.battlingWith]._hp > 0) map.rooms[player.roomIDin].enemies[player.battlingWith].Bite(player);
                    else
                    {
                        map.AddChange(new(map.rooms[player.roomIDin].enemies[player.battlingWith].x, map.rooms[player.roomIDin].enemies[player.battlingWith].y, CellID.None));
                        player.phase = Player.Phase.Adventure;
                        map.rooms[player.roomIDin].enemies[player.battlingWith].dead = true;
                        player.battlingWith = -1;
                        map.Update();
                    }
                }
            }

        }

        public bool Battle(Enemy enemy/*, ConsoleKeyInfo consoleKey*/) //фаза боя
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
