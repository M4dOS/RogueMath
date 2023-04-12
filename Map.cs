namespace RogueMath
{
    internal class Map //карта игры 
    {

        public int maxX, maxY; //границы карты
        protected int edge; //расстояние от полей карты
        public CellInfo[,] cellMap; //карта "клеток" (вероятно будет двойным массивом(списком))
        public List<Room> rooms; //список комнат
        public List<Tunel> tunels; //список всех проходов
        public bool[,] mapplace; //карта занятости

        public List<CellInfo> changesForCellMap;

        //общепринятые константы
        private const int roomEdge = 3;
        private const int minRooms = 5;
        private const int maxRooms = 12;
        private const int minRoomX = 10;
        private const int maxRoomX = 25;
        private const int minRoomY = 10;
        private const int maxRoomY = 15;

        public void AddChange(CellInfo change)
        {
            changesForCellMap.Add(change);
        }
        protected void GenerateMap() //генерация карты на основе обьектов 
        {
            //генерация крайних стен и пустот
            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < maxX; x++)
                {
                    //прорисовка границ
                    if ((x == maxX - 1 && y == 0) || (y == maxY - 1 && x == 0)) //углы побочной диагонали
                    {
                        cellMap[x, y] = new CellInfo(x, y, CellID.SecondVSpot);
                    }
                    else if ((y == 0 && x == 0) || (x == maxX - 1 && y == maxY - 1)) //углы главной диагонали
                    {
                        cellMap[x, y] = new CellInfo(x, y, CellID.MainVSpot);
                    }
                    else if (y == maxY - 1 || y == 0) //горизонтальные стены
                    {
                        cellMap[x, y] = new CellInfo(x, y, CellID.HWall);
                    }
                    else if (x == maxX - 1 || x == 0) //вертикальные стены
                    {
                        cellMap[x, y] = new CellInfo(x, y, CellID.VWall);
                    }

                    //прорисовка окошка для счётчиков
                    else if ((y == maxY - 4 - 1 && x == 3) || (y == maxY - 2 - 1 && x == maxX - 3 - 1)) //углы главной диагонали
                    {
                        cellMap[x, y] = new CellInfo(x, y, CellID.SecondVSpot);
                    }
                    else if ((y == maxY - 4 - 1 && (x == 3 || x == maxX - 3 - 1)) || (y == maxY - 2 - 1 && (x == 3 || x == maxX - 3 - 1))) //углы побочной диагонали
                    {
                        cellMap[x, y] = new CellInfo(x, y, CellID.MainVSpot);
                    }
                    else if (y > maxY - 4 - 1 && y < maxY - 2 - 1 && (x == 3 || x == maxX - 3 - 1)) //вертикальные стены
                    {
                        cellMap[x, y] = new CellInfo(x, y, CellID.VWall);
                    }
                    else if (x > 3 && x < maxX - 3 - 1 && (y == maxY - 4 - 1 || y == maxY - 2 - 1))
                    {
                        cellMap[x, y] = new CellInfo(x, y, CellID.HWall);
                    }
                    else
                    {
                        cellMap[x, y] = x > 3 && x < maxX - 3 - 1 && y > maxY - 4 - 1 && y < maxY - 2 - 1
                            ? new CellInfo(x, y, CellID.None)
                            : new CellInfo(x, y, CellID.Void);
                    }
                }
            }

            //генерация комнат

            foreach (Room room in rooms)
            {
                //генерация основания комнаты
                for (int y = room.y; y < room.height + room.y; ++y)
                {
                    for (int x = room.x; x < room.wigth + room.x; ++x)
                    {
                        if ((x == room.wigth + room.x - 1 && y == room.y) || (y == room.height + room.y - 1 && x == room.x)) //углы побочной диагонали
                        {
                            cellMap[x, y] = room.isExplored ? new CellInfo(x, y, CellID.SecondVSpot) : new CellInfo(x, y, CellID.Status, false);
                        }
                        else if ((y == room.y && x == room.x) || (x == room.wigth + room.x - 1 && y == room.height + room.y - 1)) //углы главной диагонали
                        {
                            cellMap[x, y] = room.isExplored ? new CellInfo(x, y, CellID.MainVSpot) : new CellInfo(x, y, CellID.Status, false);
                        }
                        else if (y == room.height + room.y - 1 || y == room.y) //горизонтальные стены
                        {
                            cellMap[x, y] = room.isExplored ? new CellInfo(x, y, CellID.HWall) : new CellInfo(x, y, CellID.Status, false);
                        }
                        else if (x == room.wigth + room.x - 1 || x == room.x) //вертикальные стены
                        {
                            cellMap[x, y] = room.isExplored ? new CellInfo(x, y, CellID.VWall) : new CellInfo(x, y, CellID.Status, false);
                        }
                        else
                        {
                            cellMap[x, y] = room.isExplored ? new CellInfo(x, y, CellID.None) : new CellInfo(x, y, CellID.Status, true);
                        }
                    }
                }

                //генерация выходов
                foreach (Exit exit in room.exits)
                {
                    if (!room.isExplored)
                    {
                        cellMap[exit.x, exit.y] = !exit.isOpen ? new CellInfo(exit.x, exit.y, CellID.Status, false) : new CellInfo(exit.x, exit.y, CellID.Status, true);
                    }
                    else
                    {
                        cellMap[exit.x, exit.y] = exit.isOpen ? new CellInfo(exit.x, exit.y, CellID.ExitOpen) : new CellInfo(exit.x, exit.y, CellID.ExitClose);
                    }
                }

                //генерация туннелей
                foreach (Tunel tunel in tunels)
                {
                    foreach (Line line in tunel.lines)
                    {
                        for (int x = line.xStart; x < line.xEnd; ++x)
                        {
                            for (int y = line.yStart; y < line.yEnd; ++y)
                            {
                                cellMap[x, y] = !line.isExplored ? new CellInfo(x, y, CellID.Status, false) : new CellInfo(x, y, CellID.Tunel);
                            }
                        }
                    }
                }

                //генерация доп. предметов на карте
                foreach (CellInfo obj in room.objects)
                {
                    if (room.isExplored) { cellMap[obj.x, obj.y] = new CellInfo(obj.x, obj.y, CellID.Status, false); }
                    cellMap[obj.x, obj.y] = new CellInfo(obj);
                }

                //генерация врагов
                foreach (Enemy enemy in room.enemies)
                {
                    cellMap[enemy.x, enemy.y] = room.isExplored ? new CellInfo(enemy.x, enemy.y, CellID.Status) : new CellInfo(enemy.x, enemy.y, CellID.Enemy, false);
                }
            }
        }

        public void Update()  /*ждёт пока его напишут вместо PrintMap*/
        {
            Console.SetCursorPosition(0, 0);
            foreach (CellInfo change in changesForCellMap)
            {
                Console.SetCursorPosition(change.x, change.y);
                cellMap[change.x, change.y] = change;
                Console.Write((char)cellMap[change.x, change.y].cellID);
            }
            changesForCellMap.Clear();
        }

        public void PrintMap() //печать карты 
        {
            bool isDebug = false;

            Console.SetCursorPosition(0, 0);
            for (int y = 0; y < maxY; ++y)
            {
                for (int x = 0; x < maxX; ++x)
                {
                    Console.Write((char)cellMap[x, y].cellID);

                }
                if (y == maxY - 1)
                {
                    Console.Write("");
                }
                else
                {
                    Console.WriteLine();
                }
            }


            if (isDebug)
            {
                List<CellID> cellIDs = new() { CellID.HWall, CellID.VWall, CellID.ExitClose, /*CellID.Void,*/
                                           CellID.Enemy, CellID.Chest, CellID.Shop};
                for (int y = 0; y < mapplace.GetLength(1); ++y)
                {
                    for (int x = 0; x < mapplace.GetLength(0); ++x)
                    {
                        if (cellIDs.Contains(cellMap[x, y].cellID))
                        {
                            Console.Write((char)cellMap[x, y].cellID);
                        }
                        else
                        {
                            Console.Write(mapplace[x, y] ? 1 : 0);
                        }
                    }
                    Console.WriteLine("");
                }
            }
        }

        public void TypePlacer() //распределение RoomType 
        {

        }

        private void RoomPlace() //расставление комнат 
        {
            bool isDebug = false;

            Random rand = new();
            List<Room> loc_rooms = new();

            int tries = 0;


            while (rooms.Count is not (>= minRooms and <= maxRooms))
            {
                for (int i = 0; i < 150; ++i)
                {
                    Room room0 = new(rand.Next(edge + 1 + 3, maxX - edge + 1 - 3), rand.Next(edge + 1, maxY - (edge * 2) + 1), rand.Next(minRoomX, maxRoomX + 1), rand.Next(minRoomY, maxRoomY + 1), RoomType.Enemy)
                    {
                        manual = false
                    };

                    if (room0.x + room0.wigth < maxX - edge && room0.y + room0.height < maxY - 10)
                    {
                        loc_rooms.Add(room0);
                    }
                }

                List<bool> validID = new();


                foreach (Room manual_room in rooms)
                {
                    for (int y = manual_room.y - roomEdge; y < manual_room.y + manual_room.height + roomEdge; ++y)
                    {
                        for (int x = manual_room.x - roomEdge; x < manual_room.x + manual_room.wigth + roomEdge; x++)
                        {
                            mapplace[x, y] = true;
                        }
                    }
                }

                foreach (Room room in loc_rooms)
                {
                    bool isThird = false;

                    for (int y = room.y - roomEdge; y < room.y + room.height + roomEdge; ++y)
                    {
                        if (isThird) { break; }
                        for (int x = room.x - roomEdge; x < room.x + room.wigth + roomEdge; x++)
                        {
                            if (mapplace[x, y])
                            {
                                isThird = true;
                            }
                        }
                    }

                    if (isThird)
                    {
                        validID.Add(!isThird);
                    }

                    else
                    {
                        for (int y = room.y - roomEdge; y < room.y + room.height + roomEdge; ++y)
                        {
                            for (int x = room.x - roomEdge; x < room.x + room.wigth + roomEdge; x++)
                            {
                                mapplace[x, y] = true;
                            }
                        }

                        validID.Add(!isThird);
                    }
                }

                for (int i = 0; i < validID.Count; ++i)
                {
                    if (validID[i]) { rooms.Add(loc_rooms[i]); }
                }

                if (rooms.Count > maxRooms)
                {
                    while (rooms.Count > maxRooms)
                    {
                        Room room_x = rooms[rand.Next(rooms.Count)];
                        if (!room_x.manual)
                        {
                            _ = rooms.Remove(room_x);
                            CalibrateRoomIDs();
                        }
                    }
                }

                ++tries;

                if (isDebug)
                {
                    for (int y = 0; y < mapplace.GetLength(1); ++y)
                    {
                        for (int x = 0; x < mapplace.GetLength(0); ++x)
                        {
                            Console.Write(mapplace[x, y] ? 1 : 0);
                        }
                        Console.WriteLine("");
                    }
                    Console.WriteLine("\n");
                    Thread.Sleep(20);
                }

                if (tries > 2)
                {
                    int manuals = 0;
                    foreach (Room room in rooms)
                    {
                        if (!room.manual)
                        {
                            for (int y = room.y - roomEdge; y < room.y + room.height + roomEdge; ++y)
                            {
                                for (int x = room.x - roomEdge; x < room.x + room.wigth + roomEdge; ++x)
                                {
                                    mapplace[x, y] = false;
                                }
                            }
                        }
                        else
                        {
                            ++manuals;
                        }
                    }

                    while (rooms.Count > manuals)
                    {
                        for (int i = 0; i < rooms.Count; ++i)
                        {
                            if (!rooms[i].manual) { rooms.RemoveAt(i); break; }
                        }
                    }
                    loc_rooms.Clear();
                    tries = 0;
                }
            }

            CalibrateRoomIDs();

        }

        public void PrepareForWeb() //подготовка карты занятости под создание соединений 
        {
            foreach (Room manual_room in rooms)
            {
                for (int y = manual_room.y - roomEdge; y < manual_room.y + manual_room.height + roomEdge; ++y)
                {
                    for (int x = manual_room.x - roomEdge; x < manual_room.x + manual_room.wigth + roomEdge; x++)
                    {
                        if (x == manual_room.x - roomEdge || x == manual_room.x + manual_room.wigth + roomEdge - 1
                        || y == manual_room.y - roomEdge || y == manual_room.y + manual_room.height + roomEdge - 1

                        /*|| x == manual_room.x - roomEdge + 1 || x == manual_room.x + manual_room.wigth + roomEdge - 1 - 1
                        || y == manual_room.y - roomEdge + 1 || y == manual_room.y + manual_room.height + roomEdge - 1 - 1*/)
                        {
                            mapplace[x, y] = false;
                        }
                    }
                }
            }
        }

        private void CreateWeb() //соединение комнат 
        {
            Exit exitFrom;
            Exit exitTo;

            List<Exit> exitList = new();

            PrepareForWeb();

            foreach (Room room in rooms)
            {
                foreach (Exit exit in room.exits)
                {
                    exitList.Add(exit);
                }
            }

            int intDestination = maxX + maxY + 1;
            List<int> removeExitsID = new();

            foreach (Exit dot in exitList)
            {
                exitFrom = dot;
                exitTo = dot;

                List<Tunel> tunels = new();

                bool findConnect = false;

                for (int indexOfExit = 0; indexOfExit < exitList.Count; indexOfExit++)
                {
                    if (exitList[indexOfExit] == dot || exitList[indexOfExit].isConnected)
                    {
                        continue;
                    }

                    foreach (int remove in removeExitsID)
                    {
                        if (removeExitsID.Contains(indexOfExit))
                        {
                            continue;
                        }
                    }
                    foreach (int remove in removeExitsID)
                    {
                        if (removeExitsID.Contains(exitList.IndexOf(dot)))
                        {
                            break;
                        }
                    }


                    if (dot.Distance(exitList[indexOfExit]) < intDestination)
                    {
                        exitTo = exitList[indexOfExit];
                        intDestination = dot.Distance(exitTo);
                    }
                }

                if (exitTo != exitFrom)
                {
                    findConnect = true;
                }

                if (!findConnect) { _ = rooms[dot.roomID].exits.Remove(dot); removeExitsID.Add(exitList.IndexOf(dot)); }

                if (!Drawer(exitFrom, exitTo, tunels))
                {
                    if (!IsValidLine(new Line(exitFrom.x, exitFrom.y, exitFrom.mode, 3, "dot-lenght"))) { removeExitsID.Add(exitList.IndexOf(exitFrom)); }
                    else if (!IsValidLine(new Line(exitTo.x, exitTo.y, exitTo.mode, 3, "dot-lenght"))) { removeExitsID.Add(exitList.IndexOf(exitTo)); }
                }

                else
                {
                    Tunel? minDestTunel = null;
                    int min = maxX * maxX;
                    foreach (Tunel tunel in tunels)
                    {
                        if (tunel.length < min)
                        {
                            minDestTunel = tunel;
                            min = tunel.length;
                        }
                    }

                    if (minDestTunel != null)
                    {
                        this.tunels.Add(minDestTunel);
                    }
                    else
                    {
                        break;
                    }

                    foreach (Line line in minDestTunel.lines)
                    {
                        line.DrawMapPlacer(this);
                    }
                }
            }
            foreach (int remove in removeExitsID)
            {
                exitList.RemoveAt(remove);
            }
        }

        protected bool Drawer(Exit exitA, Exit exitB, List<Tunel> aTunels) //рисуем туннель от выхода до выхода 
        {
            Random rand = new();
            Line startLineA = new(exitA.x, exitA.y, exitA.mode, roomEdge + 1, "dot-lenght");
            Line startLineB = new(exitB.x, exitB.y, exitB.mode, roomEdge + 1, "dot-lenght");
            List<Tunel> tunels = new();
            List<Line> lines = new();

            /*здесь должен быть код для рисования линий*/

            return true;
        }

        public void CalibrateRoomIDs() //калибруем айди у комнат и выходов 
        {
            int i = 0;
            foreach (Room room in rooms)
            {
                room.roomID = i;
                foreach (Exit exit in room.exits)
                {
                    exit.roomID = i;
                }
                foreach (Enemy enemy in room.enemies)
                {
                    AddChange(new(cellMap[enemy.x, enemy.y]) { enemyId = i });
                }
                ++i;
            }
        }

        protected bool IsValidLine(Line line) //удовлетворяет ли нарисованная линия карте 
        {
            for (int y = line.yStart; y < Math.Abs(line.xStart - line.xEnd); ++y)
            {
                for (int x = line.xStart; x < Math.Abs(line.yStart - line.yEnd); ++x)
                {
                    if (mapplace[x, y] || line.xStart < edge || line.yStart < edge)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        protected bool IsValidTunel(Tunel tunel)    //удовлетворяет ли данный туннель карте 
        {
            for (int i = 0; i < tunel.lines.Count - 1; ++i)
            {
                if (!(tunel.lines[i].xEnd == tunel.lines[i + 1].xStart && tunel.lines[i].yEnd == tunel.lines[i + 1].yStart))
                {
                    return false;
                }

                if (!IsValidLine(tunel.lines[i]) || !IsValidLine(tunel.lines[i + 1]))
                {
                    return false;
                }
            }
            return true;
        }

        public void Create(Player player) //создание карты 
        {
            RoomPlace();
            CreateWeb();
            GenerateMap();
            AddEnemies(player);
            Update();
            CalibrateRoomIDs();
            PrintMap();
        }

        public void AddEnemies(Player player) //спавн врагов 
        {
            foreach (Room room in rooms)
            {
                if (room.roomType == RoomType.Enemy)
                {
                    room.enemies.Add(new Enemy(player._lvl, Race.Math, room.x + 3, room.y + 2));
                    if (room.isExplored)
                    {
                        AddChange(new(room.enemies[0].x, room.enemies[0].y, CellID.Enemy));
                    }
                }

                else if (room.roomType == RoomType.Mather)
                {
                    room.enemies.Add(new Enemy(player._lvl, Race.Mather, room.x + 3, room.y + 2));
                    if (room.isExplored)
                    {
                        AddChange(new(room.enemies[0].x, room.enemies[0].y, CellID.Boss));
                    }
                }
            }
        }

        public Map(int maxX, int maxY, int edge) //создание (устаревшее) 
        {
            cellMap = new CellInfo[maxX, maxY];
            changesForCellMap = new();
            rooms = new();
            this.maxX = maxX;
            this.maxY = maxY;
            this.edge = edge;
            mapplace = new bool[maxX, maxY];
            tunels = new();
        }

        public Map(int maxX, int maxY, int edge, List<Room> rooms) //создание 
                                                                   : this(maxX, maxY, edge)
        {
            foreach (Room room in rooms)
            {
                this.rooms.Add(room);
            }
        }
    }
}
