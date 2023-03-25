using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueMath
{
    internal class Room
    {
        int x,y; //координаты
        Lines width; //длина
        Lines height; //ширина
        List<Exit> exits; //выходы комнаты
        bool isExplored; //исследована ли комната?
        RoomID roomType; //id комнаты
        List<CellInfo> objects; //список доп. обьектов на карте
        public Room() 
        {

        }
    }
}
