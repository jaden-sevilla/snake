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
                point = queue[0];
                queue.RemoveAt(0);
            }
            return point;
        }
        public Point FindLast()
        {
            return queue[queue.Count - 1];
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
        public string FindDirection()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    return "up";
                }
                else if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    return "right";
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    return "down";
                }
                else if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    return "left";
                }
            }
            return null;
        }
        public void Move()
        {
            Point head = queue.FindLast();
            Point tail = queue.Dequeue();
            if (direction == "up")
            {
                head = new Point(head.X, head.Y - 1);
            }
            else if (direction == "right")
            {
                head = new Point(head.X + 1, head.Y);
            }
            else if (direction == "down")
            {
                head = new Point(head.X, head.Y + 1);
            }
            else if (direction == "left")
            {
                head = new Point(head.X - 1, head.Y);
            }
            queue.Enqueue(head);
            
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
            
            //Point snake = new Point(2, 10);
            //PrintGrid(grid);

            //Console.SetCursorPosition(snake.X, snake.Y);
            //Console.WriteLine("■");

            Snake snake = new Snake(2, 1);
            snake.Move();
            

            Console.ReadKey();
        }
    }
}
