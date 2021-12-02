using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Solutions
{
    public static class Day2
    {
        public static int SolveA()
        {
            string path = Extensions.GetPath("Inputs/Day2.txt");
            string[] inputs = File.ReadAllLines(path);

            int horizontal = 0, depth = 0;

            for (int i = 0; i < inputs.Length; i++)
            {
                int number = int.Parse(inputs[i].Split(' ')[1]);

                switch (inputs[i][0])
                {
                    default:
                        Console.WriteLine($"ERROR: {inputs[i]} is not a valid instruction");
                        break;
                    case 'f': //Forward (horizontal)
                        horizontal += number;
                        break;
                    case 'u': //Up      (-depth)
                        depth -= number;
                        break;
                    case 'd': //Down    (depth)
                        depth += number;
                        break;
                }
            }

            return horizontal * depth;
        }

        public static int SolveB()
        {
            string path = Extensions.GetPath("Inputs/Day2.txt");
            string[] inputs = File.ReadAllLines(path);

            int horizontal = 0, depth = 0, aim = 0;

            for (int i = 0; i < inputs.Length; i++)
            {
                int number = int.Parse(inputs[i].Split(' ')[1]);

                switch (inputs[i][0])
                {
                    default:
                        Console.WriteLine($"ERROR: {inputs[i]} is not a valid instruction");
                        break;
                    case 'f': //Forward (horizontal)
                        horizontal += number;
                        depth += aim * number;
                        break;
                    case 'u': //Up      (-depth)
                        //depth -= number;
                        aim -= number;
                        break;
                    case 'd': //Down    (depth)
                        //depth += number;
                        aim += number;
                        break;
                }
            }

            return horizontal * depth;
        }
    }
}

