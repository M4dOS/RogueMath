
namespace RogueMath
{
    enum RoomType //id комнаты
    {
        Bonus = -2, //бонус-комната
        Enemy = -1, //комната с врагами
        Empty = 0, //пустая комната (комната спавна она такая)
        Mather = 1, //комната с боссом
        Shop = 2 //комната с магазином
    }
    public enum Race
    {
        Math = -1, //враг
        Human = 0, // персонаж
        Mather = 1 // босс
    }

    enum CellID //id клетки
    {
        //пустота
        Void = '⋆',

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
        Enemy = 'x',

        //для внутрикартовых обьектов
        Chest = '!',
        Shop = '$',

        //для классов игрока
        Player = '@',
    }

}
