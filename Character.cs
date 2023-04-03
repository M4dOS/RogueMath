

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
        protected List<Buff>? buffs;
        protected int _exp;
        protected int _gold;
        public void Bite(Character character)
        {
            if (_atk - character._def < 1) --character._hp;
            else character._hp -= (_atk - character._def);
        }
        public void ULTRABite(Character character)
        {
            if (_energy > 0)
            {
                if (_atk+_energy / 2 - character._def < 1) --character._hp;
                else character._hp -= (_atk+_energy / 2 - character._def);
                if (_energy % 2 == 0)
                {
                    _energy /= 2;
                }
                else
                {
                    _energy = _energy / 2 + 1;
                }
            }
        }
    }

    internal class Enemy : Character
    {
        public Enemy(int _lvl,  Race r, int x, int y)
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
        public bool Movement(Map map) // движение монстрика
        {
            if (dead) return false;
            
            Random random = new Random();
            int temp_x = x;
            int temp_y = y;
            switch (random.Next(0, 5))
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
            if (map.cellMap[temp_x,temp_y].cellID == CellID.None)
            {                
                map.cellMap[temp_x, temp_y].enemyId = map.cellMap[x,y].enemyId;
                map.cellMap[x, y].enemyId = -1;
                map.cellMap[temp_x, temp_y].cellID = map.cellMap[x, y].cellID;
                map.cellMap[x, y].cellID = CellID.None;
                y = temp_y;
                x = temp_x;
                return true;
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
            phase = Phase.Nothing;
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
            ConsoleKeyInfo consoleKey = Console.ReadKey(true);
            int temp_x = x;
            int temp_y = y;
            List<ConsoleKey> consoleKeysList = new() { ConsoleKey.UpArrow, ConsoleKey.W, ConsoleKey.DownArrow, ConsoleKey.S,
                                                       ConsoleKey.LeftArrow, ConsoleKey.A, ConsoleKey.RightArrow, ConsoleKey.D };

            List<CellID> cellIDs = new() { CellID.HWall, CellID.VWall, CellID.ExitClose, CellID.Void,
                                           CellID.Enemy, CellID.Chest, CellID.Shop};

            switch (consoleKey.Key)
            {
                case ConsoleKey.UpArrow:
                case ConsoleKey.W:
                    --temp_y;
                    break;

                case ConsoleKey.LeftArrow:
                case ConsoleKey.A:
                    --temp_x;
                    break;

                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                    ++temp_y;
                    break;

                case ConsoleKey.RightArrow:
                case ConsoleKey.D:
                    ++temp_x;
                    break;

                default: break;
            }

            if ((!cellIDs.Contains(map.cellMap[temp_x, temp_y].cellID)
                && consoleKeysList.Contains(consoleKey.Key)) || (temp_x != x && temp_y != y))
            {
                
                map.cellMap[x, y].cellID = tempCell;
                y = temp_y;
                x = temp_x;
                tempCell = map.cellMap[x, y].cellID;
                map.cellMap[x, y].cellID = CellID.Player;
                return true;
            }
            else return false;
        }
        public enum Phase // фаза
        {
            Nothing,
            Battle
        }
        public int EnemyCheck(Map map) // проверка на врага
        {
            if (map.cellMap[x+1,y].cellID == CellID.Enemy) return map.cellMap[x + 1,y].enemyId;
            else if (map.cellMap[x -1, y].cellID == CellID.Enemy) return map.cellMap[x - 1, y].enemyId;
            else if (map.cellMap[x, y + 1].cellID == CellID.Enemy) return map.cellMap[x, y + 1].enemyId;
            else if (map.cellMap[x, y - 1].cellID == CellID.Enemy) return map.cellMap[x, y - 1].enemyId;
            else return -1;
        }
        public bool Battle (Enemy enemy) //фаза боя
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
