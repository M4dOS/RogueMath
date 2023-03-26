using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueMath
{
    internal class Room
    {
        //координаты
        public int x;
        public int y;

        public int wigth; //длина
        public int height; //ширина
        public List<Exit> exits; //выходы комнаты
        public bool isExplored; //исследована ли комната?
        RoomID roomType; //id комнаты
        public List<CellInfo> objects; //список доп. обьектов на карте
        public bool manual;
        public Room(int x, int y, int wight, int height)
        {
            Random rand = new Random();
            this.x = x;
            this.y = y;
            this.wigth = wight;
            this.height = height;
            this.isExplored = false;
            this.objects = new List<CellInfo>();
            this.exits = new List<Exit>();
            this.manual = true;

            while (exits.Count <= 1)
            {
                List<Exit> loc_exits = new List<Exit>();

                for(int i = 0; i < 15; ++i) 
                {
                    Exit exit0 = new Exit(rand.Next(x, x + wight + 1), rand.Next(y, y + height + 1));
                    loc_exits.Add(exit0);
                }

                List<Exit> WallN = new List<Exit>();
                List<Exit> WallS = new List<Exit>();
                List<Exit> WallW = new List<Exit>();
                List<Exit> WallE = new List<Exit>();

                foreach (Exit exit in loc_exits)
                {
                    if ( (exit.x == x || exit.y == y || exit.x == x + wight-1 || exit.y == y + height -1) //проверка на нахождение на стороне
                    && (exit.y >= y + 3 && exit.y <= y + height - 4 || exit.x >= x + 3 && exit.x <= x + wight - 4) ) //проверка что это не угол 
                    {
                        if (exit.x == x)
                        {
                            WallW.Add(exit);
                        }
                        else if (exit.y == y)
                        {
                            WallN.Add(exit);
                        }
                        else if (exit.x == x + wight - 1)
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

                List<List<Exit>> Walls = new List<List<Exit>> { WallN, WallS, WallW, WallE };
                foreach (List<Exit> ex in Walls)
                {
                    if(ex.Count < 2) //зачем проверять стену с <2 выходами
                    {
                        continue;
                    }

                    while (ex.Count > 2) //нужно не более 2 выходов на 1 стене
                    {
                        ex.RemoveAt(0);
                    }

                    //если расстояние выходов на одной стене маленькое - просто уберу одну из них

                    if ( Math.Abs(ex[0].x - ex[1].x) < 4 && ex == WallN || ex == WallS)
                    {
                        ex.RemoveAt(0);
                    }

                    else if ( Math.Abs(ex[0].y - ex[1].y) < 4 && ex == WallW || ex == WallE)
                    {
                        ex.RemoveAt(0);
                    }
                }
                
                //добавляем все отфильтрованные выходы в один список
                foreach(List<Exit> ex in Walls)
                {
                    foreach (Exit exit in ex)
                    {
                        exits.Add(exit);
                    }
                }

                //если вышло слишком много, отнимаем до 4
                if(exits.Count > 4) 
                { 
                    while (exits.Count > 4) 
                    { 
                        exits.RemoveAt(rand.Next(0,exits.Count));
                    }
                }
            }
        }
    }
}
