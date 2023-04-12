namespace RogueMath
{
    internal class Room //комната 
    {
        //координаты
        public int x;
        public int y;

        public List<Enemy> enemies;

        public int roomID; //идентификатор комнаты
        public int wigth; //длина
        public int height; //ширина
        public List<Exit> exits; //выходы комнаты
        public bool isExplored; //исследована ли комната?
        public RoomType roomType; //id комнаты
        public List<CellInfo> objects; //список доп. обьектов на карте
        public bool manual; //создана ли вручную?

        protected void ExitPlacer() //размещение выходов 
        {
            Random rand = new();

            while (exits.Count <= 1)
            {
                List<Exit> loc_exits = new();

                for (int i = 0; i < 150; ++i)
                {
                    Exit exit0 = new(rand.Next(x, x + wigth + 1), rand.Next(y, y + height + 1));
                    loc_exits.Add(exit0);
                }

                List<Exit> WallN = new();
                List<Exit> WallS = new();
                List<Exit> WallW = new();
                List<Exit> WallE = new();

                foreach (Exit exit in loc_exits)
                {
                    if ((exit.x == x || exit.y == y || exit.x == x + wigth - 1 || exit.y == y + height - 1) //проверка на нахождение на стороне
                    && ((exit.y >= y + 3 && exit.y <= y + height - 4) || (exit.x >= x + 3 && exit.x <= x + wigth - 4))) //проверка что это не угол 
                    {
                        if (exit.x == x)
                        {
                            WallW.Add(exit);
                        }
                        else if (exit.y == y)
                        {
                            WallN.Add(exit);
                        }
                        else if (exit.x == x + wigth - 1)
                        {
                            WallE.Add(exit);
                        }
                        else if (exit.y == y + height - 1)
                        {
                            WallS.Add(exit);
                        }
                    }
                }

                //теперь выходы отфильтрованы по стенам

                List<List<Exit>> Walls = new() { WallN, WallS, WallW, WallE };
                foreach (List<Exit> ex in Walls)
                {
                    if (ex.Count < 2) //зачем проверять стену с <2 выходами
                    {
                        continue;
                    }

                    while (ex.Count > 2) //нужно не более 2 выходов на 1 стене
                    {
                        ex.RemoveAt(2);
                    }

                    //если расстояние выходов на одной стене маленькое - просто уберу одну из них

                    if ((Math.Abs(ex[0].x - ex[1].x) < 4 && ex == WallN) || ex == WallS
                        || (Math.Abs(ex[0].y - ex[1].y) < 4 && ex == WallW) || ex == WallE)
                    {
                        ex.RemoveAt(0);
                    }

                    //добавляем все отфильтрованные выходы в один список
                    foreach (Exit exit in Walls[Walls.IndexOf(ex)])
                    {
                        exit.mode = Walls.IndexOf(ex);
                        exits.Add(exit);
                    }

                    //если вышло слишком много, отнимаем до 4
                    if (exits.Count > 4)
                    {
                        while (exits.Count > 4)
                        {
                            exits.RemoveAt(rand.Next(0, exits.Count));
                        }
                    }
                }
            }
        }

        protected int DeadEnemiesCount()
        {
            int i = 0;
            foreach (Enemy enemy in enemies)
            {
                if (!enemy.dead)
                {
                    ++i;
                }
            }
            return i;
        }
        public bool PlayerInRoom(Player player)
        {
            return player.x > x && player.x < x + wigth && player.y > y && player.y < y + height && player.roomIDin == roomID;
        }
        public void ChangeDoorsStatus(Map map, Player player)
        {
            bool openStatus = true;
            if (PlayerInRoom(player) && DeadEnemiesCount() > 0)
            {
                openStatus = false;
            }

            foreach (Exit exit in exits)
            {
                if (openStatus)
                {
                    map.AddChange(new(exit.x, exit.y, CellID.ExitOpen));
                }
                else
                {
                    map.AddChange(new(exit.x, exit.y, CellID.ExitClose));
                }
            }
            player.battlingWith = openStatus ? -1 : roomID;
        }

        public Room(int x, int y, int wigth, int height) //создание комнаты-спавна
        {
            this.x = x;
            this.y = y;
            this.wigth = wigth;
            this.height = height;
            isExplored = false;
            objects = new List<CellInfo>();
            exits = new List<Exit>();
            enemies = new List<Enemy>();
            roomType = RoomType.Spawn;
            manual = true;
            ExitPlacer();
        }
        public Room(int x, int y, int wigth, int height, RoomType roomType) : this(x, y, wigth, height) //создание комнаты определённого типа
        {
            this.roomType = roomType;
        }

        public void ChangeType(RoomType roomType)
        {
            this.roomType = roomType;
        }

        public void Exploring(Player player, Map map)
        {
            foreach (Exit exit in exits)
            {
                if ((player.x == exit.x && player.y == exit.y + 1)
                    || (player.x == exit.x && player.y == exit.y - 1)
                    || (player.x == exit.x - 1 && player.y == exit.y)
                    || (player.x == exit.x + 1 && player.y == exit.y)
                    || PlayerInRoom(player))
                {
                    if (exit.isOpen)
                    {
                        isExplored = true;
                        player.roomIDin = exit.roomID;
                    }

                }
            }

            if (isExplored)
            {
                //генерация основания комнаты
                for (int y = this.y; y < height + this.y; ++y)
                {
                    for (int x = this.x; x < wigth + this.x; ++x)
                    {
                        if ((x == wigth + this.x - 1 && y == this.y) || (y == height + this.y - 1 && x == this.x)) //углы побочной диагонали
                        {
                            map.AddChange(new(x, y, CellID.SecondVSpot));
                        }
                        else if ((y == this.y && x == this.x) || (x == wigth + this.x - 1 && y == height + this.y - 1)) //углы главной диагонали
                        {
                            map.AddChange(new(x, y, CellID.MainVSpot));
                        }
                        else if (y == height + this.y - 1 || y == this.y) //горизонтальные стены
                        {
                            map.AddChange(new(x, y, CellID.HWall));
                        }
                        else if (x == wigth + this.x - 1 || x == this.x) //вертикальные стены
                        {
                            map.AddChange(new(x, y, CellID.VWall));
                        }
                        else
                        {
                            map.AddChange(new(x, y, CellID.None));
                        }
                    }
                }

                //генерация выходов
                ChangeDoorsStatus(map, player);

                //генерация доп. предметов на карте
                foreach (CellInfo obj in objects)
                {
                    map.AddChange(new(obj));
                }

                foreach (Enemy enemy in enemies)
                {
                    if (enemy.dead) { map.AddChange(new(enemy.x, enemy.y, CellID.None)); }
                    else
                    {
                        map.AddChange(new(enemy.x, enemy.y, enemy.sign));
                    }
                }

                isExplored = false;
            }



        }
    }
}
