using System;

namespace Console_Tetris_.Net
{
    class Program
    {
        public static string blockArea = "■";   //content of the tetris figure
        public static int rows = 0, score = 0, level = 1;   //eliminated rows, score and level
        public static int[,] grid = new int[23, 10];    
        public static int[,] spawnedBlockLocation = new int[23, 10];    //the location of the block that is falling down
        private static TetrisFigure tFig;   //figure falling down
        private static TetrisFigure nextTFig;   //next figure to be spawned

        static void Main()
        {

            DrawBorder();
            GetMenu();

            tFig = new TetrisFigure();
            tFig.DisplayFigure();
            nextTFig = new TetrisFigure();

            TetrisFigure.ActivityLoop();

            Console.ReadLine();

        }

        //Drawing Borders
        public static void DrawBorder()
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
        public static void GetMenu()
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

        //Dashboard that shows score, level and the ammount of rows that you eliminate
        public static void GetDashboard()
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
