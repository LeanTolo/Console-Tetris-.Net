using System;
using System.Diagnostics;

namespace Console_Tetris_.Net
{
    class Program
    {
        public static string blockArea = "■";   //content of the tetris figure
        public static int rows = 0, score = 0, level = 1;   //eliminated rows, score and level
        public static int[,] grid = new int[23, 10];    

        //Repeated statics Atributes
        private static TetrisFigure tFig;   //figure falling down
        private static TetrisFigure nextTFig;   //next figure to be spawned
        public static bool isDropped = false;
        public static int[,] spawnedBlockLocation = new int[23, 10];    //the location of the block that is falling down

        //Timers
        public static Stopwatch timer = new Stopwatch();
        public static Stopwatch dropTimer = new Stopwatch();
        public static Stopwatch inputTimer = new Stopwatch();
        public static int dropTime, dropRate = 300;

        //Movement
        public static ConsoleKeyInfo pressedKey;
        public static bool isKeyPressed = false;

        static void Main()
        {

            DrawBorder();
            GetMenu();

            tFig = new TetrisFigure();
            tFig.DisplayFigure();
            nextTFig = new TetrisFigure();

            RefreshConsole();

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
        public static void GetDashboard(int levels, int scores, int rowss)
        {
            Console.SetCursorPosition(30, 5);
            Console.WriteLine("Level : " + levels);
            Console.SetCursorPosition(30, 7);
            Console.WriteLine("Score : " + scores);
            Console.SetCursorPosition(30, 9);
            Console.WriteLine("Rows Cleared : " + rowss);
        }

        private static void RefreshConsole()
        {
            while (true)//Update Loop
            {
                dropTime = (int)dropTimer.ElapsedMilliseconds;
                if (dropTime > dropRate)
                {
                    dropTime = 0;
                    dropTimer.Restart();
                    tFig.Drop();
                }
                if (isDropped == true)
                {
                    tFig = nextTFig;
                    nextTFig = new TetrisFigure();
                    tFig.DisplayFigure();

                    isDropped = false;
                }
                for (int j = 0; j < 10; j++)
                {
                    if (spawnedBlockLocation[0, j] == 1)
                        return;
                }
                ClearBlock();
                Input();
            } //end Update

        }

        //We read the key that we press
        private static void Input()
        {
            if (Console.KeyAvailable)
            {
                pressedKey = Console.ReadKey();
                isKeyPressed = true;
            }
            else
                isKeyPressed = false;

            if (pressedKey.Key == ConsoleKey.LeftArrow & !tFig.isSomethingLeft() & isKeyPressed)
            {
                for (int i = 0; i < 4; i++)
                {
                    tFig.location[i][1] -= 1;
                }
                tFig.Update();
            }
            else if (pressedKey.Key == ConsoleKey.RightArrow & !tFig.isSomethingRight() & isKeyPressed)
            {
                for (int i = 0; i < 4; i++)
                {
                    tFig.location[i][1] += 1;
                }
                tFig.Update();
            }
            if (pressedKey.Key == ConsoleKey.DownArrow & isKeyPressed)
            {
                tFig.Drop();
            }
            if (pressedKey.Key == ConsoleKey.UpArrow & isKeyPressed)
            {
                for (; tFig.isSomethingBelow() != true;)
                {
                    tFig.Drop();
                }
            }
            if (pressedKey.Key == ConsoleKey.Spacebar & isKeyPressed)
            {
                //rotate
                tFig.Rotate();
                tFig.Update();
            }
        }

        //what happens when we complete a line in the grid
        private static void ClearBlock()
        {
            int combo = 0;
            for (int i = 0; i < 23; i++)
            {
                int j;
                for (j = 0; j < 10; j++)
                {
                    if (spawnedBlockLocation[i, j] == 0)
                        break;
                }
                if (j == 10)
                {
                    rows++;
                    combo++;
                    for (j = 0; j < 10; j++)
                    {
                        spawnedBlockLocation[i, j] = 0;
                    }
                    int[,] newTetLocation = new int[23, 10];
                    for (int k = 1; k < i; k++)
                    {
                        for (int l = 0; l < 10; l++)
                        {
                            newTetLocation[k + 1, l] = spawnedBlockLocation[k, l];
                        }
                    }
                    for (int k = 1; k < i; k++)
                    {
                        for (int l = 0; l < 10; l++)
                        {
                            spawnedBlockLocation[k, l] = 0;
                        }
                    }
                    for (int k = 0; k < 23; k++)
                        for (int l = 0; l < 10; l++)
                            if (newTetLocation[k, l] == 1)
                                spawnedBlockLocation[k, l] = 1;
                    TetrisFigure.Draw();
                }
            }

            lvlModifier(combo);

            GetDashboard(level, score, rows);

            dropRate = 300 - 22 * level;

        }

        public static void lvlModifier(int combo)
        {
            if (combo == 1)
                score += 50 * level;
            else if (combo == 2)
                score += 100 * level;
            else if (combo == 3)
                score += 300 * level;
            else if (combo > 3)
                score += 500 * combo * level;

            if (rows < 10) level = 1;
            else if (rows < 20) level = 2;
            else if (rows < 35) level = 3;
            else if (rows < 45) level = 4;
            else if (rows < 55) level = 5;
            else if (rows < 70) level = 6;
            else if (rows < 90) level = 7;
            else if (rows < 110) level = 8;
            else if (rows < 130) level = 9;
            else if (rows < 150) level = 10;
        }
    }
}
