using System;
using System.Collections.Generic;
using System.IO;


public class Character
{
    protected Race r;
    protected int _hp;
    protected int _def;
    protected int _atk;
    protected int _nerves;
    protected Weapon? w;///
    protected int _lvl;
    protected short x;
    protected short y;
    protected List<Buff>? buffs;
    protected int _exp;
    protected int _gold;
}

public class Enemy : Character
{
    public Enemy(int _lvl, Race r)
    {
        switch (r) //добавить потом ещё рвсс врагов
        {
            case Race.Math:
                _hp = 10;
                _nerves = 5;
                _atk = 5;
                _def = 0;
                _exp = 0;
                _gold = 0;
                break;
        }

        this._lvl = _lvl;
        _hp = _hp + _lvl * 3;
        _nerves = _nerves + _lvl * 2;
        _atk = _atk + _lvl * 5;
        _def = _def + _lvl * 2;
    }
}

public class Player : Character
{
    public Player(int _lvl, Race r)
    {
        switch (r) //добавить потом ещё рвсс врагов
        {
            case Race.Human:
                _hp = 11;
                _nerves = 10;
                _atk = 5;
                _def = 0;
                _exp = 0;
                _gold = 0;
                break;
        }
        this._lvl = _lvl;
        _hp = _hp + _lvl * 3;
        _nerves = _nerves + _lvl * 2;
        _atk = _atk + _lvl * 5;
        _def = _def + _lvl * 2;
    }
    private int LvlUp(int _lvl)
    {
        return _lvl * 10;
    }
    public int exp
    {
        set
        {
            _exp += value;
            if (_exp > LvlUp(_lvl + 1))
            {
                ++_lvl;
                _exp -= LvlUp(_lvl + 1);
            }
        }
        get { return _exp; }
    }
}

public class Weapon
{

}

public class Buff
{

}

public enum Race
{
    Math = -1, //враг
    Human = 0, // персонаж
    Mather = 1 // босс
}

internal class Program
{
    static void Main(string[] args)
    {
        // Enemy a = new Enemy(1, 5, 4, 3);

    }
}