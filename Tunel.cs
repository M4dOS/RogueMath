namespace RogueMath
{
    internal class Tunel //проход между комнатами 
    {
        protected Exit exit1; //один конец
        protected Exit exit2; //второй конец
        public List<Line> lines; //система линий
        public int length; //длина
        public Tunel(Exit exit1, Exit exit2, List<Line> lines) //создание
        {
            this.exit1 = exit1;
            this.exit2 = exit2;
            this.lines = lines;
            length = CalcLength();
        }

        public Tunel() //создание пустышки
        {
            exit1 = new();
            exit2 = new();
            lines = new List<Line>();
            length = -1;
        }
        protected int CalcLength() //вычисление длины туннеля
        {
            int length = 0;
            foreach (Line line in lines) { length += line.length; }
            return length;
        }

        public void Exploring(Player player, Map map)
        {
            foreach (Tunel tunel in map.tunels)
            {
                foreach (Line line in tunel.lines)
                {
                    line.Exploring(player);
                    if (line.isExplored)
                    {
                        for (int x = line.xStart; x < line.xEnd; x++)
                        {
                            for (int y = line.yStart; y < line.yEnd; y++)
                            {
                                map.AddChange(new(x, y, CellID.Tunel));
                            }
                        }
                        line.isExplored = false;
                    }
                }
            }
        }
    }
}
