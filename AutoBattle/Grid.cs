using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static AutoBattle.Types;

namespace AutoBattle
{
    public class Grid
    {
        public List<GridBox> grids = new List<GridBox>();
        public int xLenght;
        public int yLength;
        public Grid(int Lines, int Columns)
        {
            xLenght = Lines;
            yLength = Columns;
            Console.WriteLine("The battle field has been created\n");
            for (int i = 0; i < Lines; i++)
            {
                    //grids.Add(newBox);
                for(int j = 0; j < Columns; j++)
                {
                    GridBox newBox = new GridBox(j, i, (Columns * i + j));
                    grids.Add(newBox);
                    Console.Write($"{newBox.index}\n");
                }
            }
        }

        // prints the matrix that indicates the tiles of the battlefield
        public void drawBattlefield(int Lines, int Columns)
        {
            for (int i = 0; i < Lines; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    GridBox currentgrid = grids[Columns * i + j];
                    if (currentgrid.character != null)
                    {
                        string character = (currentgrid.character.isEnemy) ? "X" : "0";
                        Console.Write("[" + character + "]\t");
                    }
                    else
                    {
                        Console.Write($"[ ]\t");
                    }
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            Console.Write(Environment.NewLine + Environment.NewLine);
        }

    }
}
