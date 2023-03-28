namespace RogueMath
{
    internal class Map
    {
        int maxX, maxY; //границы карты
        CellInfo[,] cellMap; //карта "клеток" (вероятно будет двойным массивом(списком))
        public List<Room> rooms; //список комнат


        /*public void FindSize()
        { //находит размеры
            foreach (CellInfo axes in this.cellMap)
            {
                if (axes.x > maxX)
                {
                    maxX = axes.x;
                }
                if (axes.y > maxY)
                {
                    maxY = axes.y;
                }
            }
        }*/

        /* public void SortCells()
        { //сортировка клеток
            List<List<CellInfo>> sortedMap = new List<List<CellInfo>>();
            for (int i = 0; i < this.maxX; ++i)
            {
                for (int j = 0; i < this.maxY; ++j)
                {
                    foreach (List<CellInfo> axes1 in this.cellMap)
                    {
                        foreach (CellInfo axes in axes1)
                        {
                            if (axes.x == i && axes.y == j)
                            {
                                sortedMap.Add(axes1);
                                break;
                            }
                        }
                    }
                }
            }

            this.cellMap = sortedMap;
        }*/

        bool[,] mapplace;

        public void GenerateMap()
        {
            //генерация крайних стен и пустот
            for (int i = 0; i < this.maxY; i++)
            {
                for (int j = 0; j < this.maxX; j++)
                {
                    if (j == this.maxX - 1 && i == 0 || i == this.maxY - 1 && j == 0) //углы побочной диагонали
                    {
                        cellMap[j, i] = new CellInfo(j, i, CellID.SecondVSpot);
                    }
                    else if (i == 0 && j == 0 || j == this.maxX - 1 && i == this.maxY - 1) //углы главной диагонали
                    {
                        cellMap[j, i] = new CellInfo(j, i, CellID.MainVSpot);
                    }
                    else if (i == this.maxY - 1 || i == 0) //горизонтальные стены
                    {
                        cellMap[j, i] = new CellInfo(j, i, CellID.HWall);
                    }
                    else if (j == this.maxX - 1 || j == 0) //вертикальные стены
                    {
                        cellMap[j, i] = new CellInfo(j, i, CellID.VWall);
                    }
                    else
                    {
                        cellMap[j, i] = new CellInfo(j, i, CellID.Void);
                    }
                }
            }

            //генерация комнат

            foreach (Room room in rooms)
            {
                //генерация основания комнаты
                for (int i = room.y; i < room.height + room.y; ++i)
                {
                    for (int j = room.x; j < room.wigth + room.x; ++j)
                    {
                        if (j == room.wigth + room.x - 1 && i == room.y || i == room.height + room.y - 1 && j == room.x) //углы побочной диагонали
                        {
                            cellMap[j, i] = new CellInfo(j, i, CellID.SecondVSpot);
                        }
                        else if (i == room.y && j == room.x || j == room.wigth + room.x - 1 && i == room.height + room.y - 1) //углы главной диагонали
                        {
                            cellMap[j, i] = new CellInfo(j, i, CellID.MainVSpot);
                        }
                        else if (i == room.height + room.y - 1 || i == room.y) //горизонтальные стены
                        {
                            cellMap[j, i] = new CellInfo(j, i, CellID.HWall);
                        }
                        else if (j == room.wigth + room.x - 1 || j == room.x) //вертикальные стены
                        {
                            cellMap[j, i] = new CellInfo(j, i, CellID.VWall);
                        }
                        else
                        {
                            cellMap[j, i] = new CellInfo(j, i, CellID.None);
                        }
                    }
                }

                //генерация выходов
                foreach (Exit exit in room.exits)
                {
                    cellMap[exit.x, exit.y] = new CellInfo(exit.x, exit.y, CellID.Exit);
                }

                //генерация туннелей
                foreach(Room room1 in rooms)
                {
                    foreach(Tunel tunel in room1.tunels)
                    {
                        foreach(Line line in tunel.lines)
                        {
                            for(int x = line.xStart; x < line.xEnd; ++x)
                            {
                                for(int y = line.yStart; y < line.yEnd; ++y)
                                {
                                    cellMap[x, y] = new CellInfo(x, y, CellID.Tunel);
                                }
                            }
                        }
                    }
                }

                //генерация доп. предметов на карте
                foreach (CellInfo obj in room.objects)
                {
                    cellMap[obj.x, obj.y] = new CellInfo(obj);
                }
            }
        }

        public void PrintMap()
        { //печать карты
            for (int j = 0; j < this.maxY; ++j)
            {
                for (int i = 0; i < this.maxX; ++i)
                {
                    Console.Write((char)this.cellMap[i, j].cellID);
                }
                Console.WriteLine();
            }
        }

        public void RoomPlace()
        {
            Random rand = new Random();
            List<Room> loc_rooms = new List<Room>();

            int minRooms = 5;
            int maxRooms = 8;

            int tries = 0;

            this.mapplace = new bool[maxX, maxY];

            while (!(minRooms <= rooms.Count && rooms.Count <= maxRooms))
            {
                for (int i = 0; i < 150; ++i)
                {
                    Room room0 = new Room(rand.Next(5, maxX - 5 + 1), rand.Next(5, maxY - 10 + 1), rand.Next(15, 25 + 1), rand.Next(5, 20 + 1));
                    room0.manual = false;
                    if (room0.x + room0.wigth < maxX - 5 && room0.y + room0.height < maxY - 10)
                    {
                        loc_rooms.Add(room0);
                    }
                }

                List<bool> validID = new List<bool>();


                foreach (Room manual_room in rooms)
                {
                    for (int y = manual_room.y - 5; y < manual_room.y + manual_room.height + 5; ++y)
                    {
                        for (int x = manual_room.x - 5; x < manual_room.x + manual_room.wigth + 5; x++)
                        {
                            mapplace[x, y] = true;
                        }
                    }
                }

                foreach (Room room in loc_rooms)
                {
                    bool isThird = false;

                    for (int y = room.y - 5; y < room.y + room.height + 5; ++y)
                    {
                        if (isThird) { break; }
                        for (int x = room.x - 5; x < room.x + room.wigth + 5; x++)
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
                        for (int y = room.y - 5; y < room.y + room.height + 5; ++y)
                        {
                            for (int x = room.x - 5; x < room.x + room.wigth + 5; x++)
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

                if (rooms.Count > 8)
                {
                    while (rooms.Count > 8)
                    {
                        Room room_x = rooms[rand.Next(rooms.Count)];
                        if (!room_x.manual)
                        {
                            rooms.Remove(room_x);
                            CalibrateRoomIDs();
                        }
                    }
                }

                ++tries;


#if DEBUG       //дебаг карты занятости
                for (int i = 0; i < mapplace.GetLength(1); ++i)
                {
                    for (int j = 0; j < mapplace.GetLength(0); ++j)
                    {
                        Console.Write(mapplace[j, i] ? 1 : 0);
                    }
                    Console.WriteLine("");
                }
                Thread.Sleep(2000);
#endif

                if (tries == 2)
                {
                    for (int i = 0; i < rooms.Count; i++)
                    {
                        Room room = rooms[i];

                        for (int y = room.y; y < room.y + room.height; ++y)
                        {
                            for (int x = room.x; x < room.x + room.wigth; ++x)
                            {
                                mapplace[x, y] = false;
                            }
                        }

                        if (!room.manual)
                        {
                            rooms.Remove(room);
                            CalibrateRoomIDs();
                        }
                    }

                    tries = 0;
                }

                CalibrateRoomIDs();

            }
        }

        public void CreateWeb()
        {


            /*List<int> roomExitIDs = new List<int>();
            int ExitIDroom = -1;

            Random rand = new Random();
            List<Exit> exits_web = new List<Exit>();


            foreach (Room room in rooms)
            {
                foreach (Exit exit in room.exits)
                {
                    exits_web.Add(exit);
                    ++ExitIDroom;
                }
                roomExitIDs.Add(ExitIDroom);
            }

            foreach(Exit exit in exits_web)
            {
                int roomIDfrom = exit.roomID;
                int roomIDto = 0;
                int exitID = 0;
                int shortD = 9999;

                for(int i = 0; i < exits_web.Count; ++i)
                {
                    if (exits_web[i] == exit)
                    {
                        continue;
                    }

                    if ((int)Math.Sqrt(Math.Pow(exits_web[i].x - exit.x, 2) + Math.Pow(exits_web[i].y - exit.y, 2)) < shortD)
                    {
                        shortD = (int)Math.Sqrt(Math.Pow(exits_web[i].x - exit.x, 2) + Math.Pow(exits_web[i].y - exit.y, 2));
                        roomIDto = exits_web[i].roomID;
                        exitID = i;
                    }
                }

                Exit exitTo = exits_web[(int)exitID];

                Exit exitIn = exits_web[(int)roomIDto];
                Line line0 = new Line(0,0,0,0);

                switch (exit.mode)
                {
                    case 1: line0 = new Line(exit.x, exit.y + 1, exit.x, exit.y + 1 + 2); break;
                    case 2: line0 = new Line(exit.x, exit.y - 1, exit.x, exit.y - 1 - 2); break;
                    case 3: line0 = new Line(exit.x + 1, exit.y, exit.x + 1 + 2, exit.y); break;
                    case 4: line0 = new Line(exit.x - 1, exit.y, exit.x - 1 - 2, exit.y); break;
                }

                if (!IsValidLine(line0))
                {
                    foreach (int index in roomExitIDs)
                    {
                        if(exits_web.IndexOf(exit) < index)
                        {
                            rooms[index].exits.Remove(exit);
                            break;
                        }
                    }
                    continue;
                }
                else
                {
                    int cursorX = line0.xEnd;
                    int cursorY = line0.yEnd;

                }
            }

*/
        }

        public void CalibrateRoomIDs()
        {
            int i = 0;
            foreach (Room room in rooms)
            {
                room.roomID = i;
                ++i;
                foreach (Exit exit in room.exits)
                {
                    exit.roomID = i;
                }
            }
        }

        public bool IsValidLine(Line line) 
        {
            for(int y = line.yStart; y<Math.Abs(line.xStart - line.xEnd); ++y)
            {
                for(int x = line.xStart; x<Math.Abs(line.yStart - line.yEnd); ++x)
                {
                    if (mapplace[x,y] || line.xStart<5 || line.yStart < 5) return false;
                }
            }
            return true;
        }

        public Map(int maxX, int maxY)
        { //конструктор (under construction)
            this.cellMap = new CellInfo[maxX, maxY];
            this.rooms = new List<Room>();
            this.maxX = maxX;
            this.maxY = maxY;
        }
    }
}
