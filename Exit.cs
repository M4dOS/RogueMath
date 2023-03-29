namespace RogueMath
{
    internal class Exit
    {
        public int x, y; //координаты
        /*protected int width; //ширина двери (вероятно не реализуется)*/
        public bool isOpen; //открыта ли дверь?
        protected bool isNone; //можно ли наступать на клетку? (1 для пустоты и туннелей, остальное редактируется отдельно)
        public bool isConnected;
        public int roomID;
        public int mode;
        public Exit(int x, int y)
        {
            this.x = x;
            this.y = y;
            isOpen = true;
            isNone = true;
            isConnected = false;
        }

        public int Distance(Exit exitTo)
        {
            return (int)Math.Sqrt(Math.Pow((exitTo.x - this.x),2) + Math.Pow((exitTo.y - this.y),2));
        }
    }
}
