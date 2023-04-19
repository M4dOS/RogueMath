using RogueMath;
namespace RogueMath
{
    public readonly struct Pixel
    {
        private const char PixelChar = '█';

        private const char PixelType1 = (char)CellID.Player; //Заочник
        private const char PixelType2 = (char)CellID.СoffeeLover; //Кофеман
        private const char PixelType3 = (char)CellID.Deadline; //дедлайн

        private const char PixelEnemy1 = (char)CellID.Enemy;
        private const char PixelEnemy2 = (char)CellID.Boss;

        public Pixel(int x, int y, ConsoleColor color)
        {
            X = x;
            Y = y;
            Color = color;
        }
        public int X { get; }
        public int Y { get; }
        public ConsoleColor Color { get; }

        public void Draw()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(PixelChar);
        }
        public void Clear()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(' ');
        }


        public void DrawType1_d()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(PixelType1);
        }
        public void DrawType2_d()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(PixelType2);
        }
        public void DrawType3_d()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(PixelType3);
        }


        public void DrawEnemy1_d()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(PixelEnemy1);
        }
        private const int MapWidth = 165;
        private const int MapHeight = 55;

        private const ConsoleColor BorderColor = ConsoleColor.White;
        private const ConsoleColor CharaColor = ConsoleColor.Red;


        public static void DrawBorder()
        {
            
            for (int i = 0; i < MapWidth; i++)
            {
                new Pixel(i, 0, BorderColor).Draw();
                new Pixel(i, MapHeight - 1, BorderColor).Draw();
            }
            for (int i = 0; i < MapHeight - 1; i++)
            {
                new Pixel(0, i, BorderColor).Draw();
                new Pixel(MapWidth - 1, i, BorderColor).Draw();
            }
        }
        public static void DrawType1()
        {
            for (int i = 8; i < 17; i++)
            {
                new Pixel(i, 3, CharaColor).Draw();
                new Pixel(i, 7, CharaColor).Draw();

            }
            for (int i = 3; i < 7; i++)
            {
                new Pixel(8, i, CharaColor).Draw();

                new Pixel(17 - 1, i, CharaColor).Draw();
            }
            new Pixel(12, 5, CharaColor).Draw/*Type1*/();

        }
        public static void DrawType2()
        {
            for (int i = 8; i < 17; i++)
            {
                new Pixel(i, 3, CharaColor).Draw();
                new Pixel(i, 7, CharaColor).Draw();

            }
            for (int i = 3; i < 7; i++)
            {
                new Pixel(8, i, CharaColor).Draw();

                new Pixel(17 - 1, i, CharaColor).Draw();
            }
            new Pixel(12, 5, CharaColor).Draw/*Type2*/();

        }
        public static void DrawType3()
        {
            for (int i = 8; i < 17; i++)
            {
                new Pixel(i, 3, CharaColor).Draw();
                new Pixel(i, 7, CharaColor).Draw();

            }
            for (int i = 3; i < 7; i++)
            {
                new Pixel(8, i, CharaColor).Draw();

                new Pixel(17 - 1, i, CharaColor).Draw();
            }
            new Pixel(12, 5, CharaColor).Draw/*Type3*/();

        }

        public static void Draw_1_Char()
        {
            //head
            for (int i = 28; i < 40; i++)
            {
                new Pixel(i, 13, CharaColor).Draw();

                for (int j = 34; j < 40; j++)
                {
                    new Pixel(j, 20, CharaColor).Draw();
                }
                //vertical head
                for (int j = 13; j < 19; j++)
                {
                    new Pixel(28, j, CharaColor).Draw();
                }
                for (int j = 13; j < 20; j++)
                {
                    new Pixel(39, j, CharaColor).Draw();
                }

                //eye
                new Pixel(37, 16, CharaColor).Draw();

                //headphones
                for (int j = 14; j < 17; j++)
                {
                    new Pixel(32, j, CharaColor).Draw();
                }
                for (int j = 31; j < 34; j++)
                {
                    new Pixel(j, 15, CharaColor).Draw();
                    new Pixel(j, 16, CharaColor).Draw();
                }

                //head around neck
                for (int j = 18; j < 20; j++)
                {
                    new Pixel(34, j, CharaColor).Draw();
                }
                for (int j = 29; j < 35; j++)
                {
                    new Pixel(j, 18, CharaColor).Draw();
                }

                //hair
                for (int j = 35; j < 41; j++)
                {
                    new Pixel(j, 12, CharaColor).Draw();
                }
                new Pixel(40, 13, CharaColor).Draw();
            }
            //neck
            for (int j = 19; j < 21; j++)
            {
                new Pixel(31, j, CharaColor).Draw();
            }

            //body
            for (int i = 22; i < 31; i++)
            {
                new Pixel(28, i, CharaColor).Draw();
                new Pixel(40 - 1, i, CharaColor).Draw();
            }
            for (int i = 28; i < 40; i++)
            {
                new Pixel(i, 21, CharaColor).Draw();
                new Pixel(i, 29, CharaColor).Draw();
            }

            //hand R
            for (int i = 22; i < 31; i++)
            {
                new Pixel(24, i, CharaColor).Draw();
                new Pixel(28, i, CharaColor).Draw();
            }
            for (int i = 24; i < 28; i++)
            {
                new Pixel(i, 21, CharaColor).Draw();
                new Pixel(i, 31, CharaColor).Draw();

                new Pixel(i, 24, CharaColor).Draw();
            }

            //hand L original
            for (int i = 22; i < 31; i++)
            {
                new Pixel(43, i, CharaColor).Draw();
            }
            for (int i = 40; i < 44; i++)
            {
                new Pixel(i, 21, CharaColor).Draw();
                new Pixel(i, 31, CharaColor).Draw();

                new Pixel(i, 24, CharaColor).Draw();
            }

            //leg L
            for (int i = 31; i < 40; i++)
            {
                new Pixel(28, i, CharaColor).Draw();
                new Pixel(35 - 1, i, CharaColor).Draw();
            }
            //foot L
            for (int j = 28; j < 36; j++)
            {
                new Pixel(j, 40, CharaColor).Draw();
            }

            //leg R
            for (int i = 31; i < 40; i++)
            {
                new Pixel(40 - 1, i, CharaColor).Draw();
            }
            //foot R
            for (int j = 36; j < 41; j++)
            {
                new Pixel(j, 40, CharaColor).Draw();
            }

            //phone
            for (int i = 30; i < 32; i++)
            {
                new Pixel(22, i, CharaColor).Draw();
                new Pixel(31 - 1, i, CharaColor).Draw();
            }
            for (int i = 22; i < 31; i++)
            {
                new Pixel(i, 30, CharaColor).Draw();
                new Pixel(i, 32 - 1, CharaColor).Draw();
            }
            for (int i = 25; i < 28; i++)
            {
                new Pixel(i, 30, CharaColor).Clear();
            }

        }
        public static void Draw_1_CharBite()
        {
            //head
            for (int i = 28; i < 40; i++)
            {
                new Pixel(i, 13, CharaColor).Draw();

                for (int j = 34; j < 40; j++)
                {
                    new Pixel(j, 20, CharaColor).Draw();
                }
                //vertical head
                for (int j = 13; j < 19; j++)
                {
                    new Pixel(28, j, CharaColor).Draw();
                }
                for (int j = 13; j < 20; j++)
                {
                    new Pixel(39, j, CharaColor).Draw();
                }

                //eye
                new Pixel(37, 16, CharaColor).Draw();

                //headphones
                for (int j = 14; j < 17; j++)
                {
                    new Pixel(32, j, CharaColor).Draw();
                }
                for (int j = 31; j < 34; j++)
                {
                    new Pixel(j, 15, CharaColor).Draw();
                    new Pixel(j, 16, CharaColor).Draw();
                }

                //head around neck
                for (int j = 18; j < 20; j++)
                {
                    new Pixel(34, j, CharaColor).Draw();
                }
                for (int j = 29; j < 35; j++)
                {
                    new Pixel(j, 18, CharaColor).Draw();
                }

                //hair
                for (int j = 35; j < 41; j++)
                {
                    new Pixel(j, 12, CharaColor).Draw();
                }
                new Pixel(40, 13, CharaColor).Draw();
            }
            //neck
            for (int j = 19; j < 21; j++)
            {
                new Pixel(31, j, CharaColor).Draw();
            }

            //body
            for (int i = 22; i < 31; i++)
            {
                new Pixel(28, i, CharaColor).Draw();
                new Pixel(40 - 1, i, CharaColor).Draw();
            }
            for (int i = 28; i < 40; i++)
            {
                new Pixel(i, 21, CharaColor).Draw();
                new Pixel(i, 29, CharaColor).Draw();
            }

            //hand R
            for (int i = 22; i < 31; i++)
            {
                new Pixel(24, i, CharaColor).Draw();
                new Pixel(28, i, CharaColor).Draw();
            }
            for (int i = 24; i < 28; i++)
            {
                new Pixel(i, 21, CharaColor).Draw();
                new Pixel(i, 31, CharaColor).Draw();

                new Pixel(i, 24, CharaColor).Draw();
            }

            //hand L original
            for (int i = 22; i < 31; i++)
            {
                new Pixel(43, i, CharaColor).Clear();
            }
            for (int i = 40; i < 44; i++)
            {
                new Pixel(i, 21, CharaColor).Clear();
                new Pixel(i, 31, CharaColor).Clear();

                new Pixel(i, 24, CharaColor).Clear();
            }

            //hand L
            for (int i = 40; i < 54; i++)
            {
                new Pixel(i, 21, CharaColor).Draw();
                new Pixel(i, 23, CharaColor).Draw();
            }
            for (int i = 21; i < 23 + 1; i++)
            {
                new Pixel(40, i, CharaColor).Draw();
                new Pixel(54, i, CharaColor).Draw();

                new Pixel(45, i, CharaColor).Draw();
            }

            //leg L
            for (int i = 31; i < 40; i++)
            {
                new Pixel(28, i, CharaColor).Draw();
                new Pixel(35 - 1, i, CharaColor).Draw();
            }
            //foot L
            for (int j = 28; j < 36; j++)
            {
                new Pixel(j, 40, CharaColor).Draw();
            }

            //leg R
            for (int i = 31; i < 40; i++)
            {
                new Pixel(40 - 1, i, CharaColor).Draw();
            }
            //foot R
            for (int j = 36; j < 41; j++)
            {
                new Pixel(j, 40, CharaColor).Draw();
            }

            //phone
            for (int i = 30; i < 32; i++)
            {
                new Pixel(22, i, CharaColor).Draw();
                new Pixel(31 - 1, i, CharaColor).Draw();
            }
            for (int i = 22; i < 31; i++)
            {
                new Pixel(i, 30, CharaColor).Draw();
                new Pixel(i, 32 - 1, CharaColor).Draw();
            }
            for (int i = 25; i < 28; i++)
            {
                new Pixel(i, 30, CharaColor).Clear();
            }
            Thread.Sleep(500);
        }
        public static void Draw_1_CharUltraBite()
        {
            //head
            for (int i = 28; i < 40; i++)
            {
                new Pixel(i, 13, CharaColor).Draw();

                for (int j = 34; j < 40; j++)
                {
                    new Pixel(j, 20, CharaColor).Draw();
                }
                //vertical head
                for (int j = 13; j < 19; j++)
                {
                    new Pixel(28, j, CharaColor).Draw();
                }
                for (int j = 13; j < 20; j++)
                {
                    new Pixel(39, j, CharaColor).Draw();
                }

                //eye
                new Pixel(37, 16, CharaColor).Draw();

                //headphones
                for (int j = 14; j < 17; j++)
                {
                    new Pixel(32, j, CharaColor).Draw();
                }
                for (int j = 31; j < 34; j++)
                {
                    new Pixel(j, 15, CharaColor).Draw();
                    new Pixel(j, 16, CharaColor).Draw();
                }

                //head around neck
                for (int j = 18; j < 20; j++)
                {
                    new Pixel(34, j, CharaColor).Draw();
                }
                for (int j = 29; j < 35; j++)
                {
                    new Pixel(j, 18, CharaColor).Draw();
                }

                //hair
                for (int j = 35; j < 41; j++)
                {
                    new Pixel(j, 12, CharaColor).Draw();
                }
                new Pixel(40, 13, CharaColor).Draw();
            }
            //neck
            for (int j = 19; j < 21; j++)
            {
                new Pixel(31, j, CharaColor).Draw();
            }

            //body
            for (int i = 22; i < 31; i++)
            {
                new Pixel(28, i, CharaColor).Draw();
                new Pixel(40 - 1, i, CharaColor).Draw();
            }
            for (int i = 28; i < 40; i++)
            {
                new Pixel(i, 21, CharaColor).Draw();
                new Pixel(i, 29, CharaColor).Draw();
            }

            //hand R его правая
            for (int i = 22; i < 31; i++)
            {
                new Pixel(24, i, CharaColor).Draw();
                new Pixel(28, i, CharaColor).Draw();
            }
            for (int i = 24; i < 28; i++)
            {
                new Pixel(i, 21, CharaColor).Draw();
                new Pixel(i, 31, CharaColor).Draw();

                new Pixel(i, 24, CharaColor).Draw();
            }

            //hand L original
            for (int i = 22; i < 31; i++)
            {
                new Pixel(43, i, CharaColor).Clear();
            }
            for (int i = 40; i < 44; i++)
            {
                new Pixel(i, 21, CharaColor).Clear();
                new Pixel(i, 31, CharaColor).Clear();

                new Pixel(i, 24, CharaColor).Clear();
            }

            //hand L его левая
            for (int i = 40; i < 54; i++)
            {
                new Pixel(i, 21, CharaColor).Draw();
                new Pixel(i, 23, CharaColor).Draw();
            }
            for (int i = 21; i < 23 + 1; i++)
            {
                new Pixel(40, i, CharaColor).Draw();
                new Pixel(54, i, CharaColor).Draw();

                new Pixel(45, i, CharaColor).Draw();
            }
            //sparkle
            for (int i = 57; i < 66; i++)
            {
                new Pixel(i, 22, CharaColor).Draw();
            }
            for (int i = 20; i < 25; i++)
            {
                new Pixel(61, i, CharaColor).Draw();
            }

            //leg L
            for (int i = 31; i < 40; i++)
            {
                new Pixel(28, i, CharaColor).Draw();
                new Pixel(35 - 1, i, CharaColor).Draw();
            }
            //foot L
            for (int j = 28; j < 36; j++)
            {
                new Pixel(j, 40, CharaColor).Draw();
            }

            //leg R
            for (int i = 31; i < 40; i++)
            {
                new Pixel(40 - 1, i, CharaColor).Draw();
            }
            //foot R
            for (int j = 36; j < 41; j++)
            {
                new Pixel(j, 40, CharaColor).Draw();
            }

            //phone
            for (int i = 30; i < 32; i++)
            {
                new Pixel(22, i, CharaColor).Draw();
                new Pixel(31 - 1, i, CharaColor).Draw();
            }
            for (int i = 22; i < 31; i++)
            {
                new Pixel(i, 30, CharaColor).Draw();
                new Pixel(i, 32 - 1, CharaColor).Draw();
            }
            for (int i = 25; i < 28; i++)
            {
                new Pixel(i, 30, CharaColor).Clear();
            }
            Thread.Sleep(500);
        }


        public static void Draw_2_Char()
        {
            //head
            for (int i = 28; i < 40; i++)
            {
                new Pixel(i, 13, CharaColor).Draw();

                for (int j = 34; j < 40; j++)
                {
                    new Pixel(j, 20, CharaColor).Draw();
                }
                //vertical head
                for (int j = 13; j < 19; j++)
                {
                    new Pixel(28, j, CharaColor).Draw();
                }
                for (int j = 13; j < 20; j++)
                {
                    new Pixel(39, j, CharaColor).Draw();
                }

                //eye
                new Pixel(37, 16, CharaColor).Draw();
                new Pixel(36, 16, CharaColor).Draw();
                new Pixel(35, 16, CharaColor).Draw();

                new Pixel(40, 16, CharaColor).Draw();
                new Pixel(41, 16, CharaColor).Draw();

                //hair
                for (int j = 30; j < 40; j++)
                {
                    new Pixel(j, 12, CharaColor).Draw();
                }

                new Pixel(40, 13, CharaColor).Draw();

                for (int j = 28; j < 35; j++)
                {
                    new Pixel(j, 14, CharaColor).Draw();
                }
                for (int j = 28; j < 34; j++)
                {
                    new Pixel(j, 15, CharaColor).Draw();
                }
                for (int j = 28; j < 32; j++)
                {
                    new Pixel(j, 16, CharaColor).Draw();
                }
                for (int j = 28; j < 32; j++)
                {
                    new Pixel(j, 17, CharaColor).Draw();
                }

                //head around neck
                for (int j = 18; j < 20; j++)
                {
                    new Pixel(34, j, CharaColor).Draw();
                }
                for (int j = 29; j < 35; j++)
                {
                    new Pixel(j, 18, CharaColor).Draw();
                }

            }
            //neck
            for (int j = 19; j < 21; j++)
            {
                new Pixel(31, j, CharaColor).Draw();
            }

            //body
            for (int i = 22; i < 31; i++)
            {
                new Pixel(28, i, CharaColor).Draw();
                new Pixel(40 - 1, i, CharaColor).Draw();
            }
            for (int i = 28; i < 40; i++)
            {
                new Pixel(i, 21, CharaColor).Draw();
                new Pixel(i, 29, CharaColor).Draw();
            }

            //hand R
            for (int i = 22; i < 31; i++)
            {
                new Pixel(24, i, CharaColor).Draw();
                new Pixel(28, i, CharaColor).Draw();
            }
            for (int i = 24; i < 28; i++)
            {
                new Pixel(i, 21, CharaColor).Draw();
                new Pixel(i, 31, CharaColor).Draw();

                new Pixel(i, 28, CharaColor).Draw();
            }

            //hand L original
            for (int i = 22; i < 26; i++)
            {
                new Pixel(43, i, CharaColor).Draw();
            }
            for (int i = 40; i < 44; i++)
            {
                new Pixel(i, 21, CharaColor).Draw();
                new Pixel(i, 28, CharaColor).Draw();
            }
            for (int i = 43; i < 52; i++)
            {
                new Pixel(i, 26, CharaColor).Draw();
                new Pixel(i, 28, CharaColor).Draw();
            }
            new Pixel(51, 27, CharaColor).Draw();
            new Pixel(44, 27, CharaColor).Draw();

            //coffee
            for (int i = 45; i < 52; i++)
            {
                new Pixel(i, 24, CharaColor).Draw();

            }
            new Pixel(46, 25, CharaColor).Draw();
            new Pixel(50, 25, CharaColor).Draw();


            //leg L
            for (int i = 31; i < 40; i++)
            {
                new Pixel(28, i, CharaColor).Draw();
                new Pixel(35 - 1, i, CharaColor).Draw();
            }
            //foot L
            for (int j = 28; j < 36; j++)
            {
                new Pixel(j, 40, CharaColor).Draw();
                new Pixel(j, 39, CharaColor).Draw();
            }

            //leg R
            for (int i = 31; i < 40; i++)
            {
                new Pixel(40 - 1, i, CharaColor).Draw();
            }
            //foot R
            for (int j = 36; j < 41; j++)
            {
                new Pixel(j, 40, CharaColor).Draw();
                new Pixel(j, 39, CharaColor).Draw();
            }


        }
        public static void Draw_2_CharBite()
        {
            //head
            for (int i = 28; i < 40; i++)
            {
                new Pixel(i, 13, CharaColor).Draw();

                for (int j = 34; j < 40; j++)
                {
                    new Pixel(j, 20, CharaColor).Draw();
                }
                //vertical head
                for (int j = 13; j < 19; j++)
                {
                    new Pixel(28, j, CharaColor).Draw();
                }
                for (int j = 13; j < 20; j++)
                {
                    new Pixel(39, j, CharaColor).Draw();
                }

                //eye
                new Pixel(37, 16, CharaColor).Draw();
                new Pixel(36, 16, CharaColor).Draw();
                new Pixel(35, 16, CharaColor).Draw();

                new Pixel(40, 16, CharaColor).Draw();
                new Pixel(41, 16, CharaColor).Draw();

                //hair
                for (int j = 30; j < 40; j++)
                {
                    new Pixel(j, 12, CharaColor).Draw();
                }

                new Pixel(40, 13, CharaColor).Draw();

                for (int j = 28; j < 35; j++)
                {
                    new Pixel(j, 14, CharaColor).Draw();
                }
                for (int j = 28; j < 34; j++)
                {
                    new Pixel(j, 15, CharaColor).Draw();
                }
                for (int j = 28; j < 32; j++)
                {
                    new Pixel(j, 16, CharaColor).Draw();
                }
                for (int j = 28; j < 32; j++)
                {
                    new Pixel(j, 17, CharaColor).Draw();
                }

                //head around neck
                for (int j = 18; j < 20; j++)
                {
                    new Pixel(34, j, CharaColor).Draw();
                }
                for (int j = 29; j < 35; j++)
                {
                    new Pixel(j, 18, CharaColor).Draw();
                }

            }
            //neck
            for (int j = 19; j < 21; j++)
            {
                new Pixel(31, j, CharaColor).Draw();
            }

            //body
            for (int i = 22; i < 31; i++)
            {
                new Pixel(28, i, CharaColor).Draw();
                new Pixel(40 - 1, i, CharaColor).Draw();
            }
            for (int i = 28; i < 40; i++)
            {
                new Pixel(i, 21, CharaColor).Draw();
                new Pixel(i, 29, CharaColor).Draw();
            }

            //hand R
            for (int i = 22; i < 31; i++)
            {
                new Pixel(24, i, CharaColor).Draw();
                new Pixel(28, i, CharaColor).Draw();
            }
            for (int i = 24; i < 28; i++)
            {
                new Pixel(i, 21, CharaColor).Draw();
                new Pixel(i, 31, CharaColor).Draw();

                new Pixel(i, 28, CharaColor).Draw();
            }

            //hand L original
            for (int i = 22; i < 26; i++)
            {
                new Pixel(43, i, CharaColor).Clear();
            }
            for (int i = 40; i < 44; i++)
            {
                new Pixel(i, 21, CharaColor).Clear();
                new Pixel(i, 28, CharaColor).Clear();
            }
            for (int i = 43; i < 52; i++)
            {
                new Pixel(i, 26, CharaColor).Clear();
                new Pixel(i, 28, CharaColor).Clear();
            }
            new Pixel(51, 27, CharaColor).Clear();
            new Pixel(44, 27, CharaColor).Clear();

            //hand L
            for (int i = 40; i < 54; i++)
            {
                new Pixel(i, 21, CharaColor).Draw();
                new Pixel(i, 23, CharaColor).Draw();
            }
            for (int i = 21; i < 23 + 1; i++)
            {
                new Pixel(40, i, CharaColor).Draw();
                new Pixel(54, i, CharaColor).Draw();

                new Pixel(49, i, CharaColor).Draw();

            }
            //coffee
            for (int i = 48; i < 55; i++)
            {
                new Pixel(i, 19, CharaColor).Draw();

            }
            new Pixel(49, 20, CharaColor).Draw();
            new Pixel(53, 20, CharaColor).Draw();


            //leg L
            for (int i = 31; i < 40; i++)
            {
                new Pixel(28, i, CharaColor).Draw();
                new Pixel(35 - 1, i, CharaColor).Draw();
            }
            //foot L
            for (int j = 28; j < 36; j++)
            {
                new Pixel(j, 40, CharaColor).Draw();
                new Pixel(j, 39, CharaColor).Draw();
            }

            //leg R
            for (int i = 31; i < 40; i++)
            {
                new Pixel(40 - 1, i, CharaColor).Draw();
            }
            //foot R
            for (int j = 36; j < 41; j++)
            {
                new Pixel(j, 40, CharaColor).Draw();
                new Pixel(j, 39, CharaColor).Draw();
            }
            Thread.Sleep(500);

        }
        public static void Draw_2_CharUltraBite()
        {
            //head
            for (int i = 28; i < 40; i++)
            {
                new Pixel(i, 13, CharaColor).Draw();

                for (int j = 34; j < 40; j++)
                {
                    new Pixel(j, 20, CharaColor).Draw();
                }
                //vertical head
                for (int j = 13; j < 19; j++)
                {
                    new Pixel(28, j, CharaColor).Draw();
                }
                for (int j = 13; j < 20; j++)
                {
                    new Pixel(39, j, CharaColor).Draw();
                }

                //eye
                new Pixel(37, 16, CharaColor).Draw();
                new Pixel(36, 16, CharaColor).Draw();
                new Pixel(35, 16, CharaColor).Draw();

                new Pixel(40, 16, CharaColor).Draw();
                new Pixel(41, 16, CharaColor).Draw();

                //hair
                for (int j = 30; j < 40; j++)
                {
                    new Pixel(j, 12, CharaColor).Draw();
                }

                new Pixel(40, 13, CharaColor).Draw();

                for (int j = 28; j < 35; j++)
                {
                    new Pixel(j, 14, CharaColor).Draw();
                }
                for (int j = 28; j < 34; j++)
                {
                    new Pixel(j, 15, CharaColor).Draw();
                }
                for (int j = 28; j < 32; j++)
                {
                    new Pixel(j, 16, CharaColor).Draw();
                }
                for (int j = 28; j < 32; j++)
                {
                    new Pixel(j, 17, CharaColor).Draw();
                }

                //head around neck
                for (int j = 18; j < 20; j++)
                {
                    new Pixel(34, j, CharaColor).Draw();
                }
                for (int j = 29; j < 35; j++)
                {
                    new Pixel(j, 18, CharaColor).Draw();
                }

            }
            //neck
            for (int j = 19; j < 21; j++)
            {
                new Pixel(31, j, CharaColor).Draw();
            }

            //body
            for (int i = 22; i < 31; i++)
            {
                new Pixel(28, i, CharaColor).Draw();
                new Pixel(40 - 1, i, CharaColor).Draw();
            }
            for (int i = 28; i < 40; i++)
            {
                new Pixel(i, 21, CharaColor).Draw();
                new Pixel(i, 29, CharaColor).Draw();
            }

            //hand R
            for (int i = 22; i < 31; i++)
            {
                new Pixel(24, i, CharaColor).Draw();
                new Pixel(28, i, CharaColor).Draw();
            }
            for (int i = 24; i < 28; i++)
            {
                new Pixel(i, 21, CharaColor).Draw();
                new Pixel(i, 31, CharaColor).Draw();

                new Pixel(i, 28, CharaColor).Draw();
            }

            //hand L original
            for (int i = 22; i < 26; i++)
            {
                new Pixel(43, i, CharaColor).Clear();
            }
            for (int i = 40; i < 44; i++)
            {
                new Pixel(i, 21, CharaColor).Clear();
                new Pixel(i, 28, CharaColor).Clear();
            }
            for (int i = 43; i < 52; i++)
            {
                new Pixel(i, 26, CharaColor).Clear();
                new Pixel(i, 28, CharaColor).Clear();
            }
            new Pixel(51, 27, CharaColor).Clear();
            new Pixel(44, 27, CharaColor).Clear();

            //hand L
            for (int i = 40; i < 54; i++)
            {
                new Pixel(i, 21, CharaColor).Draw();
                new Pixel(i, 23, CharaColor).Draw();
            }
            for (int i = 21; i < 23 + 1; i++)
            {
                new Pixel(40, i, CharaColor).Draw();
                new Pixel(54, i, CharaColor).Draw();

                new Pixel(49, i, CharaColor).Draw();

            }
            //sparkle
            for (int i = 57; i < 66; i++)
            {
                new Pixel(i, 22, CharaColor).Draw();
            }
            for (int i = 20; i < 25; i++)
            {
                new Pixel(61, i, CharaColor).Draw();
            }

            //coffee
            for (int i = 48; i < 55; i++)
            {
                new Pixel(i, 19, CharaColor).Draw();

            }
            new Pixel(49, 20, CharaColor).Draw();
            new Pixel(53, 20, CharaColor).Draw();


            //leg L
            for (int i = 31; i < 40; i++)
            {
                new Pixel(28, i, CharaColor).Draw();
                new Pixel(35 - 1, i, CharaColor).Draw();
            }
            //foot L
            for (int j = 28; j < 36; j++)
            {
                new Pixel(j, 40, CharaColor).Draw();
                new Pixel(j, 39, CharaColor).Draw();
            }

            //leg R
            for (int i = 31; i < 40; i++)
            {
                new Pixel(40 - 1, i, CharaColor).Draw();
            }
            //foot R
            for (int j = 36; j < 41; j++)
            {
                new Pixel(j, 40, CharaColor).Draw();
                new Pixel(j, 39, CharaColor).Draw();
            }
            Thread.Sleep(500);

        }

        public static void Draw_3_Char()
        {
            //head
            for (int i = 28; i < 40; i++)
            {
                new Pixel(i, 13, CharaColor).Draw();

                for (int j = 34; j < 40; j++)
                {
                    new Pixel(j, 20, CharaColor).Draw();
                }
                //vertical head
                for (int j = 13; j < 19; j++)
                {
                    new Pixel(28, j, CharaColor).Draw();
                }
                for (int j = 13; j < 20; j++)
                {
                    new Pixel(39, j, CharaColor).Draw();
                }

                //eye

                new Pixel(36, 16, CharaColor).Draw();

                //head around neck
                for (int j = 18; j < 20; j++)
                {
                    new Pixel(34, j, CharaColor).Draw();
                }
                for (int j = 29; j < 35; j++)
                {
                    new Pixel(j, 18, CharaColor).Draw();
                }

            }
            //hair

            for (int i = 12; i < 17; i++)
            {
                new Pixel(26, i, CharaColor).Draw();
                new Pixel(34, i, CharaColor).Draw();
                new Pixel(41, i, CharaColor).Draw();
            }
            for (int i = 26; i < 34; i++)
            {
                new Pixel(i, 16, CharaColor).Draw();
            }
            for (int i = 26; i < 41; i++)
            {
                new Pixel(i, 12, CharaColor).Draw();
            }
            //delete head
            for (int j = 13; j < 16; j++)
            {
                new Pixel(28, j, CharaColor).Clear();
            }
            for (int j = 29; j < 34; j++)
            {
                new Pixel(j, 13, CharaColor).Clear();
            }


            //neck
            for (int j = 19; j < 21; j++)
            {
                new Pixel(31, j, CharaColor).Draw();
            }

            //body
            for (int i = 22; i < 31; i++)
            {
                new Pixel(28, i, CharaColor).Draw();
                new Pixel(40 - 1, i, CharaColor).Draw();
            }
            for (int i = 28; i < 40; i++)
            {
                new Pixel(i, 21, CharaColor).Draw();
                new Pixel(i, 30, CharaColor).Draw();
            }

            //hand R
            for (int i = 22; i < 31; i++)
            {
                new Pixel(24, i, CharaColor).Draw();
                new Pixel(28, i, CharaColor).Draw();
            }
            for (int i = 24; i < 28; i++)
            {
                new Pixel(i, 21, CharaColor).Draw();
                new Pixel(i, 31, CharaColor).Draw();

                new Pixel(i, 29, CharaColor).Draw();
            }

            //pillow
            for (int i = 24; i < 29; i++)
            {
                new Pixel(19, i, CharaColor).Draw();
                new Pixel(32, i, CharaColor).Draw();
            }
            for (int i = 19; i < 25; i++)
            {
                new Pixel(i, 24, CharaColor).Draw();
                new Pixel(i, 28, CharaColor).Draw();
            }

            //blanket
            for (int i = 29; i < 40; i++)
            {
                new Pixel(22, i, CharaColor).Draw();
            }
            for (int i = 20; i < 34; i++)
            {
                new Pixel(i, 39, CharaColor).Draw();
            }

            for (int i = 28; i < 32; i++)
            {
                new Pixel(i, 24, CharaColor).Draw();
                new Pixel(i, 28, CharaColor).Draw();
            }

            //hand L original
            for (int i = 22; i < 31; i++)
            {
                new Pixel(43, i, CharaColor).Draw();
            }
            for (int i = 40; i < 44; i++)
            {
                new Pixel(i, 21, CharaColor).Draw();
                new Pixel(i, 31, CharaColor).Draw();

                new Pixel(i, 29, CharaColor).Draw();
            }


            //leg L
            for (int i = 31; i < 40; i++)
            {
                new Pixel(28, i, CharaColor).Draw();
                new Pixel(35 - 1, i, CharaColor).Draw();
            }
            //foot L
            for (int j = 28; j < 36; j++)
            {
                new Pixel(j, 40, CharaColor).Draw();
                new Pixel(j, 39, CharaColor).Draw();
            }

            //leg R
            for (int i = 31; i < 40; i++)
            {
                new Pixel(40 - 1, i, CharaColor).Draw();
            }
            //foot R
            for (int j = 36; j < 41; j++)
            {
                new Pixel(j, 40, CharaColor).Draw();
                new Pixel(j, 39, CharaColor).Draw();
            }


        }
        public static void Draw_3_CharBite()
        {
            //head
            for (int i = 28; i < 40; i++)
            {
                new Pixel(i, 13, CharaColor).Draw();

                for (int j = 34; j < 40; j++)
                {
                    new Pixel(j, 20, CharaColor).Draw();
                }
                //vertical head
                for (int j = 13; j < 19; j++)
                {
                    new Pixel(28, j, CharaColor).Draw();
                }
                for (int j = 13; j < 20; j++)
                {
                    new Pixel(39, j, CharaColor).Draw();
                }

                //eye

                new Pixel(36, 16, CharaColor).Draw();

                //head around neck
                for (int j = 18; j < 20; j++)
                {
                    new Pixel(34, j, CharaColor).Draw();
                }
                for (int j = 29; j < 35; j++)
                {
                    new Pixel(j, 18, CharaColor).Draw();
                }

            }
            //hair

            for (int i = 12; i < 17; i++)
            {
                new Pixel(26, i, CharaColor).Draw();
                new Pixel(34, i, CharaColor).Draw();
                new Pixel(41, i, CharaColor).Draw();
            }
            for (int i = 26; i < 34; i++)
            {
                new Pixel(i, 16, CharaColor).Draw();
            }
            for (int i = 26; i < 41; i++)
            {
                new Pixel(i, 12, CharaColor).Draw();
            }
            //delete head
            for (int j = 13; j < 16; j++)
            {
                new Pixel(28, j, CharaColor).Clear();
            }
            for (int j = 29; j < 34; j++)
            {
                new Pixel(j, 13, CharaColor).Clear();
            }


            //neck
            for (int j = 19; j < 21; j++)
            {
                new Pixel(31, j, CharaColor).Draw();
            }

            //body
            for (int i = 22; i < 31; i++)
            {
                new Pixel(28, i, CharaColor).Draw();
                new Pixel(40 - 1, i, CharaColor).Draw();
            }
            for (int i = 28; i < 40; i++)
            {
                new Pixel(i, 21, CharaColor).Draw();
                new Pixel(i, 30, CharaColor).Draw();
            }

            //hand R
            for (int i = 22; i < 31; i++)
            {
                new Pixel(24, i, CharaColor).Draw();
                new Pixel(28, i, CharaColor).Draw();
            }
            for (int i = 24; i < 28; i++)
            {
                new Pixel(i, 21, CharaColor).Draw();
                new Pixel(i, 31, CharaColor).Draw();

                new Pixel(i, 29, CharaColor).Draw();
            }

            //pillow
            for (int i = 24; i < 29; i++)
            {
                new Pixel(19, i, CharaColor).Draw();
                new Pixel(32, i, CharaColor).Draw();
            }
            for (int i = 19; i < 25; i++)
            {
                new Pixel(i, 24, CharaColor).Draw();
                new Pixel(i, 28, CharaColor).Draw();
            }

            //blanket
            for (int i = 29; i < 40; i++)
            {
                new Pixel(22, i, CharaColor).Draw();
            }
            for (int i = 20; i < 34; i++)
            {
                new Pixel(i, 39, CharaColor).Draw();
            }

            for (int i = 28; i < 32; i++)
            {
                new Pixel(i, 24, CharaColor).Draw();
                new Pixel(i, 28, CharaColor).Draw();
            }

            //hand L original
            for (int i = 22; i < 31; i++)
            {
                new Pixel(43, i, CharaColor).Clear();
            }
            for (int i = 40; i < 44; i++)
            {
                new Pixel(i, 21, CharaColor).Clear();
                new Pixel(i, 31, CharaColor).Clear();

                new Pixel(i, 29, CharaColor).Clear();
            }
            //hand L
            for (int i = 40; i < 54; i++)
            {
                new Pixel(i, 21, CharaColor).Draw();
                new Pixel(i, 23, CharaColor).Draw();
            }
            for (int i = 21; i < 23 + 1; i++)
            {
                new Pixel(40, i, CharaColor).Draw();
                new Pixel(54, i, CharaColor).Draw();

                new Pixel(50, i, CharaColor).Draw();

            }

            //leg L
            for (int i = 31; i < 40; i++)
            {
                new Pixel(28, i, CharaColor).Draw();
                new Pixel(35 - 1, i, CharaColor).Draw();
            }
            //foot L
            for (int j = 28; j < 36; j++)
            {
                new Pixel(j, 40, CharaColor).Draw();
                new Pixel(j, 39, CharaColor).Draw();
            }

            //leg R
            for (int i = 31; i < 40; i++)
            {
                new Pixel(40 - 1, i, CharaColor).Draw();
            }
            //foot R
            for (int j = 36; j < 41; j++)
            {
                new Pixel(j, 40, CharaColor).Draw();
                new Pixel(j, 39, CharaColor).Draw();
            }

            Thread.Sleep(500);
        }
        public static void Draw_3_CharUltraBite()
        {
            //head
            for (int i = 28; i < 40; i++)
            {
                new Pixel(i, 13, CharaColor).Draw();

                for (int j = 34; j < 40; j++)
                {
                    new Pixel(j, 20, CharaColor).Draw();
                }
                //vertical head
                for (int j = 13; j < 19; j++)
                {
                    new Pixel(28, j, CharaColor).Draw();
                }
                for (int j = 13; j < 20; j++)
                {
                    new Pixel(39, j, CharaColor).Draw();
                }

                //eye

                new Pixel(36, 16, CharaColor).Draw();

                //head around neck
                for (int j = 18; j < 20; j++)
                {
                    new Pixel(34, j, CharaColor).Draw();
                }
                for (int j = 29; j < 35; j++)
                {
                    new Pixel(j, 18, CharaColor).Draw();
                }

            }
            //hair

            for (int i = 12; i < 17; i++)
            {
                new Pixel(26, i, CharaColor).Draw();
                new Pixel(34, i, CharaColor).Draw();
                new Pixel(41, i, CharaColor).Draw();
            }
            for (int i = 26; i < 34; i++)
            {
                new Pixel(i, 16, CharaColor).Draw();
            }
            for (int i = 26; i < 41; i++)
            {
                new Pixel(i, 12, CharaColor).Draw();
            }
            //delete head
            for (int j = 13; j < 16; j++)
            {
                new Pixel(28, j, CharaColor).Clear();
            }
            for (int j = 29; j < 34; j++)
            {
                new Pixel(j, 13, CharaColor).Clear();
            }


            //neck
            for (int j = 19; j < 21; j++)
            {
                new Pixel(31, j, CharaColor).Draw();
            }

            //body
            for (int i = 22; i < 31; i++)
            {
                new Pixel(28, i, CharaColor).Draw();
                new Pixel(40 - 1, i, CharaColor).Draw();
            }
            for (int i = 28; i < 40; i++)
            {
                new Pixel(i, 21, CharaColor).Draw();
                new Pixel(i, 30, CharaColor).Draw();
            }

            //hand R
            for (int i = 22; i < 31; i++)
            {
                new Pixel(24, i, CharaColor).Draw();
                new Pixel(28, i, CharaColor).Draw();
            }
            for (int i = 24; i < 28; i++)
            {
                new Pixel(i, 21, CharaColor).Draw();
                new Pixel(i, 31, CharaColor).Draw();

                new Pixel(i, 29, CharaColor).Draw();
            }

            //pillow
            for (int i = 24; i < 29; i++)
            {
                new Pixel(19, i, CharaColor).Draw();
                new Pixel(32, i, CharaColor).Draw();
            }
            for (int i = 19; i < 25; i++)
            {
                new Pixel(i, 24, CharaColor).Draw();
                new Pixel(i, 28, CharaColor).Draw();
            }

            //blanket
            for (int i = 29; i < 40; i++)
            {
                new Pixel(22, i, CharaColor).Draw();
            }
            for (int i = 20; i < 34; i++)
            {
                new Pixel(i, 39, CharaColor).Draw();
            }

            for (int i = 28; i < 32; i++)
            {
                new Pixel(i, 24, CharaColor).Draw();
                new Pixel(i, 28, CharaColor).Draw();
            }

            //hand L original
            for (int i = 22; i < 31; i++)
            {
                new Pixel(43, i, CharaColor).Clear();
            }
            for (int i = 40; i < 44; i++)
            {
                new Pixel(i, 21, CharaColor).Clear();
                new Pixel(i, 31, CharaColor).Clear();

                new Pixel(i, 29, CharaColor).Clear();
            }
            //hand L
            for (int i = 40; i < 54; i++)
            {
                new Pixel(i, 21, CharaColor).Draw();
                new Pixel(i, 23, CharaColor).Draw();
            }
            for (int i = 21; i < 23 + 1; i++)
            {
                new Pixel(40, i, CharaColor).Draw();
                new Pixel(54, i, CharaColor).Draw();

                new Pixel(50, i, CharaColor).Draw();
            }
            //sparkle
            for (int i = 57; i < 66; i++)
            {
                new Pixel(i, 22, CharaColor).Draw();
            }
            for (int i = 20; i < 25; i++)
            {
                new Pixel(61, i, CharaColor).Draw();
            }

            //leg L
            for (int i = 31; i < 40; i++)
            {
                new Pixel(28, i, CharaColor).Draw();
                new Pixel(35 - 1, i, CharaColor).Draw();
            }
            //foot L
            for (int j = 28; j < 36; j++)
            {
                new Pixel(j, 40, CharaColor).Draw();
                new Pixel(j, 39, CharaColor).Draw();
            }

            //leg R
            for (int i = 31; i < 40; i++)
            {
                new Pixel(40 - 1, i, CharaColor).Draw();
            }
            //foot R
            for (int j = 36; j < 41; j++)
            {
                new Pixel(j, 40, CharaColor).Draw();
                new Pixel(j, 39, CharaColor).Draw();
            }
            Thread.Sleep(500);

        }

        public static void DrawAtack()
        {
            for (int i = 10; i < 20; i++)
            {
                new Pixel(i, 43, CharaColor).Draw();
                new Pixel(i, 48, CharaColor).Draw();
            }
            for (int i = 43; i < 48; i++)
            {
                new Pixel(10, i, CharaColor).Draw();
                new Pixel(13, i, CharaColor).Draw();
                new Pixel(16, i, CharaColor).Draw();
                new Pixel(19, i, CharaColor).Draw();
            }
            for (int i = 8; i < 10; i++)
            {
                new Pixel(i, 45, CharaColor).Draw();
                new Pixel(i, 47, CharaColor).Draw();
            }
            new Pixel(8, 46, CharaColor).Draw();


        }
        public static void DrawUltraAtack()
        {
            for (int i = 25; i < 35; i++)
            {
                new Pixel(i, 43, CharaColor).Draw();
                new Pixel(i, 48, CharaColor).Draw();

                new Pixel(i, 45, CharaColor).Draw();
            }
            for (int i = 43; i < 48; i++)
            {
                new Pixel(25, i, CharaColor).Draw();
                new Pixel(29, i, CharaColor).Draw();
                new Pixel(30, i, CharaColor).Draw();
                new Pixel(34, i, CharaColor).Draw();
            }

        }
        public static void DrawInvent()
        {
            for (int i = 140; i < 152; i++)
            {
                new Pixel(i, 43, CharaColor).Draw();
                new Pixel(i, 49, CharaColor).Draw();
            }
            for (int i = 43; i < 49; i++)
            {
                new Pixel(140, i, CharaColor).Draw();
                new Pixel(152 - 1, i + 1, CharaColor).Draw();
            }
            for (int i = 144; i < 148; i++)
            {
                new Pixel(i, 46, CharaColor).Draw();
            }
            new Pixel(143, 45, CharaColor).Draw();
            new Pixel(142, 44, CharaColor).Draw();

            new Pixel(148, 45, CharaColor).Draw();
            new Pixel(149, 44, CharaColor).Draw();
        }
        public static void DrawEnemy1()
        {
            for (int i = 150; i < 159; i++)
            {
                new Pixel(i, 3, CharaColor).Draw();
                new Pixel(i, 7, CharaColor).Draw();

            }
            for (int i = 3; i < 7; i++)
            {
                new Pixel(150, i, CharaColor).Draw();

                new Pixel(159 - 1, i, CharaColor).Draw();
            }
            new Pixel(154, 5, CharaColor).Draw/*Enemy1*/();

        }
        public static void DrawEnemyX()
        {
            for (int i = 110; i < 132; i++)
            {
                new Pixel(i, 20, CharaColor).Draw();
            }
            for (int i = 110; i < 112; i++)
            {
                new Pixel(i, 40, CharaColor).Draw();
            }
            for (int i = 110; i < 128; i++)
            {
                new Pixel(i, 27, CharaColor).Draw();
            }

            //horizont
            for (int i = 20; i < 40; i++)
            {
                new Pixel(110, i, CharaColor).Draw();
            }
            for (int i = 20; i < 40; i++)
            {
                new Pixel(111, i, CharaColor).Draw();
            }
        }


    }
}
