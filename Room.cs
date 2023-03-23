using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueMath
{
    internal class Room
    {
        short x,y;
        Lines width;
        Lines height;
        List<Exit> exits;
        bool isExplored;
        RoomID roomType;
        public Room() 
        {

        }
    }
}
