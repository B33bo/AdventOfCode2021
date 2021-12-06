using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//NGL had to google this one
//Well, the logic at least
namespace AdventOfCode2021.Solutions
{
    public static class Day6
    {
        static ulong[] fishByTypes = new ulong[9]; //For all 9 different types of fish (0,1,2,3,4,5,6,7,8)

        private static void Init()
        {
            string[] lines = File.ReadAllLines(Extensions.GetPath("Inputs/Day6.txt"));
            string[] fishAsInt = lines[0].Split(',');

            for (int i = 0; i < fishAsInt.Length; i++)
            {
                byte currentFish = byte.Parse(fishAsInt[i]);

                fishByTypes[currentFish]++;
            }
        }

        public static ulong SolveA()
        {
            Init();
            return GetGenerationsQuickly(80);
        }

        public static ulong SolveB()
        {
            Init();
            return GetGenerationsQuickly(256);
        }

        private static ulong GetGenerationsQuickly(int generations)
        {
            for (int i = 0; i < generations; i++)
            {
                fishByTypes = GetNextGen();

                Console.WriteLine($"Gen{i+1}:{Sum(fishByTypes)}");
            }

            return Sum(fishByTypes);
        }

        private static ulong Sum(ulong[] array)
        {
            ulong currentSum = 0;
            for (int i = 0; i < array.Length; i++)
            {
                currentSum += array[i];
            }
            return currentSum;
        }

        private static ulong[] GetNextGen()
        {
            ulong[] returnValue = new ulong[9];
            
            for (int i = 0; i < 8; i++)
            {
                if (i == 8)
                    continue;
                returnValue[i] = fishByTypes[i + 1];
            }

            returnValue[6] += fishByTypes[0];
            returnValue[8] = fishByTypes[0];

            return returnValue;
        }
    }
}
