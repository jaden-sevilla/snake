using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public struct Point
    {
        public int X;
        public int Y;
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
    public class PointLinearQueue
    {
        private List<Point> queue = new List<Point>();
        
        public void Enqueue(Point point)
        {
            queue.Add(point);
        }
        public Point Dequeue()
        {
            Point point = new Point(-1,-1);
            if (!IsEmpty())
            {
                point = queue[queue.Count - 1];
                queue.RemoveAt(queue.Count - 1);
            }
            return point;
        }
        public bool IsEmpty()
        {
            return queue.Count == 0;
        }
    }
    public class Snake
    {
        private PointLinearQueue queue;
        private string direction;
        public Snake(int x, int y)
        {
            Point point = new Point(x, y);
            queue = new PointLinearQueue();
            queue.Enqueue(point);
        }
        public void Move()
        {
            Point head;
            if (direction == "up")
            {
                head = new Point(, 0);
            }
        }
        public void ChangeDirection()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    direction = "up";
                }
                else if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    direction = "right";
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    direction = "down";
                }
                else if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    direction = "left";
                }
            }
        }
    }
    internal class Program
    {
        public static void PrintGrid(Point grid)
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
            Point grid = new Point(50, 20);
            
            

            Point snake = new Point(2, 10);
            PrintGrid(grid);

            Console.SetCursorPosition(snake.X, snake.Y);
            Console.WriteLine("■");

            

            Console.ReadKey();
        }
    }
}
