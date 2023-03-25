﻿using System;
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
        MainVSpot = '/',
        SecondVSpot = '\\',
        Exit = '+',
        Enemy = 3,
        Chest = 4,
        Shop = 5,
        Player = 6
    }
}
