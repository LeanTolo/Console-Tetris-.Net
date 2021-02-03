using System;

namespace Console_Tetris_.Net
{
    class Program
    {
        public static string blockArea = "■";
        public static int rows = 0, score = 0, level = 1;
        private static TetrisFigure tFig;
        private static TetrisFigure nextTFig;
        static void Main()
        {
            //Console.OutputEncoding = System.Text.Encoding.UTF8;
            drawBorder();
            getMenu();
            
            tFig = new TetrisFigure();
            Console.ReadLine();

        }

        //Drawing Borders
        public static void drawBorder()
        {
            for (int lengthCount = 0; lengthCount <= 22; ++lengthCount)
            {
                Console.SetCursorPosition(0, lengthCount);
                Console.Write("|");
                Console.SetCursorPosition(21, lengthCount);
                Console.Write("|");
            }
            Console.SetCursorPosition(0, 23);
            for (int widthCount = 0; widthCount <= 10; widthCount++)
            {
                Console.Write("--");
            }

        }

        

        //We create the menu, if you press ESC key, you exit the program
        //else, the game runs
        public static void getMenu()
        {

            Console.SetCursorPosition(4, 5);
            Console.WriteLine("Press Any Key");
            Console.SetCursorPosition(5, 6);
            Console.WriteLine("To Start The");
            Console.SetCursorPosition(9, 7);
            Console.WriteLine("Game");
            Console.SetCursorPosition(3, 9);
            Console.WriteLine("Or ESC To Leave");
            if (Console.ReadKey(true).Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }
        }

        public static void getDashboard()
        {
            Console.SetCursorPosition(30, 5);
            Console.WriteLine("Level : " + level);
            Console.SetCursorPosition(30, 7);
            Console.WriteLine("Score : " + score);
            Console.SetCursorPosition(30, 9);
            Console.WriteLine("Rows Cleared : " + rows);
        }
    }
}
