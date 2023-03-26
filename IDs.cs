using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueMath
{
    enum RoomID //id комнаты
    {
        Bonus = -2,
        Enemy = -1,
        Empty = 0,
        Mather = 1,
        Shop = 2
    }

    enum CellID //id клетки
    {
        Void = '.',
        None = ' ',
        VWall = '|',
        HWall = '-',
        MainVSpot = '0',
        SecondVSpot = '1',
        Exit = '+',
        Enemy = 'x',
        Chest = '!',
        Shop = '$',
        Player = '@'
    }
}
