using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Globalization;

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
        
        public List<Point> GetList()
        {
            return queue;
        }
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
        public Point Peek()
        {
            return queue[0];
        }
        public Point Last()
        {
            return queue[queue.Count - 1];
        }
        public bool IsEmpty()
        {
            return queue.Count == 0;
        }
        public bool ContainsDuplicates()
        {
            return queue.Count != queue.Distinct().Count();
		}
    }
    public class Snake
    {
        private PointLinearQueue queue;
        private string direction;
        private Point grid;
        public Snake(int x, int y, Point grid)
        {
            Point point = new Point(x, y);
            direction = "right";
            this.grid = grid;
            queue = new PointLinearQueue();
            queue.Enqueue(point);
        }
        public List<Point> GetQueueList()
        {
            return queue.GetList();
        }
        public void FindDirection()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.UpArrow && direction != "down")
                {
					direction = "up";
                }
                else if (keyInfo.Key == ConsoleKey.RightArrow && direction != "left")
                {
					direction = "right";
				}
				else if (keyInfo.Key == ConsoleKey.DownArrow && direction != "up")
                {
					direction = "down";
				}
				else if (keyInfo.Key == ConsoleKey.LeftArrow && direction != "right")
                {
					direction = "left";
				}
			}
        }
        public void Move()
        {
            Point head = queue.Last();
            Point tail = queue.Peek();

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

			Console.SetCursorPosition(head.X * 2, head.Y);
            Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("██");
            Console.ResetColor();
		}
        public void Shrink()
        {
            Point tail = queue.Dequeue();
			Console.SetCursorPosition(tail.X * 2, tail.Y);
			Console.Write("  ");
		}
        public bool CheckCollision()
        {
            Point head = queue.Last();
			if (head.X < 1 || head.Y < 1 || head.X >= grid.X - 1|| head.Y >= grid.Y - 1)
			{
				return true;
			}
			if (queue.ContainsDuplicates())
			{
				return true;
			}
			return false;
		}
        public bool HasEatenApple(Apple a)
        {
			Point head = queue.Last();
            Point apple = a.GetPoint();
			if (head.X == apple.X && head.Y == apple.Y)
            {
                return true;
            }
            return false;
        }
    }
    public class Apple
    {
        private Point point;
        private Point grid;
        private Snake snake;
        private Random rand = new Random();
        public Apple(Snake snake, Point grid)
        {
            this.snake = snake;
            this.grid = grid;
            GeneratePosition();
			PrintApple();
		}
		public Point GetPoint()
        {
            return point;
        }
        public void GeneratePosition()
        {
            while (true)
            {
				point = new Point(rand.Next(1, grid.X-1), rand.Next(1, grid.Y-1));
                if (!IsAppleOnSnake(snake))
                {
                    break;
                }
			}
		}
        public void PrintApple()
        {
            Console.SetCursorPosition(point.X * 2, point.Y);
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write("██");
			Console.ResetColor();
		}
        public bool IsAppleOnSnake(Snake snake)
        {
            List<Point> list = snake.GetQueueList();
            foreach (Point p in list)
            {
                if (p.X == point.X && p.Y == point.Y)
                {
                    return true;
                }
            }
            return false;
        }
	}
    internal class Program
    {
        public static void PrintGrid(Point grid)
        {
			Console.ForegroundColor = ConsoleColor.White;
			for (int y = 0; y < grid.Y; y++)
            {
                for (int x = 0; x < grid.X; x++)
                {
                    if (x == 0 || y == 0 || x == grid.X - 1|| y == grid.Y - 1)
                    {
						Console.Write("██");
					}
                    else
                    {
                        Console.Write("  ");
                    }
                }
                Console.WriteLine("");
            }
			Console.ResetColor();
		}
		static void Main(string[] args)
        {
            //Console.WriteLine($"                 _        \r\n                | |       \r\n ___ _ __   __ _| | _____ \r\n/ __| '_ \\ / _` | |/ / _ \\\r\n\\__ \\ | | | (_| |   <  __/\r\n|___/_| |_|\\__,_|_|\\_\\___|");

            //Console.WriteLine($"\nPress any key");
            //Console.ReadKey();
            //Console.Clear();
            
            Console.CursorVisible = false;
            Console.SetWindowSize(100, 30);
			Point grid = new Point(25, 25);
			PrintGrid(grid);

			Snake snake = new Snake(2, 2, grid);
            Apple apple = new Apple(snake, grid);
            while (true)
            {
                snake.FindDirection();
                snake.Move();
                if (snake.CheckCollision())
                {
                    break;
                }
                if (snake.HasEatenApple(apple))
                {
                    apple.GeneratePosition();
                    apple.PrintApple();
                }
                else
                {
                    snake.Shrink();
                }
                Thread.Sleep(100);
            }
            Console.SetCursorPosition(0, grid.Y + 2);
            Console.WriteLine($"Game over!");

			Thread.Sleep(1000);

			Console.ReadKey();
        }
    }
}
