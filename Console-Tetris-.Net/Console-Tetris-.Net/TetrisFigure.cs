using System;
using System.Collections.Generic;
using System.Linq;

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

        //List with all the figures
        public static List<int[,]> tetrisFigures = new List<int[,]>() { Figure1, Figure2, Figure3, Figure4, Figure5, Figure6, Figure7 };

        //Display
        private int[,] block;
        public List<int[]> location = new List<int[]>();
        private bool isErect = false;
  

        //ctor of the Tetris Figure
        public TetrisFigure()
        {
            Random rnd = new Random();
            block = tetrisFigures[rnd.Next(0, 7)];
            Console.Clear();
            Program.DrawBorder();
            Program.GetDashboard(1,0,0);
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

        //draw the figure when we actualize the console
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

        //update the console
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

        //Funcion that Fall down the tetris figure
        public void Drop()
        {

            if (isSomethingBelow())
            {
                for (int i = 0; i < 4; i++)
                {
                    Program.spawnedBlockLocation[location[i][0], location[i][1]] = 1; //should change name to dropped block location
                }
                Program.isDropped = true;

            }
            else
            {
                for (int numCount = 0; numCount < 4; numCount++)
                {
                    location[numCount][0] += 1;
                }
                Update();
            }
        }


        //IN THIS PART THERE ARE METHODS OF MOVEMENT

        //Rotate function analizes all the figures and what to verify if we want to rotate it
        public void Rotate()
        {
            List<int[]> templocation = new List<int[]>();
            for (int i = 0; i < block.GetLength(0); i++)
            {
                for (int j = 0; j < block.GetLength(1); j++)
                {
                    if (block[i, j] == 1)
                    {
                        templocation.Add(new int[] { i, (10 - block.GetLength(1)) / 2 + j });
                    }
                }
            }

            if (block == tetrisFigures[0])
            {
                if (isErect == false)
                {
                    for (int i = 0; i < location.Count; i++)
                    {
                        templocation[i] = TransformMatrix(location[i], location[2], "Clockwise");
                    }
                }
                else
                {
                    for (int i = 0; i < location.Count; i++)
                    {
                        templocation[i] = TransformMatrix(location[i], location[2], "Counterclockwise");
                    }
                }
            }

            else if (block == tetrisFigures[3])
            {
                for (int i = 0; i < location.Count; i++)
                {
                    templocation[i] = TransformMatrix(location[i], location[3], "Clockwise");
                }
            }

            else if (block == tetrisFigures[1]) return;
            else
            {
                for (int i = 0; i < location.Count; i++)
                {
                    templocation[i] = TransformMatrix(location[i], location[2], "Clockwise");
                }
            }


            for (int count = 0; isOverlayLeft(templocation) != false | isOverlayRight(templocation) != false | isOverlayBelow(templocation) != false; count++)
            {
                if (isOverlayLeft(templocation) == true)
                {
                    for (int i = 0; i < location.Count; i++)
                    {
                        templocation[i][1] += 1;
                    }
                }

                if (isOverlayRight(templocation) == true)
                {
                    for (int i = 0; i < location.Count; i++)
                    {
                        templocation[i][1] -= 1;
                    }
                }
                if (isOverlayBelow(templocation) == true)
                {
                    for (int i = 0; i < location.Count; i++)
                    {
                        templocation[i][0] -= 1;
                    }
                }
                if (count == 3)
                {
                    return;
                }
            }

            location = templocation;

        }

        // we analize the figure when we want to transform it and it depends of the clock wise direction
        public int[] TransformMatrix(int[] coord, int[] axis, string dir)
        {
            int[] pcoord = { coord[0] - axis[0], coord[1] - axis[1] };
            if (dir == "Counterclockwise")
            {
                pcoord = new int[] { -pcoord[1], pcoord[0] };
            }
            else if (dir == "Clockwise")
            {
                pcoord = new int[] { pcoord[1], -pcoord[0] };
            }

            return new int[] { pcoord[0] + axis[0], pcoord[1] + axis[1] };
        }


        //check if there is something below, and the same for the other ones IS SOMETHING
        public bool isSomethingBelow()
        {
            for (int i = 0; i < 4; i++)
            {
                if (location[i][0] + 1 >= 23)
                    return true;
                if (location[i][0] + 1 < 23)
                {
                    if (Program.spawnedBlockLocation[location[i][0] + 1, location[i][1]] == 1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool? isOverlayBelow(List<int[]> location)
        {
            List<int> ycoords = new List<int>();
            for (int i = 0; i < 4; i++)
            {
                ycoords.Add(location[i][0]);
                if (location[i][0] >= 23)
                    return true;
                if (location[i][0] < 0)
                    return null;
                if (location[i][1] < 0)
                {
                    return null;
                }
                if (location[i][1] > 9)
                {
                    return null;
                }
            }
            for (int i = 0; i < 4; i++)
            {
                if (ycoords.Max() - ycoords.Min() == 3)
                {
                    if (ycoords.Max() == location[i][0] | ycoords.Max() - 1 == location[i][0])
                    {
                        if (Program.spawnedBlockLocation[location[i][0], location[i][1]] == 1)
                        {
                            return true;
                        }
                    }

                }
                else
                {
                    if (ycoords.Max() == location[i][0])
                    {
                        if (Program.spawnedBlockLocation[location[i][0], location[i][1]] == 1)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }


        public bool isSomethingLeft()
        {
            for (int i = 0; i < 4; i++)
            {
                if (location[i][1] == 0)
                {
                    return true;
                }
                else if (Program.spawnedBlockLocation[location[i][0], location[i][1] - 1] == 1)
                {
                    return true;
                }
            }
            return false;
        }
        public bool? isOverlayLeft(List<int[]> location)
        {
            List<int> xcoords = new List<int>();
            for (int i = 0; i < 4; i++)
            {
                xcoords.Add(location[i][1]);
                if (location[i][1] < 0)
                {
                    return true;
                }
                if (location[i][1] > 9)
                {
                    return false;
                }
                if (location[i][0] >= 23)
                    return null;
                if (location[i][0] < 0)
                    return null;
            }
            for (int i = 0; i < 4; i++)
            {
                if (xcoords.Max() - xcoords.Min() == 3)
                {
                    if (xcoords.Min() == location[i][1] | xcoords.Min() + 1 == location[i][1])
                    {
                        if (Program.spawnedBlockLocation[location[i][0], location[i][1]] == 1)
                        {
                            return true;
                        }
                    }

                }
                else
                {
                    if (xcoords.Min() == location[i][1])
                    {
                        if (Program.spawnedBlockLocation[location[i][0], location[i][1]] == 1)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool isSomethingRight()
        {
            for (int i = 0; i < 4; i++)
            {
                if (location[i][1] == 9)
                {
                    return true;
                }
                else if (Program.spawnedBlockLocation[location[i][0], location[i][1] + 1] == 1)
                {
                    return true;
                }
            }
            return false;
        }

        public bool? isOverlayRight(List<int[]> location)
        {
            List<int> xcoords = new List<int>();
            for (int i = 0; i < 4; i++)
            {
                xcoords.Add(location[i][1]);
                if (location[i][1] > 9)
                {
                    return true;
                }
                if (location[i][1] < 0)
                {
                    return false;
                }
                if (location[i][0] >= 23)
                    return null;
                if (location[i][0] < 0)
                    return null;
            }
            for (int i = 0; i < 4; i++)
            {
                if (xcoords.Max() - xcoords.Min() == 3)
                {
                    if (xcoords.Max() == location[i][1] | xcoords.Max() - 1 == location[i][1])
                    {
                        if (Program.spawnedBlockLocation[location[i][0], location[i][1]] == 1)
                        {
                            return true;
                        }
                    }

                }
                else
                {
                    if (xcoords.Max() == location[i][1])
                    {
                        if (Program.spawnedBlockLocation[location[i][0], location[i][1]] == 1)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

    }
}
