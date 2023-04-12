namespace RogueMath.Item_Pack
{
    internal abstract class Effect
    {
        public int Id_effect { get; set; }
        public string Name_effect { get; set; }
        public int Value_effect { get; set; }
        public bool IsPermanent_effect { get; set; }
        public Effect(int id_effect, string name, int value, bool isPermanent)
        {
            Id_effect = id_effect;
            Name_effect = name;
            Value_effect = value;
            IsPermanent_effect = isPermanent;
        }
    }

    //временные эффекты: пока их нет
    internal class BattleEffect : Effect
    {
        public int Duration { get; set; }
        public bool IsForEnemy { get; set; }
        public BattleEffect(int id_effect, string name, int value, int duration, bool isForEnemy) : base(id_effect, name, value, false)
        {
            Duration = duration;
            IsForEnemy = isForEnemy;
        }
    }

    //постоянные эффекты, навсегда увеличивают статы пока в инвенторе
    internal class PermanentEffect : Effect
    {
        public string Stat_type { get; set; }
        public PermanentEffect(int id_effect, string name, int value, string stat_type) : base(id_effect, name, value, true)
        {
            Stat_type = stat_type;
        }
    }
}
