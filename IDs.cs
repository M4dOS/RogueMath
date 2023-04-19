
namespace RogueMath
{

    enum RoomType //id комнаты 
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
        
        Mather = 0, //босс


        //персонажи

        Student = 1,
        СoffeeLover = 2,
        Deadline = 3
    }
    enum CellID //id клетки 
    {
        //пустота
        Void = '.',

        //тестовые
        Status = '□',

        //по ним нельзя ходить 
        VWall = '│',
        HWall = '—',
        MainVSpot = '0',
        SecondVSpot = '1',

        //по ним можно ходить
        None = ' ',
        Teleport = '✦',
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
        Student = '@',
        СoffeeLover = '☕',
        Deadline = '⏱'
    }

}