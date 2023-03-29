namespace RogueMath
{
    internal class Map
    {
        protected int maxX, maxY; //границы карты
        protected int edge; //расстояние от полей карты
        protected CellInfo[,] cellMap; //карта "клеток" (вероятно будет двойным массивом(списком))
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

        protected bool[,] mapplace;

        protected void GenerateMap()
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
                    if(exit.isOpen) cellMap[exit.x, exit.y] = new CellInfo(exit.x, exit.y, CellID.ExitOpen);
                    else cellMap[exit.x, exit.y] = new CellInfo(exit.x, exit.y, CellID.ExitClose);
                }

                //генерация туннелей
                /*foreach(Room room1 in rooms)
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
                }*/

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

        private void RoomPlace()
        {
            Random rand = new();
            List<Room> loc_rooms = new();

            int minRooms = 5;
            int maxRooms = 12;

            int minRoomX = 20;
            int maxRoomX = 30;
            
            int minRoomY = 10;
            int maxRoomY = 25;

            int tries = 0;

            //this.mapplace = new bool[maxX, maxY];


            while (!(minRooms <= rooms.Count && rooms.Count <= maxRooms))
            {
                for (int i = 0; i < 150; ++i)
                {
                    Room room0 = new(rand.Next(edge, maxX - edge + 1), rand.Next(edge, maxY - 10 + 1), rand.Next(minRoomX, maxRoomX + 1), rand.Next(minRoomY, maxRoomY + 1))
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
                    for (int y = manual_room.y - edge; y < manual_room.y + manual_room.height + edge; ++y)
                    {
                        for (int x = manual_room.x - edge; x < manual_room.x + manual_room.wigth + edge; x++)
                        {
                            mapplace[x, y] = true;
                        }
                    }
                }

                foreach (Room room in loc_rooms)
                {
                    bool isThird = false;

                    for (int y = room.y - edge; y < room.y + room.height + edge; ++y)
                    {
                        if (isThird) { break; }
                        for (int x = room.x - edge; x < room.x + room.wigth + edge; x++)
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
                        for (int y = room.y - edge; y < room.y + room.height + edge; ++y)
                        {
                            for (int x = room.x - edge; x < room.x + room.wigth + edge; x++)
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
                Console.WriteLine("\n");
                Thread.Sleep(20);
#endif

                if (tries > 2)
                {
                    int manuals = 0;
                    foreach (Room room in rooms)
                    {
                        if (!room.manual)
                        {
                            for (int y = room.y - edge; y < room.y + room.height + edge; ++y)
                            {
                                for (int x = room.x - edge; x < room.x + room.wigth + edge; ++x)
                                {
                                    mapplace[x, y] = false;
                                }
                            }
                        }
                        else ++manuals;
                    }

                    while(rooms.Count > manuals)
                    {
                        for (int i = 0; i < rooms.Count; ++i)
                        {
                            if (!rooms[i].manual) {rooms.RemoveAt(i); break;}
                        }
                    }
                    loc_rooms.Clear();
                    tries = 0;
                }
            }

            CalibrateRoomIDs();

        }

        private void CreateWeb()
        {
            Exit exitFrom;
            Exit exitTo;

            List<Exit> exitList = new();

            foreach(Room room in rooms)
            {
                foreach (Exit exit in room.exits)
                {
                    exitList.Add(exit);
                }
            }

            int shortDestination = maxX + maxY + 1;

            foreach(Exit dot in exitList)
            {
                exitFrom = dot;
                exitTo = dot;

                bool findConnect = false;

                List<int> removeExitsID = new();

                for(int indexOfExit = 0; indexOfExit < exitList.Count; indexOfExit++)
                {
                    if (exitList[indexOfExit] == dot || exitList[indexOfExit].isConnected) continue;
                    foreach (int remove in removeExitsID){ if (indexOfExit == remove) continue; }

                    if (dot.Distance(exitList[indexOfExit]) < shortDestination)
                    {
                        exitTo = exitList[indexOfExit];
                        shortDestination = dot.Distance(exitTo);
                    }
                }
                if(exitTo != exitFrom) findConnect = true;
                if (!findConnect) {rooms[dot.roomID].exits.Remove(dot); exitList.IndexOf(dot); }

                foreach (int remove in removeExitsID) exitList.RemoveAt(remove);

                Drawer(exitFrom, exitTo);
            }
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

        private void Drawer(Exit exitA, Exit exitB)
        {
            Line line0;
            List<Line> lines = new();

            if (exitA.mode == 0)
            {

            }
        }
        protected void CalibrateRoomIDs()
        {
            int i = 0;
            foreach (Room room in rooms)
            {
                room.roomID = i;
                foreach (Exit exit in room.exits)
                {
                    exit.roomID = i;
                }
                ++i;
            }
        }

        protected bool IsValidLine(Line line) 
        {
            for(int y = line.yStart; y<Math.Abs(line.xStart - line.xEnd); ++y)
            {
                for(int x = line.xStart; x<Math.Abs(line.yStart - line.yEnd); ++x)
                {
                    if (mapplace[x,y] || line.xStart<edge || line.yStart < edge) return false;
                }
            }
            return true;
        }

        public Map(int maxX, int maxY, int edge)
        { //конструктор (under construction)
            this.cellMap = new CellInfo[maxX, maxY];
            this.rooms = new();
            this.maxX = maxX;
            this.maxY = maxY;
            this.edge = edge;
            this.mapplace = new bool[maxX, maxY];
            RoomPlace();
            //CreateWeb();
            GenerateMap();
            //PrintMap();
        }
    }
}
