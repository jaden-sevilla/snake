using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Coord
    {
        public int Y;
        public int X;
        public Coord(int Y, int X)
        {
            this.X = X;
            this.Y = Y;
        }
    }
    internal class Program
    {
        public static void PrintGrid(Coord grid)
        {
            for (int y = 0; y < grid.Y; y++)
            {
                for (int x = 0; x < grid.X; x++)
                {
                    if (x == 0 || y == 0 || x == grid.X - 1|| y == grid.Y - 1)
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine("");
            }
        }

        static void Main(string[] args)
        {
            Coord grid = new Coord(20, 50);
            Coord snake = new Coord(2, 10);
            PrintGrid(grid);

            Console.SetCursorPosition(snake.X, snake.Y);
            Console.WriteLine("■");

            

            Console.ReadKey();
        }
    }
}
