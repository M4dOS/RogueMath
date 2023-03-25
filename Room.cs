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
        List<Exit> exits; //выходы комнаты
        bool isExplored; //исследована ли комната?
        RoomID roomType; //id комнаты
        List<CellInfo> objects; //список доп. обьектов на карте
        public Room(int x, int y, int wight, int height) 
        {
            this.x = x;
            this.y = y;
            this.wigth = wight;
            this.height = height;
            this.isExplored = false;
        }
    }
}
