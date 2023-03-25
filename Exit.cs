using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueMath
{
    internal class Exit
    {
        int x, y; //координаты
        int width; //ширина двери (вероятно не реализуется)
        bool isOpen; //открыта ли дверь?
        bool isNone; //можно ли наступать на клетку? (1 для пустоты и туннелей, остальное редактируется отдельно)
        public Exit() 
        {
            
        }
    }
}
