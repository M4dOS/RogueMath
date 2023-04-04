namespace RogueMath
{
    internal class Exit //выход 
    {
        public int x, y; //координаты
        public bool isOpen; //открыта ли дверь?

        public bool isConnected; //соединён ли с другим выходом?
        public int roomID; //айди комнаты, которой он принадлежит
        public int mode; //направление (0 -верхняя стена, 1 - нижняя стена, 2 - левая стена, 3 - правая стена)
        public Exit(int x, int y) //создание 
        {
            this.x = x;
            this.y = y;
            isOpen = true;
            isConnected = false;
        }

        public Exit() { } //создание пустышки 

        public int Distance(Exit exitTo) //вычисление расстояния до определённого выхода 
        {
            return (int)Math.Sqrt(Math.Pow(exitTo.x - x, 2) + Math.Pow(exitTo.y - y, 2));
        }
    }
}
