using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public static class Extensions
    {
        public static string GetPath(string RelativeLoc)
        {
            List<string> currentDir = System.Reflection.Assembly.GetEntryAssembly().Location.Split(@"\").ToList();

            currentDir.RemoveAt(currentDir.Count - 1);
            string path = currentDir.ToRealString(@"\");
            return path + @"\" + RelativeLoc;
        }

        public static string ToRealString(this List<string> inputList, string seperator)
        {
            if (inputList.Count == 0)
                return "";

            string str = "";
            for (int i = 0; i < inputList.Count; i++)
            {
                str += seperator + inputList[i];
            }

            return str[seperator.Length..];
        }

        public static string Readable(this int[,] array)
        {
            string str = "";

            for (int x = 0; x < array.GetLength(0); x++)
            {
                for (int y = 0; y < array.GetLength(1); y++)
                {
                    if (array[x, y] > 9)
                        str += "*";
                    else
                        str += array[x, y];
                }
                str += "\n";
            }
            return str;
        }

        public static int Difference(int a, int b)
        {
            if (a > b)
                return a - b;
            return b - a;
        }
    }

    public struct Vector2
    {
        public int x, y;

        public Vector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2[] Intersections(Vector2 a, Vector2 b, bool solveDiags)
        {
            if (a.x == b.x)
            {
                //The X values are equal, find the change in Y values
                int difference = Extensions.Difference(a.y, b.y);
                int smallest = Math.Min(a.y, b.y);
                Vector2[] intersects = new Vector2[difference + 1];

                for (int i = 0; i < difference + 1; i++)
                {
                    intersects[i] = new Vector2(a.x, smallest + i);
                }
                return intersects;
            }

            if (a.y == b.y)
            {
                //The Y values are equal, find the change in X values
                int difference = Extensions.Difference(a.x, b.x);
                int smallest = Math.Min(a.x, b.x);
                Vector2[] intersects = new Vector2[difference + 1];

                for (int i = 0; i < difference + 1; i++)
                {
                    intersects[i] = new Vector2(smallest + i, a.y);
                }
                return intersects;
            }

            if (solveDiags)
            {
                if (IsDiagonal(a, b))
                {
                    Vector2 currentVec2 = a;
                    List<Vector2> intersections = new();
                    Direction dir = Directionof(a, b);
                    int i = 0;
                    while (currentVec2 != b)
                    {
                        //Console.WriteLine($"{currentVec2} intersects with {a}-{b}");
                        intersections.Add(currentVec2);

                        //if (currentVec2 == new Vector2(0, 0))
                            //Console.WriteLine($"{a} + {b} zero");

                        switch (dir)
                        {
                            case Direction.TopLeft:
                                currentVec2.x--;
                                currentVec2.y++;
                                break;
                            case Direction.TopRight:
                                currentVec2.x++;
                                currentVec2.y++;
                                break;
                            case Direction.BottomLeft:
                                currentVec2.x--;
                                currentVec2.y--;
                                break;
                            case Direction.BottomRight:
                                currentVec2.x++;
                                currentVec2.y--;
                                break;
                            default:
                                break;
                        }
                    }
                    intersections.Add(currentVec2);
                    return intersections.ToArray();
                }
            }

            return Array.Empty<Vector2>();
        }

        enum Direction
        {
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight
        }

        /// <summary>
        /// if B is further to the right of A, it's ?-right
        /// </summary>
        private static Direction Directionof(Vector2 a, Vector2 b)
        {
            if (a.x < b.x)
            {
                if (a.y < b.y)
                    return Direction.TopRight;
                return Direction.BottomRight;
            }

            if (a.y < b.y)
                return Direction.TopLeft;
            return Direction.BottomLeft;
        }

        public static bool IsDiagonal(Vector2 a, Vector2 b)
        {
            int newX = Math.Max(a.x, b.x) - Math.Min(a.x, b.x);
            int newY = Math.Max(a.y, b.y) - Math.Min(a.y, b.y);
            return newX == newY;
        }

        public static Vector2 Parse(string s)
        {
            string[] split = s.Split(',');

            return new(int.Parse(split[0]), int.Parse(split[1]));
        }

        public static Vector2 Parse(string[] s)
        {
            return new(int.Parse(s[0]), int.Parse(s[1]));
        }

        public override string ToString()
        {
            return $"({x},{y})";
        }

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x + b.x, a.y + b.y);
        }

        public static bool operator ==(Vector2 a, Vector2 b)
        {
            return (a.x == b.x) && (a.y == b.y);
        }

        public static bool operator !=(Vector2 a, Vector2 b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            return (Vector2)obj == this;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
