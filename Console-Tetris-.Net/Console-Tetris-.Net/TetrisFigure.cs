using System;
using System.Collections.Generic;

namespace Console_Tetris_.Net
{
    public class TetrisFigure
    {
        //FIGURES
        public static int[,] Figure1 = new int[1, 4] { { 1, 1, 1, 1 } }; // ""  ----  "" line
        public static int[,] Figure2 = new int[2, 2] { { 1, 1 }, { 1, 1 } }; // ""  [ ]  "" square
        public static int[,] Figure3 = new int[2, 3] { { 0, 1, 0 }, { 1, 1, 1 } }; // ""  |-  "" T
        public static int[,] Figure4 = new int[2, 3] { { 0, 1, 1 }, { 1, 1, 0 } }; // ""  __|¯¯  "" S
        public static int[,] Figure5 = new int[2, 3] { { 1, 1, 0 }, { 0, 1, 1 } }; // ""  ¯¯|__  "" Z
        public static int[,] Figure6 = new int[2, 3] { { 1, 0, 0 }, { 1, 1, 1 } }; // ""  _|  "" J
        public static int[,] Figure7 = new int[2, 3] { { 0, 0, 1 }, { 1, 1, 1 } }; // ""  |_  "" L

        public static bool isDropped = false;

        //List with all the figures
        public static List<int[,]> tetrisFigures = new List<int[,]>() { Figure1, Figure2, Figure3, Figure4, Figure5, Figure6, Figure7 };

        private int[,] block;
        public List<int[]> location = new List<int[]>();

        public TetrisFigure()
        {
            Random rnd = new Random();
            block = tetrisFigures[rnd.Next(0, 7)];
            Console.Clear();
            Program.DrawBorder();
            Program.GetDashboard();
            ShowTetrisFigureOnDashboard();
            

            
        }

        //Show the next tetris figure to be displayed on screen
        public void ShowTetrisFigureOnDashboard()
        {
            for (int i = 0; i < block.GetLength(0); i++)
            {
                for (int j = 0; j < block.GetLength(1); j++)
                {
                    if (block[i, j] == 1)
                    {
                        //we scale the block and then set the position
                        Console.SetCursorPosition(((10 - block.GetLength(1)) / 2 + j) * 2 + 30, i + 12);
                        Console.Write(Program.blockArea);
                    }
                }
            }
        }

        public void DisplayFigure()
        {
            for (int i = 0; i < block.GetLength(0); i++)
            {
                for (int j = 0; j < block.GetLength(1); j++)
                {
                    if (block[i, j] == 1)
                    {
                        location.Add(new int[] { i, (10 - block.GetLength(1)) / 2 + j });
                    }
                }
            }
            Update();
        }


        public static void Draw()
        {
            for (int i = 0; i < 23; ++i)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.SetCursorPosition(1 + 2 * j, i);
                    if (Program.grid[i, j] == 1 )
                    {
                        Console.SetCursorPosition(1 + 2 * j, i);
                        Console.Write(Program.blockArea);
                    }
                    else
                    {
                        Console.Write("  ");
                    }
                }

            }
        }
        public void Update()
        {
            for (int i = 0; i < 23; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Program.grid[i, j] = 0;
                }
            }
            for (int i = 0; i < 4; i++)
            {
                Program.grid[location[i][0], location[i][1]] = 1;
            }
            Draw();
        }

        //This is the loop that updates the console every time when the figure is falling down
        public static void ActivityLoop()
        {
            while (true)
            {

                if (TetrisFigure.isDropped == true)
                {
                    tFig = nextTFig;
                    nextTFig = new TetrisFigure();
                    tFig.DisplayFigure();

                    TetrisFigure.isDropped = false;
                }
                for (int j = 0; j < 10; j++)
                {
                    if (spawnedBlockLocation[0, j] == 1)
                        return;
                }

            }
        }

    }
}
