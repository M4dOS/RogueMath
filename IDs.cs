using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueMath
{
    enum RoomID
    {
        Bonus = -2,
        Enemy = -1,
        Empty = 0,
        Mather = 1,
        Shop = 2
    }

    enum CellID
    {
        None = 0,
        Wall = 1,
        Exit = 2,
        Enemy = 3,
        Chest = 4,
        Shop = 5
    }
}
