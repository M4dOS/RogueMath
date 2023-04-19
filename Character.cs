
using System;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Linq;
using Windows.System.Threading.Core;
using RogueMath.Item_Pack;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Windows.Networking.Sockets;
using Windows.Foundation.Diagnostics;
using System.Reflection;
using Windows.UI.Core;

namespace RogueMath
{
    internal class Character
    {
        public Race r;
        public int _maxHp;
        public int _hp;
        public int _def;
        public int _atk;
        public int _energy;
        public int _maxEnergy;


        //пока оружек нет, можно удалить
        //protected Weapon? w;


        public int _lvl;
        public int x;
        public int y;
        public int roomIDin;

        //временных эффектов тоже (Batle effects), могу попробовать добавить позже
       //protected List<Buff>? buffs;

        public int _exp;
        public int _gold;
       public CellID sign;
       
        public enum Phase // фаза
        {
            Adventure,
            Battle
        }
        
        public void Stats(Map map)
        {
            int forCx = Console.CursorLeft; int forCy = Console.CursorTop;

            Console.SetCursorPosition(5, map.maxY - 4);
            for (int i = 5; i < (map.maxX - 5); i++)
            {
                Console.Write(" ");
            }

            Console.SetCursorPosition(5, map.maxY - 4);
            Console.Write($"LVL:{_lvl} EXP:{_exp} HP:{_hp}/{_maxHp} DEF:{_def} ENG:{_energy}/{_maxEnergy} ATK:{_atk} GOLD:{_gold}");

            if (roomIDin > -1 && map.rooms[roomIDin].enemies.Count != 0 && !map.rooms[roomIDin].enemies[0].dead)
            {
                Enemy enemy = map.rooms[roomIDin].enemies[0];
                string stateLine = $"LVL:{enemy._lvl} EXP:{enemy._exp} HP:{enemy._hp}/{enemy._maxHp} DEF:{enemy._def} ENG:{enemy._energy}/{enemy._maxEnergy} ATK:{enemy._atk} GOLD:{enemy._gold}";
                Console.CursorLeft = map.maxX - 5 - stateLine.Length;
                Console.Write(stateLine);
            }

            Console.SetCursorPosition(forCx, forCy);
        }
        
        public void Bite(Character character)
        {
            if (_atk - character._def < 1) 
            { 
                --character._hp; 
            }
            else character._hp -= _atk - character._def;
        }
        public void ULTRABite(Character character)
        {
            if (_energy > 0)
            {
                if (_atk + (_energy / 2) - character._def < 1) --character._hp;
                else character._hp -= _atk + (_energy / 2) - character._def;
                if (_energy % 2 == 0)
                {
                    _energy /= 2;
                }
                else
                {
                    _energy = (_energy / 2) + 1;
                }
            }
        }
    }
    
    internal class Enemy : Character
    {
        public bool dead;

        public Enemy(int _lvl, Race r, int x, int y)
        {
            switch (r) //добавить потом ещё рвсс врагов
            {
                case Race.Math:
                    this._lvl = 1;
                    _hp = 50;
                    _energy = 10;
                    _maxHp = _hp;
                    _maxEnergy = _energy;
                    _atk = 10;
                    _def = 0;
                    _exp = 5;
                    _gold = 5;
                    sign = CellID.Enemy;
                    this.r = r;
                    break;
                case Race.Mather:
                    this._lvl = 1;
                    _hp = 150;
                    _maxHp = _hp;
                    _maxEnergy = _energy;
                    _energy = 100;
                    _atk = 35;
                    _def = 10;
                    _exp = 20;
                    _gold = 20;
                    sign = CellID.Boss;
                    this.r = r;
                    break;
            }
            this._lvl = _lvl;
            if (_lvl > 1)
            {
                _hp = _hp + (_lvl * 3);
                _energy = _energy + (_lvl * 2);
                _atk = _atk + (_lvl * 5);
                _def = _def + (_lvl * 2);
            }
            this.x = x;
            this.y = y;
            dead = false;
        }
        
        public bool Movement(Map map, Player player) // движение монстрика
        {
            if (dead) return false;

            Random random = new Random();
            int temp_x = x;
            int temp_y = y;
            switch (random.Next(1, 4 + 1))
            {
                case 1:
                    ++temp_x;
                    break;
                case 2:
                    --temp_x;
                    break;
                case 3:
                    ++temp_y;
                    break;
                case 4:
                    --temp_y;
                    break;
                default: break;
            }

            List<CellID> players = new List<CellID>() { CellID.Student, CellID.СoffeeLover, CellID.Deadline };

            if (map.cellMap[temp_x, temp_y].cellID == CellID.None && !players.Contains(map.cellMap[temp_x, temp_y].cellID))
            {
                bool condition = true;
                foreach (CellInfo check in map.changesForCellMap)
                {
                    if (players.Contains(check.cellID) )
                    {
                        if (check.x == temp_x && check.y == temp_y) { condition = false; break; }
                    }
                }
                if (condition)
                {
                    map.AddChange(new(temp_x, temp_y, map.cellMap[x, y].cellID) { enemyId = map.cellMap[x, y].enemyId });
                    map.AddChange(new(x, y, map.cellMap[temp_x, temp_y].cellID) { enemyId = map.cellMap[temp_x, temp_y].enemyId });
                    y = temp_y;
                    x = temp_x;
                    return true;
                }
                else return false;
            }
            else return false;

        }

        //дроп с врага
        public void EnemyLootItem(Player p)
        {
            Random rnd = new Random();
            int value_gold = rnd.Next(1, 5);
            int value_exp = rnd.Next(5, 10);



            //Artefact art = rnd.Next();
            switch (value_gold)
            {
                case 1:
                break;
                default:
                break;
            }
            
        }
        
    }

    internal class Player : Character
    {
        public Inventory inventory;
        public int battlingWith;
        public Phase phase;
        public bool dead;
        public Player(Race r, int x, int y)//устаревшее 
        {
            //this.inventory = new Inventory();
            Health_Cons hp_potions = new Health_Cons(15, 5, "вкусняхи");
            Energy_Cons en_potions = new Energy_Cons(10, 5, "кофе");
            List<Item> items = new List<Item>();
            List<Artefact> arts_equiped = new List<Artefact>();
            this.r = r;
            switch (this.r) //добавить потом ещё расс врагов
            {
                case Race.Student:
                    sign = CellID.Student;
                    _lvl = 1;
                    _hp = 1;
                    _maxHp = 11;
                    _energy = 1;
                    _maxEnergy = 10;
                    _atk = 5;
                    _def = 0;
                    _exp = 0;
                    _gold = 0;
                    break;
                case Race.СoffeeLover:
                    sign = CellID.СoffeeLover;
                    _lvl = 1;
                    _hp = 1;
                    _maxHp = 11;
                    _energy = 1;
                    _maxEnergy = 10;
                    _atk = 5;
                    _def = 0;
                    _exp = 0;
                    _gold = 0;
                    break;
                case Race.Deadline:
                    sign = CellID.Deadline;
                    _lvl = 1;
                    _hp = 50;
                    _maxHp = 100;
                    _energy = 50;
                    _maxEnergy = 100;
                    _atk = 20;
                    _def = 0;
                    _exp = 0;
                    _gold = 0;
                    break;
            }
            this.x = x;
            this.y = y;
            battlingWith = -1;
            phase = Phase.Adventure;
            this.inventory = new Inventory(6, hp_potions, en_potions, arts_equiped);
        }

        public Player(Map map, int index)
        {
            this.r = RaceOfPlayer(map.floor);
            //this.inventory = new Inventory();
            Health_Cons hp_potions = new Health_Cons(15, 5, "вкусняхи");
            Energy_Cons en_potions = new Energy_Cons(10, 5, "кофе");
            List<Item> items = new List<Item>();
            List<Artefact> arts_equiped = new List<Artefact>();
            roomIDin = index;
            dead = false;
            switch (this.r) //добавить потом ещё расс врагов
            {
                
                case Race.Student:
                    sign = CellID.Student;
                    _lvl = 1;
                    _hp = 50;
                    _maxHp = 100;
                    _energy = 1;
                    _maxEnergy = 20;
                    _atk = 5;
                    _def = 10;
                    _exp = 0;
                    _gold = 0;
                    break;
                case Race.СoffeeLover:
                    sign = CellID.СoffeeLover;
                    _lvl = 1;
                    _hp = 75;
                    _maxHp = 150;
                    _energy = 1;
                    _maxEnergy = 50;
                    _atk = 10;
                    _def = 5;
                    _exp = 0;
                    _gold = 0;
                    break;
                case Race.Deadline:
                    sign = CellID.Deadline;
                    _lvl = 1;
                    _hp = 150;
                    _maxHp = 200;
                    _energy = 50;
                    _maxEnergy = 100;
                    _atk = 15;
                    _def = 0;
                    _exp = 0;
                    _gold = 0;
                    break;
            }
            this.x = ((2 * map.rooms[index].x) + map.rooms[index].wigth) / 2;
            this.y = ((2 * map.rooms[index].y) + map.rooms[index].height) / 2;
            battlingWith = -1;
            map.rooms[index].Exploring(this, map);
            phase = Phase.Adventure;
            this.inventory = new Inventory(6, hp_potions, en_potions, arts_equiped);
        }
        private int LvlUp(int _lvl) // опеределение кол-ва опыта для апа уровня
        {
            return _lvl * 10;
        }
        public int exp //ап уровня
        {
            set
            {
                _exp += value;
                if (_exp > LvlUp(_lvl + 1))
                {
                    ++_lvl;
                    _exp -= LvlUp(_lvl + 1);
                    _maxHp = _maxHp + (_lvl * 3);
                    _hp = _maxHp;
                    _maxEnergy = _maxEnergy + (_lvl * 2);
                    _energy = _maxEnergy;
                    _atk = _atk + (_lvl * 5);
                    _def = _def + (_lvl * 2);
                }
            }
            get { return _exp; }
        }
        CellID tempCell = CellID.None;
        public bool Movement(Map map) // движение чела
        {

            ConsoleKeyInfo consoleKey = Console.ReadKey(true);
            int temp_x = x;
            int temp_y = y;
            List<ConsoleKey> consoleKeysList = new() { ConsoleKey.W, ConsoleKey.A, ConsoleKey.S,ConsoleKey.D, ConsoleKey.Z,
                                                       /*,ConsoleKey.UpArrow, ConsoleKey.LeftArrow, ConsoleKey.DownArrow, ConsoleKey.RightArrow*/};

            switch (consoleKey.Key)
            {
                /*case ConsoleKey.UpArrow:*/
                case ConsoleKey.W:
                    --temp_y;
                    break;

                /*case ConsoleKey.LeftArrow:*/

                case ConsoleKey.A:
                    --temp_x;
                    break;


                /*case ConsoleKey.DownArrow:*/

                case ConsoleKey.S:
                    ++temp_y;
                    break;


                /*case ConsoleKey.RightArrow:*/

                case ConsoleKey.D:
                    ++temp_x;
                    break;

                case ConsoleKey.Z:

                    List<Effect> effects = Inventory.ReadEffects("Item_Pack/Const_Effects.txt");
                    List<Artefact> arts = Inventory.ReadArtefacts("Item_Pack/Arts.txt", effects);
                    inventory.InventoryShow(map, this,  effects, arts);
                    break;

                default: break;
            }

            if (map.cellMap[temp_x, temp_y].cellID == CellID.Teleport) return false;
            if ((map.cellMap[temp_x, temp_y].isStepible && consoleKeysList.Contains(consoleKey.Key)) || (temp_x != x && temp_y != y))
            {
                map.AddChange(new(x, y, tempCell));
                y = temp_y;
                x = temp_x;
                tempCell = map.cellMap[x, y].cellID;

                foreach (Room room in map.rooms) { room.Exploring(this, map); }
                foreach (Tunel tunel in map.tunels) { tunel.Exploring(this, map); }

                map.AddChange(new(x, y, sign));
                return true;
            }
            else return false;
        }
        public int EnemyCheck(Map map) // проверка на врага
        {
            List<CellID> enemyIDs = new List<CellID>() { CellID.Enemy, CellID.Boss };
            if (enemyIDs.Contains(map.cellMap[x + 1, y].cellID)) return map.cellMap[x + 1, y].enemyId;
            else if (enemyIDs.Contains(map.cellMap[x - 1, y].cellID)) return map.cellMap[x - 1, y].enemyId;
            else if (enemyIDs.Contains(map.cellMap[x, y + 1].cellID)) return map.cellMap[x, y + 1].enemyId;
            else if (enemyIDs.Contains(map.cellMap[x, y - 1].cellID)) return map.cellMap[x, y - 1].enemyId;
            else return -1;
        }
        public bool TeleportCheck(Map map) // проверка на врага
        {
            for(int ix = x-1; ix <= x + 1; ix++)
            {
                for(int iy = y - 1; iy <= y + 1; iy++)
                {
                    if (map.cellMap[ix, iy].cellID == CellID.Teleport) return true;
                }
            }
            return false;
        }
        public void GetArtLoot(Player player, Map map) // дает дроп с врага при вызове (или не даёт, как повезёт с: )
        {
            static List<Effect> ReadEffect(string filename)
            {
                StreamReader sr = new StreamReader(filename);
                List<Effect> effects = new List<Effect>();

                string line;
                // Read and display lines from the file until the end of
                // the file is reached.
                while ((line = sr.ReadLine()) != null)
                {
                    string[] effectArray = line.Split("|");

                    if (effectArray.Length == 4)
                    {
                        PermanentEffect effect = new PermanentEffect(
                            Convert.ToInt32(effectArray[0]),
                            effectArray[1],
                            Convert.ToInt32(effectArray[2]),
                            effectArray[3]
                        );
                        effects.Add(effect);
                    }
                }

                return effects;
            }
            static List<Artefact> ReadArtefact(string filename)
            {
                StreamReader sr = new StreamReader(filename);
                List<Artefact> artefacts = new List<Artefact>();

                List<Effect> effects = ReadEffect("Item_Pack/Const_Effects.txt");

                string line;
                // Read and display lines from the file until the end of
                // the file is reached.
                while ((line = sr.ReadLine()) != null)
                {
                    string[] artLine = line.Split("|");

                    if (artLine.Length == 6)
                    {
                        Artefact art = new Artefact(
                            Convert.ToInt32(artLine[0]),
                            Convert.ToInt32(artLine[1]),
                            artLine[2],
                            artLine[3],
                            Convert.ToInt32(artLine[4])
                        );

                        string[] artEffects = artLine[5].Split(",");

                        for (int i = 0; i < artEffects.Length; i++)
                        {
                            foreach (Effect effect in effects)
                            {
                                if (effect.Id_effect == Convert.ToInt32(artEffects[i]))
                                {
                                    art.AddEffect(effect);
                                    break;
                                }
                            }
                        }

                        artefacts.Add(art);
                    }
                }

                return artefacts;
            }

            List<Effect> effects = ReadEffect("Item_Pack/Const_Effects.txt");
            List<Artefact> all_art = ReadArtefact("Item_Pack/Arts.txt");
            List<Artefact> poor_pool = all_art.Where(art => art.quality_art == 1 || art.quality_art == 2).ToList();
            List<Artefact> good_pool = all_art.Where(art => art.quality_art == 3 || art.quality_art == 4).ToList();
            Random rnd = new Random();

            player.exp = +5;
            player._gold = +5;
            int lohotron = rnd.Next(1, 9);
            switch (lohotron)
            {
                    case 1:
                    {
                        player.inventory.AddArt_Random(map, player, player.inventory.arts_equiped, poor_pool);
                        player.inventory.AddArt_Random(map, player, player.inventory.arts_equiped, poor_pool);
                        inventory.Notification("Вы нашли две прикольные штучки", map);
                        break;
                    }
                    case 2:
                    {
                        player.inventory.AddArt_Random(map, player, player.inventory.arts_equiped, poor_pool);
                        inventory.Notification("Вы нашли мощный артефакт", map);
                        break;
                    } 
                    case 3:
                    {
                        inventory.Notification("Ты попался на кликбейт", map);
                        break;
                    }
                    case 4:
                    {
                        player._gold = +15;
                        inventory.Notification("С монстра выпало немало стипы", map);
                        break;
                    }
                   case 5:
                    {
                        player.inventory.hp_potions.Get_Cons(map, player);
                        break;
                    }
                   case 6:
                    {
                        player.inventory.en_potions.Get_Cons(map, player);
                        break;
                    }
                case 7:
                    {
                        inventory.Notification("Звезды говорят: тот, кто нажмет на цифру '7' во время игры, станет лошком-пирожком", map);
                        break;
                    }
                case 8:
                    {
                        player.inventory.AddArt_Random(map, player, player.inventory.arts_equiped, poor_pool);
                        inventory.Notification("Вы нашли прикольную штуку", map);
                        break;
                    }
                case 9:
                    {
                        player.inventory.AddArt_Random(map, player, player.inventory.arts_equiped, poor_pool);
                        inventory.Notification("Вы нашли прикольную штуку", map);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        public void ViewFloor(Map map)
        {
            string floorText = $"Floor {map.floor}/5";
            Map.Frame((map.maxX-floorText.Length)/2-1, 2, (map.maxX + floorText.Length) / 2+2, 2+2);
            Console.SetCursorPosition(map.maxX / 2 - floorText.Length / 2 + 1, 2 + 1);
            Console.Write(floorText);
        }

        public bool Advenchuring(Map map)
        {

            Stats(map);

            ViewFloor(map);

            Player player = this;

            player.inventory.countOfStepsToClear++;
            if (this.inventory.countOfStepsToClear > 10) this.inventory.ClearNotification();
            if (player.phase == Player.Phase.Adventure)
            {
                if (player._hp < player._maxHp) ++player._hp;
                else if (player._energy < player._maxEnergy /*&& player._hp < player._maxHp*/) ++player._energy;

                if (player.Movement(map))
                {
                    if (TeleportCheck(map)) 
                    { 
                        this.inventory.Notification("Хотите перейти на следующий уровень? Нажмите Е рядом с телепортом для входа", map);
                        if (Console.ReadKey().Key == ConsoleKey.E)
                        {
                            return false;
                        }
                        else this.inventory.ClearNotification();
                    }
                    foreach(Room room in map.rooms)
                    {
                        foreach(Enemy enemy in room.enemies)
                        {
                            enemy.Movement(map, player);
                        }
                    }
                    /*map.AddChange(new(player.x, player.y, CellID.Player));*/
                    map.Update();
                }
                if (tempCell == CellID.Teleport) {  return false; }
                player.battlingWith = player.EnemyCheck(map);
                if (player.battlingWith > -1) player.phase = Player.Phase.Battle; 
            }

            else if (player.phase == Player.Phase.Battle)
            {
                Console.Clear();
                Stats(map);
                Pixel.DrawBorder();
                Pixel.DrawAtack();
                Pixel.DrawInvent();
                Pixel.DrawUltraAtack();

                switch (player.r)
                {
                    case Race.Student:
                        Pixel.DrawType1();
                        Pixel.Draw_1_Char/*_d*/();
                        break;
                    case Race.СoffeeLover:
                        Pixel.DrawType2();
                        Pixel.Draw_2_Char/*_d*/();
                        break;
                    case Race.Deadline:
                        Pixel.DrawType3();
                        Pixel.Draw_3_Char/*_d*/();
                        break;
                }


                switch (map.rooms[player.roomIDin].enemies[player.battlingWith].r)
                {
                    case Race.Math:
                        Pixel.DrawEnemyX();
                        Pixel.DrawEnemy1();
                        break;
                    case Race.Mather:
                        Pixel.DrawEnemy2();
                        Pixel.DrawEnemyINT();
                        break;
                }

                if (player.Battle(map.rooms[player.roomIDin].enemies[0], map))
                {
                    if (map.rooms[player.roomIDin].enemies[player.battlingWith]._hp > 0 && this._hp > 0) map.rooms[player.roomIDin].enemies[player.battlingWith].Bite(player);
                    else if (player.dead) return false;
                    else
                    {
                        int deadX = map.rooms[player.roomIDin].enemies[player.battlingWith].x;
                        int deadY = map.rooms[player.roomIDin].enemies[player.battlingWith].y;

                        //GetArtLoot(player, map);

                        map.AddChange(new(map.rooms[player.roomIDin].enemies[player.battlingWith].x, map.rooms[player.roomIDin].enemies[player.battlingWith].y, CellID.None));
                        player.phase = Player.Phase.Adventure;
                        map.rooms[player.roomIDin].enemies[player.battlingWith].dead = true;
                        map.rooms[player.roomIDin].ChangeDoorsStatus(map, player);
                        player.battlingWith = -1;


                        /*код для дропа*/
                        GetArtLoot(player, map);

                        map.PrintMap();
                        map.Update();
                    }

                    
                }
            }
            return true;
        }

        public static Race RaceOfPlayer(int floor)
        {
            switch (floor)
            {
                case 1:
                    return Race.Student;
                    break;
                case 2:
                case 3:
                    return Race.СoffeeLover;
                    break;
                case 4:
                case 5:
                    return Race.Deadline;
                    break;

                default: return Race.Mather; break;
            }
        }

        public bool Battle(Enemy enemy, Map map) //фаза боя
        {
            if (this._hp <= 0 || this.dead)
            {
                this.dead = true;
                Console.Clear();
                string text1 = "Вот и пришёл конец странствиям по бесконечным мирам...";
                string text2 = "Матанализ будет помнить тебя вечно и станет твоим пристанищем навеки вечные...";
                string text3 = "Нажми любую кнопку, чтобы начать сначала или нажми ESC для выхода";
                Console.SetCursorPosition(map.maxX / 2 - text1.Length / 2, map.maxY / 2 - 2);
                Console.WriteLine(text1);
                Console.CursorLeft = map.maxX / 2 - text2.Length / 2;
                Console.WriteLine(text2);
                Console.CursorTop += 2;
                Console.CursorLeft = map.maxX / 2 - text3.Length / 2;
                Console.WriteLine(text3);
                ConsoleKey key = Console.ReadKey().Key;
                if (key == ConsoleKey.Escape) Environment.Exit(0);
                else return true;

            }
            bool result = false;
            ConsoleKeyInfo consoleKey = Console.ReadKey(true);
            switch (consoleKey.Key)
            {
                case ConsoleKey.E:
                    switch (r)
                    {
                        case Race.Student:
                            Pixel.Draw_1_CharBite/*_d*/();
                            break;
                        case Race.СoffeeLover:
                            Pixel.Draw_2_CharBite/*_d*/();
                            break;
                        case Race.Deadline:
                            Pixel.Draw_3_CharBite/*_d*/();
                            break;
                    }
                    Bite(enemy); //кусь
                    result = true;
                    break;
                case ConsoleKey.Q:
                    switch (r)
                    {
                        case Race.Student:
                            Pixel.Draw_1_CharUltraBite/*_d*/();
                            break;
                        case Race.СoffeeLover:
                            Pixel.Draw_2_CharUltraBite/*_d*/();
                            break;
                        case Race.Deadline:
                            Pixel.Draw_3_CharUltraBite/*_d*/();
                            break;
                    }
                    ULTRABite(enemy);//УЛЬТРАКУСЬ
                    result = true;
                    break;
            }
            return result;
        }
    }
}
