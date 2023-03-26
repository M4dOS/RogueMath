using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                /*if (room.isExplored == false)
                {
                    for (int i = room.y; i < room.height + room.y; ++i)
                    {
                        for (int j = room.x; j < room.wigth + room.x; ++j)
                        {
                            cellMap[j, i] = new CellInfo(j, i, CellID.Void);
                        }
                    }
                }*/

                /*else
                {*/
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
                        cellMap[exit.x, exit.y] = new CellInfo(exit.x, exit.x, CellID.Exit);
                    }

                    //генерация доп. предметов на карте
                    foreach (CellInfo obj in room.objects)
                    {
                        cellMap[obj.x, obj.y] = new CellInfo(obj);
                    }
                /*}*/
            }
        }

        public void PrintMap()
        { //печать карты
            for (int j = 0; j < this.maxY;++j)
            {
                for(int i = 0; i < this.maxX; ++i)
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

            while(!(minRooms <= rooms.Count && rooms.Count <= maxRooms) )
            {
                for (int i = 0; i < 1500; ++i)
                {
                    Room room0 = new Room(rand.Next(5, maxX - 5 + 1), rand.Next(5, maxY - 10 + 1), rand.Next(15, 25 + 1), rand.Next(5, 20 + 1));
                    room0.manual = false;
                    if (room0.x + room0.wigth < maxX - 5 && room0.y + room0.height < maxY - 10)
                    {
                        loc_rooms.Add(room0);
                    }
                }

                bool[,] mapplace = new bool[this.maxX, this.maxY];
                List<bool> validID = new List<bool>();


                foreach (Room manual_room in rooms)
                {
                    for (int y = manual_room.y - 5; y < manual_room.y + manual_room.height; ++y)
                    {
                        for (int x = manual_room.x - 5; x < manual_room.x + manual_room.wigth; x++)
                        {
                                mapplace[x, y] = true;
                        }
                    }
                }

                foreach (Room room in loc_rooms)
                {
                    bool isThird = false;
                    for (int y = room.y - 5; y < room.y + room.height; ++y)
                    {
                        if (isThird) { break; }

                        for (int x = room.x - 5; x < room.x + room.wigth; x++)
                        {
                            if (!mapplace[x, y])
                            {
                                mapplace[x, y] = true;
                            }
                            else
                            {
                                isThird = true;
                            }
                        }
                    }
                    validID.Add(!isThird);
                }

                for (int i = 0; i < validID.Count; ++i)
                {
                    if (validID[i]) { rooms.Add(loc_rooms[i]); }
                }

                if(rooms.Count > 8)
                {
                    while(rooms.Count > 8)
                    {
                        Room room_x = rooms[rand.Next(rooms.Count)];
                        if (!room_x.manual)
                        {
                            rooms.Remove(room_x);
                        }
                    }
                }
            }
        }
            
        public Map(int maxX, int maxY) 
        { //конструктор (under construction)
            this.cellMap = new CellInfo[maxX,maxY];
            this.rooms = new List<Room>();
            this.maxX = maxX;
            this.maxY = maxY;
        }
    }
}
