namespace RogueMath
{
    internal class Character
    {
        protected Race r;
        protected int _hp;
        protected int _def;
        protected int _atk;
        protected int _nerves;
        protected Weapon? w;
        protected int _lvl;
        public int x;
        public int y;
        protected List<Buff>? buffs;
        protected int _exp;
        protected int _gold;
    }

    internal class Enemy : Character
    {
        public Enemy(int _lvl, Race r)
        {
            switch (r) //добавить потом ещё рвсс врагов
            {
                case Race.Math:
                    _hp = 10;
                    _nerves = 5;
                    _atk = 5;
                    _def = 0;
                    _exp = 0;
                    _gold = 0;
                    break;
            }

            this._lvl = _lvl;
            _hp = _hp + (_lvl * 3);
            _nerves = _nerves + (_lvl * 2);
            _atk = _atk + (_lvl * 5);
            _def = _def + (_lvl * 2);
        }
    }


    internal class Player : Character
    {
        public Player(int _lvl, Race r, int x, int y)
        {
            switch (r) //добавить потом ещё расс врагов
            {
                case Race.Human:
                    _hp = 11;
                    _nerves = 10;
                    _atk = 5;
                    _def = 0;
                    _exp = 0;
                    _gold = 0;
                    break;
            }
            this._lvl = _lvl;
            _hp = _hp + (_lvl * 3);
            _nerves = _nerves + (_lvl * 2);
            _atk = _atk + (_lvl * 5);
            _def = _def + (_lvl * 2);
            this.x = x;
            this.y = y;
        }
        private int LvlUp(int _lvl)
        {
            return _lvl * 10;
        }
        public int exp
        {
            set
            {
                _exp += value;
                if (_exp > LvlUp(_lvl + 1))
                {
                    ++_lvl;
                    _exp -= LvlUp(_lvl + 1);
                }
            }
            get { return _exp; }
        }

        CellID tempCell = CellID.None;
        public bool Movement(Map map)
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
    }
}
