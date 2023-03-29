namespace RogueMath
{
    enum RoomType //id комнаты
    {
        Bonus = -2,
        Enemy = -1,
        Empty = 0,
        Mather = 1,
        Shop = 2
    }

    enum CellID //id клетки
    {
        //пустота
        Void = '.',
        
        //стены
        VWall = '|',
        HWall = '-',
        MainVSpot = '0',
        SecondVSpot = '1',

        //другие обьекты
        None = ' ',
        ExitOpen = '+',
        ExitClose = '±',
        Tunel = '#',

        Enemy = 'x',
        Chest = '!',
        Shop = '$',
        Player = '@',
    }
}
