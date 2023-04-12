
namespace RogueMath
{
    internal enum RoomType //id комнаты 
    {
        Bonus = -2, //бонус-комната
        Enemy = -1, //комната с врагами
        Spawn = 0, //пустая комната (спавн)
        Mather = 1, //комната с боссом
        Shop = 2, //комната с магазином
        Empty = 3 //пустая комната
    }
    public enum Race //раса 
    {
        Math = -1, //враг
        Human = 0, //персонаж
        Mather = 1 //босс
    }

    internal enum CellID //id клетки 
    {
        //тестовые
        Status = '□',

        //пустота
        Void = '.',

        //по ним нельзя ходить 
        VWall = '│',
        HWall = '—',
        MainVSpot = '0',
        SecondVSpot = '1',

        //по ним можно ходить
        None = ' ',
        ExitOpen = '+',
        ExitClose = '±',
        Tunel = '#',

        //для классов врагов
        Enemy = 'F',
        Boss = '∫',

        //для внутрикартовых обьектов
        Chest = '!',
        Shop = '$',

        //для классов игрока
        Player = '@'
    }

}