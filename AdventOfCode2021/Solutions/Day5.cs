using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Solutions
{
    public static class Day5
    {
        static int[,] points;
        static int intersectionCount;
        public static void LoadLines(bool includeDiags)
        {
            intersectionCount = 0;
            points = new int[1000, 1000];
            string[] lines = File.ReadAllLines(Extensions.GetPath("Inputs/Day5.txt"));

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] splitByArrow = lines[i].Split('-');

                splitByArrow[0] = splitByArrow[0][..^1]; //Removes the annoying space
                splitByArrow[1] = splitByArrow[1][2..];  //Removes the "> "

                Vector2 a = Vector2.Parse(splitByArrow[0]);
                Vector2 b = Vector2.Parse(splitByArrow[1]);

                Vector2[] intersections = Vector2.Intersections(a, b, includeDiags);
                foreach (var intersect in intersections)
                {
                    //Console.WriteLine($"{intersect} {a}->{b}");
                    points[intersect.x, intersect.y]++;

                    if (points[intersect.x, intersect.y] == 2)
                        intersectionCount++;
                }

                if (i % 5000 == 0)
                    Console.Beep();
            }
        }

        public static int SolveA()
        {
            LoadLines(false);

            //Console.WriteLine(points.Readable());
            return intersectionCount;
        }

        public static int SolveB()
        {
            LoadLines(true);
            
            //Console.WriteLine(points.Readable());
            return intersectionCount;
        }
    }
}