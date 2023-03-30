using System.Diagnostics;

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
            for (int y = 0; y < this.maxY; y++)
            {
                for (int x = 0; x < this.maxX; x++)
                {
                    //прорисовка комнат
                    if (x == this.maxX - 1 && y == 0 || y == this.maxY - 1 && x == 0) //углы побочной диагонали
                    {
                        cellMap[x, y] = new CellInfo(x, y, CellID.SecondVSpot);
                    }
                    else if (y == 0 && x == 0 || x == this.maxX - 1 && y == this.maxY - 1) //углы главной диагонали
                    {
                        cellMap[x, y] = new CellInfo(x, y, CellID.MainVSpot);
                    }
                    else if (y == this.maxY - 1 || y == 0) //горизонтальные стены
                    {
                        cellMap[x, y] = new CellInfo(x, y, CellID.HWall);
                    }
                    else if (x == this.maxX - 1 || x == 0) //вертикальные стены
                    {
                        cellMap[x, y] = new CellInfo(x, y, CellID.VWall);
                    }

                    //прорисовка окошка для счётчиков
                    else if (y == maxY - 4 - 1 && x == 3 || y == maxY - 2 - 1 && x == maxX - 3 - 1) //углы главной диагонали
                    {
                        cellMap[x, y] = new CellInfo(x, y, CellID.SecondVSpot);
                    }
                    else if (y == maxY - 4 - 1 && (x==3 || x == maxX - 3 - 1) || y == maxY - 2 - 1 && (x == 3 || x == maxX - 3 - 1)) //углы побочной диагонали
                    {
                        cellMap[x, y] = new CellInfo(x, y, CellID.MainVSpot);
                    }
                    else if (y > maxY - 4 - 1 && y < maxY - 2 - 1 && (x==3 || x== maxX - 3 - 1)) //вертикальные стены
                    {
                        cellMap[x, y] = new CellInfo(x, y, CellID.VWall);
                    }
                    else if( x>3 && x<maxX-3 - 1 && (y == maxY - 4- 1 || y==maxY - 2 - 1))
                    {
                        cellMap[x, y] = new CellInfo(x, y, CellID.HWall);
                    }
                    else if (x > 3 && x < maxX - 3 - 1 && y > maxY - 4 - 1 && y < maxY - 2 - 1)
                    {
                        cellMap[x, y] = new CellInfo(x, y, CellID.None);
                    }

                    //заполнение пустотой
                    else
                    {
                        cellMap[x, y] = new CellInfo(x, y, CellID.Void);
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
                        if (x == room.wigth + room.x - 1 && y == room.y || y == room.height + room.y - 1 && x == room.x) //углы побочной диагонали
                        {
                            cellMap[x, y] = new CellInfo(x, y, CellID.SecondVSpot);
                        }
                        else if (y == room.y && x == room.x || x == room.wigth + room.x - 1 && y == room.height + room.y - 1) //углы главной диагонали
                        {
                            cellMap[x, y] = new CellInfo(x, y, CellID.MainVSpot);
                        }
                        else if (y == room.height + room.y - 1 || y == room.y) //горизонтальные стены
                        {
                            cellMap[x, y] = new CellInfo(x, y, CellID.HWall);
                        }
                        else if (x == room.wigth + room.x - 1 || x == room.x) //вертикальные стены
                        {
                            cellMap[x, y] = new CellInfo(x, y, CellID.VWall);
                        }
                        else
                        {
                            cellMap[x, y] = new CellInfo(x, y, CellID.None);
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
            bool isDebug = true;

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
                    Room room0 = new(rand.Next(edge + 1, maxX - edge + 1), rand.Next(edge + 1, maxY - 10 + 1), rand.Next(minRoomX, maxRoomX + 1), rand.Next(minRoomY, maxRoomY + 1))
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

                if (isDebug)
                {
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
                }
                
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
            bool isDebug = false;

            Exit exitFrom;
            Exit exitTo;

            List<Exit> exitList = new();

            foreach(Room room in rooms)
            {
                for(int y = room.y - edge; y<room.y + room.height + edge + 1; ++y)
                {
                    for(int x = room.x - edge;x<room.y+room.height + edge + 1; ++x)
                    {
                        mapplace[x,y] = false;
                    }
                }

                if (isDebug)
                {
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
                }

                foreach (Exit exit in room.exits)
                {
                    exitList.Add(exit);
                }
            }

            int shortDestination = maxX + maxY + 1;
            List<int> removeExitsID = new();

            foreach (Exit dot in exitList)
            {
                exitFrom = dot;
                exitTo = dot;

                bool findConnect = false;

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
                if (!findConnect) {rooms[dot.roomID].exits.Remove(dot); removeExitsID.Add(exitList.IndexOf(dot)); }

                Drawer(exitFrom, exitTo);
            }
            foreach (int remove in removeExitsID) exitList.RemoveAt(remove);
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
                    if (mapplace[x,y] || line.xStart < edge || line.yStart < edge) return false;
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
